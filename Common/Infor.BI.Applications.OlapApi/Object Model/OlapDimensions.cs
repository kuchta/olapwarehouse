using Infor.BI.Applications.OlapApi.Native;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents a collection of Olap dimensions.
    /// </summary>
    public class OlapDimensions : OlapCollectionObjectBase<OlapDimension>, System.Collections.IEnumerable
    {
        /// <summary>
        /// Holds the Olap server that owns the dimensions.
        /// </summary>
        private OlapServer _server;

        /// <summary>
        /// Holds the cube name if dimensions are to retrieve for a cube.
        /// </summary>
        private string _cubeName;

        /// <summary>
        /// Holds a map of names to indexes to the element collection.
        /// </summary>
        private System.Collections.Generic.Dictionary<string, int> _accessCollection;

        /// <summary>
        /// Loads the dimensions.
        /// </summary>
        private void Load()
        {
            if (Invalid)
            {
                IsInitialized = false;
            }

            if (!IsInitialized)
            {
                Collection = new System.Collections.Generic.List<OlapDimension>(0);
                _accessCollection = new System.Collections.Generic.Dictionary<string, int>();

                System.Collections.Specialized.StringCollection dimensionNames = null;

                if (_cubeName != null)
                {
                    dimensionNames = NativeOlapApi.CubeDimensions(_server.Store.ClientSlot, _server.ServerHandle, _cubeName, _server.LastErrorInternal);
                }
                else
                {
                    dimensionNames = NativeOlapApi.Dimensions(_server.Store.ClientSlot, _server.ServerHandle, _server.LastErrorInternal);
                }

                if (dimensionNames != null)
                {
                    for (int i = 0; i < dimensionNames.Count; i++)
                    {
                        string dimname = dimensionNames[i];
                        Collection.Add(new OlapDimension(_server, dimname));
                        _accessCollection.Add(dimname.ToUpper(), i);
                    }
                }
                else
                {
                    if (_server.LastErrorInternal.Value != 0)
                    {
                        throw new OlapException("Receiving the dimension collection failed!", _server.LastErrorInternal.Value);
                    }
                }
                Invalid = false;
                IsInitialized = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the OlapDimensions class. This instance will provide all
        /// dimensions of the server.
        /// </summary>
        /// <param name="server">The server that owns the dimensions.</param>
        public OlapDimensions(OlapServer server)
        {
            _server = server;
            _cubeName = null;
            _accessCollection = null;
        }

        /// <summary>
        /// Initializes a new instance of the OlapDimensions class. This instance will provide all
        /// dimensions of the specified cube.
        /// </summary>
        /// <param name="server">The server that owns the dimensions.</param>
        /// <param name="cubeName">The name of the cube for which to get the dimensions.</param>
        public OlapDimensions(OlapServer server, string cubeName)
        {
            _server = server;
            _cubeName = cubeName;
            _accessCollection = null;
        }

        /// <summary>
        /// Gets the dimension at the specified index of the collection.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>The OlapDimension or null, if the index was invalid.</returns>
        public OlapDimension this[int index]
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
        /// Gets the dimension with the specified name from the collection.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <returns>The OlapDimension or null, if the name was invalid.</returns>
        public OlapDimension this[string name]
        {
            get
            {
                Load();
                try
                {
                    int index = _accessCollection[name.ToUpper()];
                    return Collection[index];
                }
                catch (System.Exception)
                { 
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
        /// Gets an enumerator to iterator through the collection.
        /// </summary>
        /// <returns>An IEnumerator of the collection.</returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            Load();
            return Collection.GetEnumerator();
        }
    }
}
