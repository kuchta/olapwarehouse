using Infor.BI.Applications.OlapApi.Native;
using Infor.BI.Common.Application;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents an Olap server.
    /// </summary>
    public class OlapServer : OlapObjectBase, System.IDisposable
    {
        /// <summary>
        /// Holds the Olap store that owns the server.
        /// </summary>
        private OlapStore _store;

        /// <summary>
        /// Holds a collection of dimensions of the server.
        /// </summary>
        private OlapDimensions _dimensions;

        /// <summary>
        /// Holds a colelction of cubes of the server.
        /// </summary>
        private OlapCubes _cubes;

        /// <summary>
        /// Holds the setting of the server.
        /// </summary>
        private OlapServerInformation _settings;

        /// <summary>
        /// Holds the name of the server.
        /// </summary>
        private string _name;

        /// <summary>
        /// Holds the server handle.
        /// </summary>
        private int _serverHandle;

        /// <summary>
        /// Holds a flag that indicates whether a dataarea is currently active for a specific connection.
        /// </summary>
        private BoolPointer _dataareaActive;

        /// <summary>
        /// Holds the last error raised in this connection.
        /// </summary>
        private IntPointer _lastError;

        /// <summary>
        /// Holds a flag whether an object is disposed or not.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the OlapServer class.
        /// </summary>
        /// <param name="store">The Olap store that owns the server.</param>
        /// <param name="name">The name of the server.</param>
        public OlapServer(OlapStore store, string name)
        {
            _dataareaActive = new BoolPointer();
            _lastError = new IntPointer();
            _dimensions = null;
            _serverHandle = 0;
            _store = store;
            _name = name;
            _disposed = false;
        }

        /// <summary>
        /// Destroys an instance and closes any open connection if neccessary.
        /// </summary>
        public void Dispose()
        {
            try
            {
                Disconnect();
            }
            catch (System.Exception e)
            {
                // TODO 10.5: Localize
                ApplicationContext.Log.Error(typeof(NativeOlapApi), (int)LogEvent.CouldNotDestroyOlapServer, "Error while destroying OLAP servers: " + e.Message);
            }
        }

        /// <summary>
        /// Establishes a connection to the server.
        /// </summary>
        /// <param name="userName">The user name used to connect.</param>
        /// <param name="password">The password used to connect.</param>
        public void Connect(string userName, string password)
        {
            _serverHandle = NativeOlapApi.ServerConnect(_store.ClientSlot, _name, userName, password, _lastError);

            if (_serverHandle == 0)
            {
                throw new OlapException("Server connect failed!", _lastError.Value);
            }
        }

        /// <summary>
        /// Establishes a connection to the server using a COS ticket.
        /// </summary>
        /// <param name="ticket">The COS ticket to use.</param>
        public void ConnectTicket(string ticket)
        {
            _serverHandle = NativeOlapApi.ServerConnect(_store.ClientSlot, _name, ticket, _lastError);

            if (_serverHandle == 0)
            {
                throw new OlapException("Server connect failed!", _lastError.Value);
            }
        }

        /// <summary>
        /// Connects to the server using windows authentication.
        /// </summary>
        public void Connect()
        {
            _serverHandle = NativeOlapApi.ServerConnect(_store.ClientSlot, _name, _lastError);

            if (_serverHandle == 0)
            {
                throw new OlapException("Server connect failed!", _lastError.Value);
            }
        }

        /// <summary>
        /// Disconnects from the server.
        /// </summary>
        /// <returns>True, if disconnected; false, otherwise.</returns>
        public bool Disconnect()
        {
            bool result = NativeOlapApi.ServerDisconnect(_store.ClientSlot, _serverHandle, _lastError);
            _serverHandle = 0;
            _cubes = null;
            _dimensions = null;
            _settings = null;
            return result;
        }

        /// <summary>
        /// Saves all unsaved data of the server.
        /// </summary>
        /// <returns>True, if the data has been saved; false, otherwise.</returns>
        public bool Save()
        {
            return NativeOlapApi.ServerSave(_store.ClientSlot, _serverHandle, _lastError);
        }

        /// <summary>
        /// Refreshes the cubes and dimensions of the server.
        /// </summary>
        /// <returns>True, if the data has been saved; false, otherwise.</returns>
        public bool Refresh()
        {
            if (NativeOlapApi.ServerRefresh(_store.ClientSlot, _serverHandle, _lastError))
            {
                _cubes.Invalid = true;
                _dimensions.Invalid = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Save all dimension and cubes to disk and removes them from memory.
        /// </summary>
        /// <returns>True, if cleared; false, otherwise.</returns>
        public bool Clear()
        {
            return NativeOlapApi.ServerClear(_store.ClientSlot, _serverHandle, _lastError);
        }

        /// <summary>
        /// Executes an XML request. Before a request can be executed a dataarea must be defined and activated.
        /// </summary>
        /// <param name="request">The XML request to be executed.</param>
        /// <returns>The result XML returned by the OLAP server.</returns>
        public string XMLRequest(string request)
        {
            return NativeOlapApi.XMLRequest(_store.ClientSlot, _serverHandle, request);
        }

        /// <summary>
        /// Starts the OLAP load from source function.
        /// </summary>
        /// <returns>True, if successful; false, otherwise.</returns>
        public bool StartLoadFromSource()
        {
            return NativeOlapApi.StartLoadFromSource(_store.ClientSlot, _serverHandle, "1");
        }

        /// <summary>
        /// Gets the native name for a unique name.
        /// </summary>
        /// <param name="uniqueName">The unique name to convert.</param>
        /// <returns>The native name. If the unique name was invalid the result will be an empty string.</returns>
        public string ConvertUniqueNameToNative(string uniqueName)
        {
            return NativeOlapApi.ResolveUniqueName(_store.ClientSlot, _serverHandle, uniqueName);
        }

        /// <summary>
        /// Converts a double into a string using a unique formatting provided by the OLAP server.
        /// </summary>
        /// <param name="number">The number to convert.</param>
        /// <returns>A string containing the converted number. If an error occured the string is empty.</returns>
        public static string ConvertDoubleToString(double number)
        {
            string result = string.Empty;
            if (NativeOlapApi.ConvertDoubleToString(out result, number) == (int)ClientSupportErrorCodes.ECI_OK)
            {
                return result;
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the name of the server.
        /// </summary>
        /// <returns>The name of the server.</returns>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the Olap database name.
        /// </summary>
        public string DatabaseName
        {
            get
            {
                try
                {
                    return _name.Split('/')[1];
                }
                catch (System.Exception)
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the Olap server name.
        /// </summary>
        public string ServerName
        {
            get
            {
                try
                {
                    return _name.Split('/')[0];
                }
                catch (System.Exception)
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets additional information about the server.
        /// </summary>
        /// <returns>The information of the server.</returns>
        public OlapServerInformation ServerSettings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = NativeOlapApi.ServerInformation(_store.ClientSlot, _serverHandle, _lastError);
                }
                return _settings;
            }
        }

        /// <summary>
        /// Gets the server handle which will be required for further operations on the server.
        /// </summary>
        /// <returns> </returns>
        public int ServerHandle
        {
            get
            {
                return _serverHandle;
            }
        }

        /// <summary>
        /// Gets a collection of dimensions of the server.
        /// </summary>
        /// <returns> </returns>
        public OlapDimensions Dimensions
        {
            get
            {
                if (_dimensions == null)
                {
                    _dimensions = new OlapDimensions(this);
                }
                return _dimensions;
            }
        }

        /// <summary>
        /// Gets a collection of cubes of the server.
        /// </summary>
        /// <returns> </returns>
        public OlapCubes Cubes
        {
            get
            {
                if (_cubes == null)
                {
                    _cubes = new OlapCubes(this);
                }
                return _cubes;
            }
        }

        /// <summary>
        /// Gets the Olap store that owns the server.
        /// </summary>
        /// <returns> </returns>
        public OlapStore Store
        {
            get
            {
                return _store;
            }
        }

        /// <summary>
        /// Gets a reference to the dataarea acvtive flag for the specific connection.
        /// </summary>
        /// <returns> </returns>
        public BoolPointer DataAreaActive
        {
            get
            {
                return _dataareaActive;
            }
        }

        /// <summary>
        /// Gets the latest error occured on this connection.
        /// </summary>
        /// <returns> </returns>
        public int LastError
        {
            get
            {
                return _lastError.Value;
            }
        }

        /// <summary>
        /// Gets a reference to the last error raised for the specific connection.
        /// </summary>
        /// <returns> </returns>
        public IntPointer LastErrorInternal
        {
            get
            {
                return _lastError;
            }
        }
    }
}