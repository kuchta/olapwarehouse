using Infor.BI.Applications.OlapApi.Native;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents a collection of Olap attribute tables.
    /// </summary>
    public class OlapAttributeTables : OlapCollectionObjectBase<OlapAttributeTable>, System.Collections.IEnumerable
    {
        /// <summary>
        /// Holds the OlapStore that owns this server collection.
        /// </summary>
        private OlapDimension _dimension;

        private void Load()
        {
            if (Invalid)
            {
                IsInitialized = false;
            }

            if (!IsInitialized)
            {
                Collection = new System.Collections.Generic.List<OlapAttributeTable>(0);
                System.Collections.ArrayList attributeTables = NativeOlapApi.AttributeTables(_dimension.Server.Store.ClientSlot, _dimension.Server.ServerHandle, _dimension.Name, _dimension.Server.LastErrorInternal);
                if (attributeTables != null)
                {
                    for (int i = 0; i < attributeTables.Count; i++)
                    {
                        OlapAttributeTableDefintion tableDef = (OlapAttributeTableDefintion)attributeTables[i];
                        Collection.Add(new OlapAttributeTable(_dimension, tableDef.Name, tableDef.Id, tableDef.FieldCount, tableDef.RecordCount));
                    }
                }
                else
                {
                    if (_dimension.Server.LastErrorInternal.Value != 0)
                    {
                        throw new OlapException("Receiving the attribute table collection failed!", _dimension.Server.LastErrorInternal.Value);
                    }
                }
                Invalid = false;
                IsInitialized = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the OlapAttributeTables class.
        /// </summary>
        /// <param name="dimension">The Olap dimension that owns this collection.</param>
        public OlapAttributeTables(OlapDimension dimension)
        {
            _dimension = dimension;
        }

        /// <summary>
        /// Gets the Olap attribute table at the specified index of the collection.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>The OlapAttributeTable or null, if th index was invalid.</returns>
        public OlapAttributeTable this[int index]
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