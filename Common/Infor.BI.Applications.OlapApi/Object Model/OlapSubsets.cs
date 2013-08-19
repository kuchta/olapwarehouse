using Infor.BI.Applications.OlapApi.Native;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents a collection of Olap servers.
    /// </summary>
    public class OlapSubsets : OlapCollectionObjectBase<OlapSubset>, System.Collections.IEnumerable
    {
        /// <summary>
        /// Holds the OlapDimension that owns this subset collection.
        /// </summary>
        private OlapDimension _dimension;

        /// <summary>
        /// Loads the subset collection.
        /// </summary>
        private void Load()
        {
            if (Invalid)
            {
                IsInitialized = false;
            }

            if (!IsInitialized)
            {
                Collection = new System.Collections.Generic.List<OlapSubset>(0);
                System.Collections.ArrayList subsets = NativeOlapApi.DimensionSubsets(_dimension.Server.Store.ClientSlot, _dimension.Server.ServerHandle, _dimension.Name, _dimension.Server.LastErrorInternal);
                if (subsets != null)
                {
                    for (int i = 0; i < subsets.Count; i++)
                    {
                        OlapSubsetTypes type;
                        OlapSubsetDefinition subsetDef = (OlapSubsetDefinition)subsets[i];

                        if ((subsetDef.Type & 0x01) == 0x01)
                        {
                            type = OlapSubsetTypes.OlapSubsetTypesPublic;
                        }
                        else if ((subsetDef.Type & 0x02) == 0x02)
                        {
                            type = OlapSubsetTypes.OlapSubsetTypesPrivate;
                        }
                        else
                        {
                            throw new OlapException("Found unexpected subset type: " + subsetDef.Type);
                        }

                        Collection.Add(new OlapSubset(_dimension, subsetDef.RefName, subsetDef.LongName, subsetDef.CreatedByUser, type, subsetDef.SaveResultSet));
                    }
                }
                else
                {
                    if (_dimension.Server.LastErrorInternal.Value != 0)
                    {
                        throw new OlapException("Receiving the subset collection failed!", _dimension.Server.LastErrorInternal.Value);
                    }
                }
                Invalid = false;
                IsInitialized = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the OlapSubset class.
        /// </summary>
        /// <param name="dimension">The OlapDimension that owns this collection.</param>
        public OlapSubsets(OlapDimension dimension)
        {
            _dimension = dimension;
        }

        /// <summary>
        /// Gets the subset at the specified index of the collection.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>The OlapSubset or null, if the index was invalid.</returns>
        public OlapSubset this[int index]
        {
            get
            {
                Load();
                if (0 <= index && index < Collection.Count)
                {
                    return Collection[index];
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the number of items currently in the collection.
        /// </summary>
        /// <returns>The number of items in the collection.</returns>
        public int Count
        {
            get
            {
                Load();
                return Collection.Count;
            }
        }

        /// <summary>
        /// Gets an enumerator to iterate through the collection.
        /// </summary>
        /// <returns>An IEnumerator of the collection.</returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            Load();
            return Collection.GetEnumerator();
        }
    }
}
