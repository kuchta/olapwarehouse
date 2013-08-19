using Infor.BI.Applications.OlapApi.Native;
using Infor.BI.Common.Application;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents a collection of Olap servers.
    /// </summary>
    public class OlapServers : OlapCollectionObjectBase<OlapServer>, System.Collections.IEnumerable
    {
        /// <summary>
        /// Holds the OlapStore that owns this server collection.
        /// </summary>
        private OlapStore _store;

        /// <summary>
        /// Loads the server collection.
        /// </summary>
        private void Load()
        {
            if (Invalid)
            {
                IsInitialized = false;
            }

            if (!IsInitialized)
            {
                IntPointer lastError = new IntPointer();
                Collection = new System.Collections.Generic.List<OlapServer>(0);
                System.Collections.Specialized.StringCollection serverNames = NativeOlapApi.Servers(_store.ClientSlot, lastError);
                if (serverNames != null)
                {
                    for (int i = 0; i < serverNames.Count; i++)
                    {
                        Collection.Add(new OlapServer(_store, serverNames[i]));
                    }
                }
                Invalid = false;
                IsInitialized = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the OlapServers class.
        /// </summary>
        /// <param name="store">The OlapStore that owns this collection.</param>
        public OlapServers(OlapStore store)
        {
            _store = store;
        }

        /// <summary>
        /// Destroys all servers and releases any used resources.
        /// </summary>
        /// <returns> </returns>
        ~OlapServers()
        {
            try
            {
                for (int i = 0; i < Collection.Count; i++)
                {
                    Collection[i] = null;
                }
                Collection = null;
            }
            catch (System.Exception e)
            {
                // TODO 10.5: Localize
                ApplicationContext.Log.Error(typeof(NativeOlapApi), (int)LogEvent.CouldNotDestroyOlapServer, "Error while destroying OLAP servers: " + e.Message);
            }
        }

        /// <summary>
        /// Gets the server at the specified index of the collection.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>The OlapServer or null, if the index was invalid.</returns>
        public OlapServer this[int index]
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
        /// Gets the server at the specified index of the collection.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <returns>The OlapServer or null, if the index was invalid.</returns>
        public OlapServer this[string name]
        {
            get
            {
                Load();
                for (int i = 0; i < Collection.Count; i++)
                {
                    OlapServer server = Collection[i];
                    if (name.ToUpper().Equals(server.Name.ToUpper()))
                    {
                        return server;
                    }
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
