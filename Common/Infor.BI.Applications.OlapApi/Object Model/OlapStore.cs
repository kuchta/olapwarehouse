using Infor.BI.Applications.OlapApi.Native;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// This class implements the entry point to the Olap object model for .Net implemented
    /// in this assembly.
    /// </summary>
    public class OlapStore : OlapObjectBase, System.IDisposable
    {
        /// <summary>
        /// Holds the client slot of the connect process.
        /// </summary>
        private int _clientSlot;

        /// <summary>
        /// Holds a flag whether an object is disposed or not.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the OlapStore class.
        /// </summary>
        /// <param name="userName">A user name to establish the client connection.</param>
        public OlapStore(string userName)
        {
            IntPointer lastError = new IntPointer();
            _clientSlot = NativeOlapApi.ClientConnect(userName, lastError);
            if (_clientSlot == 0)
            {
                throw new OlapException("Client connect failed!", lastError.Value);
            }
        }

        /// <summary>
        /// Destorys the object and frees its resources.
        /// </summary>
        public void Dispose()
        {
            try
            {
                FreeResources();
            }
            catch (System.Exception)
            {
            }
        }

        /// <summary>
        /// Gets the Olap servers currently available in the network and on the local machine..
        /// </summary>
        /// <returns> </returns>
        public OlapServers Servers
        {
            get
            {
                OlapServers servers = new OlapServers(this);
                return servers;
            }
        }

        /// <summary>
        /// Gets the client slot that was returned when connecting the client.
        /// </summary>
        /// <returns>The client slot (>0) if successful or 0 if not successful.</returns>
        public int ClientSlot
        {
            get
            {
                return _clientSlot;
            }
        }

        /// <summary>
        /// Gets an error description for a specific OLAP error code.
        /// </summary>
        /// <param name="errorCode">An OLAP error code.</param>
        /// <returns>The description for the error code.</returns>
        public string GetErrorDescription(int errorCode)
        {
            return NativeOlapApi.ErrorDescription(errorCode);
        }

        /// <summary>
        /// Frees all resources immediately.
        /// </summary>
        public void FreeResources()
        {
            if (!_disposed)
            {
                _disposed = true;
                if (ClientSlot != 0)
                {
                    IntPointer lastError = new IntPointer();
                    NativeOlapApi.ClientDisconnect(_clientSlot, lastError);
                    _clientSlot = 0;
                }
            }
        }
    }
}
