using Infor.BI.Applications.OlapApi.Native;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents a collection of Olap cubes.
    /// </summary>
    public class OlapCubes : OlapCollectionObjectBase<OlapCube>, System.Collections.IEnumerable
    {
        /// <summary>
        /// Holds the server that owns the cubes.
        /// </summary>
        private OlapServer _server;

        /// <summary>
        /// Holds a map of names to indexes to the element collection.
        /// </summary>
        private System.Collections.Generic.Dictionary<string, int> _accessCollection;

        private void Load()
        {
            if (Invalid)
            {
                IsInitialized = false;
            }
            if (!IsInitialized)
            {
                Collection = new System.Collections.Generic.List<OlapCube>(0);
                _accessCollection = new System.Collections.Generic.Dictionary<string, int>();

                System.Collections.Specialized.StringCollection cubeNames = NativeOlapApi.Cubes(_server.Store.ClientSlot, _server.ServerHandle, _server.LastErrorInternal);
                if (cubeNames != null)
                {
                    for (int i = 0; i < cubeNames.Count; i++)
                    {
                        string cubename = cubeNames[i];
                        Collection.Add(new OlapCube(_server, cubename, _server.ServerHandle));
                        _accessCollection.Add(cubename.ToUpper(), i);
                    }
                }
                else
                {
                    if (_server.LastErrorInternal.Value != 0)
                    {
                        throw new OlapException("Receiving the cubes collection failed!", _server.LastErrorInternal.Value);
                    }
                }
                Invalid = false;
                IsInitialized = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the OlapCubes class.
        /// </summary>
        /// <param name="server">The server that owns the cubes.</param>
        public OlapCubes(OlapServer server)
        {
            _server = server;
            _accessCollection = null;
        }

        /// <summary>
        /// Gets the cube at the specified index of the collection.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>The OlapCube or null, if the index was invalid.</returns>
        public OlapCube this[int index]
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
        /// Gets the cube at the specified name of the collection.
        /// </summary>
        /// <param name="name">The nameof the item.</param>
        /// <returns>The OlapCube or null, if the name was invalid.</returns>
        public OlapCube this[string name]
        {
            get
            {
                Load();
                string nameUpper = name.ToUpper();

                try
                {
                    int index = this._accessCollection[nameUpper];
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
