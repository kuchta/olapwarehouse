using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Infor.BI.Applications.OlapApi.Native
{
    /// <summary>
    /// Encapsulates the Olap C API. The functions are not mapped 1:1 as the C
    /// versions require ugly parameter styles and they make use of unions.
    /// This interface tries to leave the functions as original as possible but
    /// as object oriented as required to provide a stable interface.
    /// </summary>
    public class NativeOlapApi
    {
        /// <summary>
        /// Olap constant to remove table from memory.
        /// </summary>
        private static readonly uint _removeTableFromMemory = 1;

        private static readonly int _calculatedCells = ServerConstants.CsConsolidated | ServerConstants.CsGlobalRule | ServerConstants.CsLocalRule;

        private static short MakeWord(byte low, byte high)
        {
            return (short)(low | (high << 8));
        }

        private static int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }

        /// <summary>
        /// Connects the client and allocates a new slot.
        /// </summary>
        /// <param name="userName">The user name used to connect.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>The client slot which must be used with most operations. If the
        /// result is 0, the method failed.
        /// </returns>
        public static int ClientConnect(string userName, IntPointer lastError)
        {
            int[] error = new int[1];
            error[0] = 0;
            int result = OlapNativeImports.MdsConnect(userName, 1, error);
            lastError.Value = error[0];
            return result;
        }

        /// <summary>
        /// Connects to an Olap server using Olap server authentication.
        /// </summary>
        /// <param name="clientSlot">The client slot returned from Connect.</param>
        /// <param name="serverName">The name of the server to connect to.</param>
        /// <param name="userName">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A server handle which will be required for several further operations. If the
        /// handle is 0 the connection failed.</returns>
        public static int ServerConnect(int clientSlot, string serverName, string userName, string password, IntPointer lastError)
        {
            int[] error = new int[1];
            error[0] = 0;
            int result = OlapNativeImports.MdsConnectServerExtW(clientSlot, serverName, userName, password, 0, 0, error);
            lastError.Value = error[0];
            return result;
        }

        /// <summary>
        /// Connects to an Olap server using windows authentication.
        /// </summary>
        /// <param name="clientSlot">The client slot returned from Connect.</param>
        /// <param name="serverName">The name of the server to connect to.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A server handle which will be required for several further operations. If the
        /// handle is 0 the connection failed.</returns>
        public static int ServerConnect(int clientSlot, string serverName, IntPointer lastError)
        {
            int[] error = new int[1];
            int result = OlapNativeImports.MdsConnectServerWinW(clientSlot, serverName, 0, error);
            lastError.Value = error[0];
            return result;
        }

        /// <summary>
        /// Connects to an Olap server using a COS ticket.
        /// </summary>
        /// <param name="clientSlot">The client slot returned from Connect.</param>
        /// <param name="serverName">The name of the server to connect to.</param>
        /// <param name="ticket">The COS ticket to use.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A server handle which will be required for several further operations. If the
        /// handle is 0 the connection failed.</returns>
        public static int ServerConnect(int clientSlot, string serverName, string ticket, IntPointer lastError)
        {
            int[] error = new int[1];
            int result = OlapNativeImports.MdsConnectServerTicketW(clientSlot, serverName, ticket, 0, error);
            lastError.Value = error[0];
            return result;
        }

        /// <summary>
        /// Disconnects the client and releases the allocated client slot.
        /// </summary>
        /// <param name="clientSlot">The slot to release.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if successful; false, otherwise.</returns>
        public static bool ClientDisconnect(int clientSlot, IntPointer lastError)
        {
            int[] error = new int[1];
            int result = OlapNativeImports.MdsDisconnect(clientSlot, error);
            lastError.Value = error[0];
            return result == 1;
        }

        /// <summary>
        /// Disconnects from a server.
        /// </summary>
        /// <param name="clientSlot">The client slot used to establish the connection.</param>
        /// <param name="serverHandle">The server handle of the server to disconnect from.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if successful; false, otherwise.</returns>
        public static bool ServerDisconnect(int clientSlot, int serverHandle, IntPointer lastError)
        {
            int[] error = new int[1];
            int result = OlapNativeImports.MdsDisconnectServer(clientSlot, serverHandle, error);
            lastError.Value = error[0];
            return result == 1;
        }

        /// <summary>
        /// Gets the id of a server by its name.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverName">The name of the server.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A value greater 1, if successful; 0, otherwise.</returns>
        public static int ServerId(int clientSlot, string serverName, IntPointer lastError)
        {
            return 0;
        }

        /// <summary>
        /// Returns a collection of strings which holds the names of all servers that are currently
        /// available on the local machine and in the network.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A string collection instance with at least on element if successful. Null, otherwise.</returns>
        public static System.Collections.Specialized.StringCollection Servers(int clientSlot, IntPointer lastError)
        {
            int[] error = new int[1];
            System.Collections.Specialized.StringCollection servers = null;
            int count = OlapNativeImports.MdsServerCountEx(clientSlot, error);
            lastError.Value = error[0];

            if (count > 0)
            {
                servers = new System.Collections.Specialized.StringCollection();

                for (int i = 0; i < count; i++)
                {
                    OlapNativeServerInformation serverResult = new OlapNativeServerInformation();
                    int result = OlapNativeImports.MdsServerNameExW(clientSlot, ref serverResult, i + 1, error);
                    lastError.Value = error[0];
                    if (result != 0)
                    {
                        servers.Add(serverResult.ServerName);
                    }
                }
                if (servers.Count == 0)
                {
                    servers = null;
                }
            }

            return servers;
        }

        /// <summary>
        /// Returns a collection of strings which holds the names of all cubes of a specified server.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A string collection instance with at least on element if successful. Null, otherwise.</returns>
        public static System.Collections.Specialized.StringCollection Cubes(int clientSlot, int serverHandle, IntPointer lastError)
        {
            System.Collections.Specialized.StringCollection cubes = null;
            OlapNativeImports.CallBegin(serverHandle, UniFunction.GetTableCount);
            int error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;
            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                int numCubes = OlapNativeImports.GetCount(serverHandle);

                cubes = new System.Collections.Specialized.StringCollection();
                for (int i = 0; i < numCubes; i++)
                {
                    OlapNativeImports.CallBegin(serverHandle, UniFunction.GetTableInfo);
                    OlapNativeImports.SetId(serverHandle, i + 1);
                    error = OlapNativeImports.CallEnd(serverHandle);
                    if (error == (int)ClientSupportErrorCodes.ECI_OK)
                    {
                        cubes.Add(OlapNativeImports.GetName(serverHandle));
                    }
                    else
                    {
                        return null;
                    }
                    lastError.Value = error;
                }
                if (cubes.Count == 0)
                {
                    cubes = null;
                }
            }

            return cubes;
        }

        /// <summary>
        /// Returns a collection of strings which holds the names of all dimensions of a specified server.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A string collection instance with at least on element if successful. Null, otherwise.</returns>
        public static System.Collections.Specialized.StringCollection Dimensions(int clientSlot, int serverHandle, IntPointer lastError)
        {
            System.Collections.Specialized.StringCollection dimensions = null;
            int error = 0;
            OlapNativeImports.CallBegin(serverHandle, UniFunction.GetDimensionCount);
            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;
            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                int numCubes = OlapNativeImports.GetCount(serverHandle);
                dimensions = new System.Collections.Specialized.StringCollection();
                for (int i = 0; i < numCubes; i++)
                {
                    OlapNativeImports.CallBegin(serverHandle, UniFunction.GetDimensionInfo);
                    OlapNativeImports.SetId(serverHandle, i + 1);
                    error = OlapNativeImports.CallEnd(serverHandle);
                    if (error == (int)ClientSupportErrorCodes.ECI_OK)
                    {
                        dimensions.Add(OlapNativeImports.GetName(serverHandle));
                    }
                    lastError.Value = error;
                }
                if (dimensions.Count == 0)
                {
                    dimensions = null;
                }
            }

            return dimensions;
        }

        /// <summary>
        /// Returns a collection of strings which holds the names of all dimensions of a specified cube on a specified server.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of a cube on the server.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A string collection instance with at least on element if successful. Null, otherwise.</returns>
        public static System.Collections.Specialized.StringCollection CubeDimensions(int clientSlot, int serverHandle, string cubeName, IntPointer lastError)
        {
            System.Collections.Specialized.StringCollection dimensions = null;

            OlapNativeImports.CallBegin(serverHandle, UniFunction.GetTableInfo);
            OlapNativeImports.SetName(serverHandle, cubeName);
            int error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;

            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                int dimensionCount = OlapNativeImports.GetDimensionCount(serverHandle);
                if (dimensionCount > 0)
                {
                    dimensions = new System.Collections.Specialized.StringCollection();

                    for (int i = 0; i < dimensionCount; i++)
                    {
                        dimensions.Add(OlapNativeImports.GetDimension(serverHandle));
                    }
                }
            }
            return dimensions;
        }

        /// <summary>
        /// Returns a collection of strings which holds the names of all elements of a specified dimension on a specified server.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimension">The name of the dimension of which to get the elements.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A string collection instance with at least on element if successful. Null, otherwise.</returns>
        public static System.Collections.Specialized.StringCollection DimensionElements(int clientSlot, int serverHandle, string dimension, IntPointer lastError)
        {
            System.Collections.Specialized.StringCollection elements = null;
            OlapNativeImports.CallBegin(serverHandle, UniFunction.GetDimensionInfo);
            OlapNativeImports.SetName(serverHandle, dimension);
            int error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;
            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                elements = new System.Collections.Specialized.StringCollection();
                int elemCount = OlapNativeImports.GetElementsCount(serverHandle);

                for (int i = 0; i < elemCount; i++)
                {
                    OlapNativeImports.CallBegin(serverHandle, UniFunction.GetElementInfo);
                    OlapNativeImports.SetDimension(serverHandle, dimension);
                    OlapNativeImports.SetId(serverHandle, i + 1);
                    error = OlapNativeImports.CallEnd(serverHandle);

                    if (error == (int)ClientSupportErrorCodes.ECI_OK)
                    {
                        if ((OlapNativeImports.GetFlags(serverHandle) & ServerConstants.MdsEtNoRightsLong) == 0)
                        {
                            elements.Add(OlapNativeImports.GetName(serverHandle));
                        }
                    }
                    lastError.Value = error;
                }
                if (elements.Count == 0)
                {
                    elements = null;
                }
            }
            return elements;
        }

        /// <summary>
        /// Returns a collection of strings which holds the names of all top-elements of a specified dimension on a specified server.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimension">The name of the dimension of which to get the elements.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A string collection instance with at least on element if successful. Null, otherwise.</returns>
        public static System.Collections.Specialized.StringCollection DimensionTopLevelElements(int clientSlot, int serverHandle, string dimension, IntPointer lastError)
        {
            System.Collections.Specialized.StringCollection elements = null;
            OlapNativeImports.CallBegin(serverHandle, UniFunction.GetDimensionInfo);
            OlapNativeImports.SetName(serverHandle, dimension);
            int error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;
            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                elements = new System.Collections.Specialized.StringCollection();
                int elemCount = OlapNativeImports.GetElementsCount(serverHandle);

                for (int i = 0; i < elemCount; i++)
                {
                    OlapNativeImports.CallBegin(serverHandle, UniFunction.GetElementInfo);
                    OlapNativeImports.SetDimension(serverHandle, dimension);
                    OlapNativeImports.SetId(serverHandle, i + 1);
                    error = OlapNativeImports.CallEnd(serverHandle);
                    if (error == (int)ClientSupportErrorCodes.ECI_OK)
                    {
                        if (OlapNativeImports.GetParentsCount(serverHandle) == 0)
                        {
                            if ((OlapNativeImports.GetFlags(serverHandle) & ServerConstants.MdsEtNoRightsLong) == 0)
                            {
                                elements.Add(OlapNativeImports.GetName(serverHandle));
                            }
                        }
                    }
                    lastError.Value = error;
                }
                if (elements.Count == 0)
                {
                    elements = null;
                }
            }
            return elements;
        }

        /// <summary>
        /// Gets the direct children of an element.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimension">The name of the dimension of which to get the elements.</param>
        /// <param name="elementName">The element which children to get.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A string collection with the element names.</returns>
        public static System.Collections.Specialized.StringCollection DimensionElementChildren(int clientSlot, int serverHandle, string dimension, string elementName, IntPointer lastError)
        {
            int error = 0;
            OlapElementInformation info = DimensionElementInformation(clientSlot, serverHandle, dimension, elementName, lastError);
            System.Collections.Specialized.StringCollection elements = null;

            if (info != null)
            {
                elements = new System.Collections.Specialized.StringCollection();
                for (int i = 0; i < info.ChildCount; i++)
                {
                    OlapNativeImports.CallBegin(serverHandle, UniFunction.GetSubElem);
                    OlapNativeImports.SetDimension(serverHandle, dimension);
                    OlapNativeImports.SetElement(serverHandle, elementName);
                    OlapNativeImports.SetId(serverHandle, i + 1); // Call IpoSetId or IpoSetName
                    error = OlapNativeImports.CallEnd(serverHandle);
                    lastError.Value = error;
                    if (error == (int)ClientSupportErrorCodes.ECI_OK)
                    {
                        int flags = OlapNativeImports.GetFlags(serverHandle);
                        if ((flags & ServerConstants.MdsEtNoRightsLong) == 0)
                        {
                            elements.Add(OlapNativeImports.GetName(serverHandle));
                        }
                    }
                }
            }
            return elements;
        }

        /// <summary>
        /// Gets all direct children of an element ignoring permissions.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimension">The name of the dimension of which to get the elements.</param>
        /// <param name="elementName">The element which children to get.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A string collection with the element names.</returns>
        public static System.Collections.Generic.Dictionary<string, bool> DimensionElementAllChildren(int clientSlot, int serverHandle, string dimension, string elementName, IntPointer lastError)
        {
            int error = 0;
            OlapElementInformation info = DimensionElementInformation(clientSlot, serverHandle, dimension, elementName, lastError);
            System.Collections.Generic.Dictionary<string, bool> elements = null;

            if (info != null)
            {
                elements = new System.Collections.Generic.Dictionary<string, bool>();
                for (int i = 0; i < info.ChildCount; i++)
                {
                    OlapNativeImports.CallBegin(serverHandle, UniFunction.GetSubElem);
                    OlapNativeImports.SetDimension(serverHandle, dimension);
                    OlapNativeImports.SetElement(serverHandle, elementName);
                    OlapNativeImports.SetId(serverHandle, i + 1); // Call IpoSetId or IpoSetName
                    error = OlapNativeImports.CallEnd(serverHandle);
                    lastError.Value = error;
                    if (error == (int)ClientSupportErrorCodes.ECI_OK)
                    {
                        int flags = OlapNativeImports.GetFlags(serverHandle);
                        bool permission = (flags & ServerConstants.MdsEtNoRightsLong) == 0;
                        elements.Add(OlapNativeImports.GetName(serverHandle), permission);
                    }
                }
            }
            return elements;
        }

        /// <summary>
        /// Gets the direct parents of an element.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimension">The name of the dimension of which to get the elements.</param>
        /// <param name="elementName">The element which children to get.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A string collection with the element names.</returns>
        public static System.Collections.Specialized.StringCollection DimensionElementParents(int clientSlot, int serverHandle, string dimension, string elementName, IntPointer lastError)
        {
            int error = 0;
            OlapElementInformation info = DimensionElementInformation(clientSlot, serverHandle, dimension, elementName, lastError);
            System.Collections.Specialized.StringCollection elements = null;

            if (info != null)
            {
                elements = new System.Collections.Specialized.StringCollection();
                for (int i = 0; i < info.ParentCount; i++)
                {
                    OlapNativeImports.CallBegin(serverHandle, UniFunction.GetParentElem);
                    OlapNativeImports.SetDimension(serverHandle, dimension);
                    OlapNativeImports.SetElement(serverHandle, elementName);
                    OlapNativeImports.SetId(serverHandle, i + 1); // Call IpoSetId or IpoSetName
                    error = OlapNativeImports.CallEnd(serverHandle);
                    lastError.Value = error;
                    if (error == (int)ClientSupportErrorCodes.ECI_OK)
                    {
                        int flags = OlapNativeImports.GetFlags(serverHandle);
                        if ((flags & ServerConstants.MdsEtNoRightsLong) == 0)
                        {
                            elements.Add(OlapNativeImports.GetName(serverHandle));
                        }
                    }
                }
            }
            return elements;
        }

        /// <summary>
        /// Returns the information about the specified server.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>An instance of the OlapServerInformation class with the server settings.
        /// Null, if an error occured.</returns>
        public static OlapServerInformation ServerInformation(int clientSlot, int serverHandle, IntPointer lastError)
        {
            OlapServerInformation serverSettings = null;
            int error = 0;
            OlapNativeImports.CallBegin(serverHandle, UniFunction.GetSettings);
            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;
            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                string ver = OlapNativeImports.GetVersion(serverHandle); // string with format info "Version.Release.Subrelease.Build" e.g."10.1.0.231"
                string[] verArr = ver.Split('.');
                int v1 = 0;
                int v2 = 0;
                int v3 = 0;
                int v4 = 0;

                if (verArr != null)
                {
                    if (verArr.Length == 4)
                    {
                        v1 = System.Convert.ToInt32(verArr[0]);
                        v2 = System.Convert.ToInt32(verArr[1]);
                        v3 = System.Convert.ToInt32(verArr[2]);
                        v4 = System.Convert.ToInt32(verArr[3]);
                    }
                }
                OlapServerMode mode = OlapServerMode.OlapServerModeWinNT;
                serverSettings = new OlapServerInformation(v1, v2, v3, v4, 0, 0, 0, mode);
            }
            return serverSettings;
        }

        /// <summary>
        /// Gets additional information about a cube.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of the cube to get the information for.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>An instance of the OlapCubeInformation class. If any error occurs the return value is null.</returns>
        public static OlapCubeInformation CubeInformation(int clientSlot, int serverHandle, string cubeName, IntPointer lastError)
        {
            OlapCubeInformation cubeInfo = null;
            int error = 0;

            OlapNativeImports.CallBegin(serverHandle, UniFunction.GetTableInfo);
            OlapNativeImports.SetName(serverHandle, cubeName);
            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;

            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                char type = OlapNativeImports.GetCType(serverHandle);
                int lastChange = OlapNativeImports.GetStamp(serverHandle);
                int baseValues = OlapNativeImports.GetValuesCount(serverHandle);
                int calculatedValues = OlapNativeImports.GetCachedValuesCount(serverHandle);

                OlapCubeType cubeType;
                switch (type)
                {
                    case 'S':
                        cubeType = OlapCubeType.OlapCubeTypeSystem;
                        break;

                    case 'U':
                        cubeType = OlapCubeType.OlapCubeTypeUser;
                        break;

                    case 'A':
                        cubeType = OlapCubeType.OlapCubeTypeCubeAccesControl;
                        break;

                    case 'D':
                        cubeType = OlapCubeType.OlapCubeTypeDimensionAccessControl;
                        break;
                    default:
                        throw new OlapException("Unknown cube type: " + type);
                }

                cubeInfo = new OlapCubeInformation(cubeType, lastChange, baseValues, calculatedValues);
            }

            return cubeInfo;
        }

        /// <summary>
        /// Gets additional information about a dimension.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimensionName">The name of the dimension to get the information for.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>An instance of the OlapDimensionInformation class. If any error occurs the return value is null.</returns>
        public static OlapDimensionInformation DimensionInformation(int clientSlot, int serverHandle, string dimensionName, IntPointer lastError)
        {
            OlapDimensionInformation dimensionInfo = null;
            int error = 0;
            OlapNativeImports.CallBegin(serverHandle, UniFunction.GetDimensionInfo);
            OlapNativeImports.SetName(serverHandle, dimensionName);
            lastError.Value = error = OlapNativeImports.CallEnd(serverHandle);

            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                string accctl = OlapNativeImports.GetDART(serverHandle);
                string longName = OlapNativeImports.GetLongName(serverHandle);
                int stamp = OlapNativeImports.GetStamp(serverHandle);
                dimensionInfo = new OlapDimensionInformation(stamp, accctl, longName);
            }

            return dimensionInfo;
        }

        /// <summary>
        /// Gets additional information about an element.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimensionName">The name of the dimension that has the element.</param>
        /// <param name="elementName">The name of the element to get the information for.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>An instance of the OlapElementInformation class. If any error occurs the return value is null.</returns>
        public static OlapElementInformation DimensionElementInformation(int clientSlot, int serverHandle, string dimensionName, string elementName, IntPointer lastError)
        {
            OlapElementInformation elementInformation = null;
            int error = 0;
            OlapNativeImports.CallBegin(serverHandle, UniFunction.GetElementInfo);
            OlapNativeImports.SetDimension(serverHandle, dimensionName);
            OlapNativeImports.SetName(serverHandle, elementName);
            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;

            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                OlapElementType elementType;
                char ctype = OlapNativeImports.GetCType(serverHandle);   // 'N'
                int childcount = OlapNativeImports.GetCount(serverHandle);  // Children count - 0
                int parentcount = OlapNativeImports.GetParentsCount(serverHandle);  // parens count

                switch (ctype)
                {
                    case 'N':
                        elementType = OlapElementType.OlapElementTypeBase;
                        break;

                    case 'C':
                        elementType = OlapElementType.OlapElementTypeConsolidation;
                        break;

                    case 'S':
                        elementType = OlapElementType.OlapElementTypeText;
                        break;

                    case 'R':
                        elementType = OlapElementType.OlapElementTypeRule;
                        break;
                    default:
                        throw new OlapException("Unknown element type: " + ctype);
                }

                elementInformation = new OlapElementInformation(elementType, parentcount, childcount);
            }
            return elementInformation;
        }

        /// <summary>
        /// Saves all tables, dimensions, users, group names, etc. (all that is kept in memory) to disk.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if saved; false, otherwise.</returns>
        public static bool ServerSave(int clientSlot, int serverHandle, IntPointer lastError)
        {
            OlapNativeImports.CallBegin(serverHandle, UniFunction.SaveKernel);
            lastError.Value = OlapNativeImports.CallEnd(serverHandle);
            return lastError.Value == (int)ClientSupportErrorCodes.ECI_OK;
        }

        /// <summary>
        /// Refreshes the dimensions and cubes of a server.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if refreshed; false, otherwise.</returns>
        public static bool ServerRefresh(int clientSlot, int serverHandle, IntPointer lastError)
        {
            OlapNativeImports.CallBegin(serverHandle, UniFunction.RefreshLists);
            lastError.Value = OlapNativeImports.CallEnd(serverHandle);
            return lastError.Value == (int)ClientSupportErrorCodes.ECI_OK;
        }

        /// <summary>
        /// Saves all cubes and dimensions and removes them from memory.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if cleared; false, otherwise.</returns>
        public static bool ServerClear(int clientSlot, int serverHandle, IntPointer lastError)
        {
            OlapNativeImports.CallBegin(serverHandle, UniFunction.AllMemRemove);
            OlapNativeImports.SetFlags(serverHandle, (int)_removeTableFromMemory);
            lastError.Value = OlapNativeImports.CallEnd(serverHandle);
            return lastError.Value == (int)ClientSupportErrorCodes.ECI_OK;
        }

        /// <summary>
        /// Saves the specified cube to disk.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of the cube to save.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if saved; false, otherwise.</returns>
        public static bool CubeSave(int clientSlot, int serverHandle, string cubeName, IntPointer lastError)
        {
            OlapNativeImports.CallBegin(serverHandle, UniFunction.SaveTable);
            OlapNativeImports.SetName(serverHandle, cubeName);
            lastError.Value = OlapNativeImports.CallEnd(serverHandle);
            return lastError.Value == (int)ClientSupportErrorCodes.ECI_OK;
        }

        /// <summary>
        /// Refreshes a cube.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of the cube to refresh.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if refreshed; false, otherwise.</returns>
        public static bool CubeRefresh(int clientSlot, int serverHandle, string cubeName, IntPointer lastError)
        {
            return true;
        }

        /// <summary>
        /// Saves the cube to disc an removes it from memory (not from disk).
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of the cube.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if cleared; false, otherwise.</returns>
        public static bool CubeClear(int clientSlot, int serverHandle, string cubeName, IntPointer lastError)
        {
            OlapNativeImports.CallBegin(serverHandle, UniFunction.TableMemRemove);
            OlapNativeImports.SetName(serverHandle, cubeName);
            OlapNativeImports.SetParam(serverHandle, _removeTableFromMemory);
            lastError.Value = OlapNativeImports.CallEnd(serverHandle);
            return lastError.Value == (int)ClientSupportErrorCodes.ECI_OK;
        }

        /// <summary>
        /// Gets the cell value of the specified cell.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of a cube.</param>
        /// <param name="elements">A collection of strings representing the address of the cell.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>The cell value or null, if the cell value could not be retrieved.</returns>
        public static object CubeGetCell(int clientSlot, int serverHandle, string cubeName, System.Collections.Specialized.StringCollection elements, IntPointer lastError)
        {
            int error = 0;

            OlapNativeImports.CallBegin(serverHandle, UniFunction.GetCell);
            OlapNativeImports.SetCube(serverHandle, cubeName);

            for (int i = 0; i < elements.Count; i++)
            {
                OlapNativeImports.SetElement(serverHandle, elements[i]);
            }

            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;

            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                int flags = OlapNativeImports.GetFlags(serverHandle);

                if ((flags & ServerConstants.CsString) == ServerConstants.CsString)
                {
                    return OlapNativeImports.GetSValue(serverHandle);
                }
                else if ((flags & ServerConstants.CsMissing) == ServerConstants.CsMissing)
                {
                    return null;
                }
                else
                {
                    return OlapNativeImports.GetNValue(serverHandle);
                }
            }
            else
            {
                System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CurrentUICulture;
                string lc = ci.TwoLetterISOLanguageName.ToLower();
                string coords = string.Empty;

                for (int i = 0; i < elements.Count; i++)
                {
                    coords = string.Concat(coords, elements[i]);
                    if (i < elements.Count - 1)
                    {
                        coords = string.Concat(coords, ", ");
                    }
                }

                if (lc.Equals("de"))
                {
                    throw new OlapException(string.Format("OLAP Server: Der Wert konnte nicht gelesen werden. Der zurückgegeben Fehler-Code war {0}. Die Koordinaten sind: {1}", error.ToString(), coords), error);
                }
                else if (lc.Equals("es"))
                {
                    throw new OlapException(string.Format("OLAP Server: Could not read value. The error code returned from OLAP was {0}. The coordinates are: {1}", error.ToString(), coords), error);
                }
                else
                {
                    throw new OlapException(string.Format("OLAP Server: Could not read value. The error code returned from OLAP was {0}. The coordinates are: {1}", error.ToString(), coords), error);
                }
            }
        }

        /// <summary>
        /// Gets the cell value of the specified cell.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of a cube.</param>
        /// <param name="first">First dimenstion of the cell.</param>
        /// <param name="second">Second dimension of the cell.</param>
        /// <param name="elements">An array of strings representing the address of the cell.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>The cell value or null, if the cell value could not be retrieved.</returns>
        public static object CubeGetCell(int clientSlot, int serverHandle, string cubeName, string first, string second, string[] elements, IntPointer lastError)
        {
            int error = 0;
            OlapNativeImports.CallBegin(serverHandle, UniFunction.GetCell);
            OlapNativeImports.SetCube(serverHandle, cubeName);

            OlapNativeImports.SetElement(serverHandle, first);
            OlapNativeImports.SetElement(serverHandle, second);

            if (elements != null)
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    OlapNativeImports.SetElement(serverHandle, elements[i]);
                }
            }

            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;

            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                int flags = OlapNativeImports.GetFlags(serverHandle);

                if ((flags & ServerConstants.CsString) == ServerConstants.CsString)
                {
                    return OlapNativeImports.GetSValue(serverHandle);
                }
                else if ((flags & ServerConstants.CsMissing) == ServerConstants.CsMissing)
                {
                    return null;
                }
                else
                {
                    return OlapNativeImports.GetNValue(serverHandle);
                }
            }
            else
            {
                System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CurrentUICulture;
                string lc = ci.TwoLetterISOLanguageName.ToLower();
                string coords = string.Empty;
                coords = string.Concat(coords, first);
                coords = string.Concat(coords, ", ");
                coords = string.Concat(coords, second);
                coords = string.Concat(coords, ", ");

                for (int i = 0; i < elements.Length; i++)
                {
                    coords = string.Concat(coords, elements[i]);
                    if (i < elements.Length - 1)
                    {
                        coords = string.Concat(coords, ", ");
                    }
                }

                if (lc.Equals("de"))
                {
                    throw new OlapException(string.Format("OLAP Server: Der Wert konnte nicht gelesen werden. Der zurückgegeben Fehler-Code war {0}. Die Koordinaten sind: {1}", error.ToString(), coords), error);
                }
                else if (lc.Equals("es"))
                {
                    throw new OlapException(string.Format("OLAP Server: Could not read value. The error code returned from OLAP was {0}. The coordinates are: {1}", error.ToString(), coords), error);
                }
                else
                {
                    throw new OlapException(string.Format("OLAP Server: Could not read value. The error code returned from OLAP was {0}. The coordinates are: {1}", error.ToString(), coords), error);
                }
            }
        }

        /// <summary>
        /// Gets the cell comment of the specified cell.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of a cube.</param>
        /// <param name="elements">A collection of strings representing the address of the cell.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>The cell comment or null, if the cell comment could not be retrieved.</returns>
        public static string CubeGetCellComment(int clientSlot, int serverHandle, string cubeName, System.Collections.Specialized.StringCollection elements, IntPointer lastError)
        {
            int error = 0;
            OlapNativeImports.CallBegin(serverHandle, UniFunction.GetCommentText);
            OlapNativeImports.SetCube(serverHandle, cubeName);

            for (int i = 0; i < elements.Count; i++)
            {
                OlapNativeImports.SetElement(serverHandle, elements[i]); // call sequence in the same order as diimensions are defined
            }

            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;

            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                return OlapNativeImports.GetSValue(serverHandle);
            }
            else
            {
                throw new OlapException("OlapApi::CubeGetCellComment", error);
            }
        }

        /// <summary>
        /// Puts the cell value of the specified cell.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of a cube.</param>
        /// <param name="elements">A collection of strings representing the address of the cell.</param>
        /// <param name="value">The cell value to write.</param>
        /// <param name="additive">If true it increments the value instead of writeback.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if the value has been written; false, otherwise.</returns>
        public static bool CubePutCell(int clientSlot, int serverHandle, string cubeName, System.Collections.Specialized.StringCollection elements, object value, bool additive, IntPointer lastError)
        {
            if (value == null)
            {
                return CubeDeleteCell(clientSlot, serverHandle, cubeName, elements, lastError);
            }
            else
            {
                int error = 0;

                OlapNativeImports.CallBegin(serverHandle, UniFunction.PutCell);
                OlapNativeImports.SetCube(serverHandle, cubeName);
                if (additive)
                {
                    OlapNativeImports.SetFlags(serverHandle, ServerConstants.PcUser | ServerConstants.PcAdditive);
                }
                else
                {
                    OlapNativeImports.SetFlags(serverHandle, ServerConstants.PcUser);
                }

                for (int i = 0; i < elements.Count; i++)
                {
                    OlapNativeImports.SetElement(serverHandle, elements[i]); // call sequence in the same order as diimensions are defined
                }

                if (value.GetType() == typeof(double))
                {
                    OlapNativeImports.SetNValue(serverHandle, (double)value);
                }
                else if (value.GetType() == typeof(int))
                {
                    OlapNativeImports.SetNValue(serverHandle, (int)value);
                }
                else if (value.GetType() == typeof(uint))
                {
                    OlapNativeImports.SetNValue(serverHandle, (uint)value);
                }
                else if (value.GetType() == typeof(string))
                {
                    OlapNativeImports.SetSValue(serverHandle, (string)value);
                }

                lastError.Value = error = OlapNativeImports.CallEnd(serverHandle);

                if (error == (int)ClientSupportErrorCodes.ECI_OK)
                {
                    return error == (int)ClientSupportErrorCodes.ECI_OK;
                }
                else
                {
                    System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CurrentUICulture;
                    string lc = ci.TwoLetterISOLanguageName.ToLower();

                    if (error == (int)ServerErrorCodes.KE_NO_RIGHTS)
                    {
                        if (lc.Equals("de"))
                        {
                            throw new OlapException("OLAP Server: Der Wert konnte nicht geschrieben werden. Sie besitzen keine Schreib-Rechte.", error);
                        }
                        else if (lc.Equals("es"))
                        {
                            throw new OlapException("OLAP Server: Could not write value. You do not have write permissions.", error);
                        }
                        else
                        {
                            throw new OlapException("OLAP Server: Could not write value. You do not have write permissions.", error);
                        }
                    }
                    else
                    {
                        string coords = string.Empty;
                        for (int i = 0; i < elements.Count; i++)
                        {
                            coords = string.Concat(coords, elements[i]);
                            if (i < elements.Count - 1)
                            {
                                coords = string.Concat(coords, ", ");
                            }
                        }

                        if (lc.Equals("de"))
                        {
                            throw new OlapException(string.Format("OLAP Server: Der Wert konnte nicht geschrieben werden. Der zurückgegeben Fehler-Code war {0}. Die Koordinaten sind: {1}", error.ToString(), coords), error);
                        }
                        else if (lc.Equals("es"))
                        {
                            throw new OlapException(string.Format("OLAP Server: Could not write value. The error code returned from OLAP was {0}. The coordinates are: {1}", error.ToString(), coords), error);
                        }
                        else
                        {
                            throw new OlapException(string.Format("OLAP Server: Could not write value. The error code returned from OLAP was {0}. The coordinates are: {1}", error.ToString(), coords), error);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Puts the cell comments to the specified cell.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of a cube.</param>
        /// <param name="elements">A collection of strings representing the address of the cell.</param>
        /// <param name="comment">The cell comment to write.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if the value has been written; false, otherwise.</returns>
        public static bool CubePutCellComment(int clientSlot, int serverHandle, string cubeName, System.Collections.Specialized.StringCollection elements, string comment, IntPointer lastError)
        {
            if (comment == null)
            {
                return CubeDeleteCellComment(clientSlot, serverHandle, cubeName, elements, lastError);
            }
            else
            {
                int error = 0;
                OlapNativeImports.CallBegin(serverHandle, UniFunction.SetCommentText);
                OlapNativeImports.SetCube(serverHandle, cubeName);

                for (int i = 0; i < elements.Count; i++)
                {
                    OlapNativeImports.SetElement(serverHandle, elements[i]); // call sequence in the same order as diimensions are defined
                }

                OlapNativeImports.SetSValue(serverHandle, comment);
                lastError.Value = error = OlapNativeImports.CallEnd(serverHandle);

                if (error == (int)ClientSupportErrorCodes.ECI_OK)
                {
                    return error == (int)ClientSupportErrorCodes.ECI_OK;
                }
                else
                {
                    throw new OlapException("OlapApi::CubePutCellComment", error);
                }
            }
        }

        /// <summary>
        /// Deletes the cell.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of a cube.</param>
        /// <param name="elements">A collection of strings representing the address of the cell.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if the value has been deleted; false, otherwise.</returns>
        public static bool CubeDeleteCell(int clientSlot, int serverHandle, string cubeName, System.Collections.Specialized.StringCollection elements, IntPointer lastError)
        {
            int error = 0;

            OlapNativeImports.CallBegin(serverHandle, UniFunction.DeleteCell);
            OlapNativeImports.SetCube(serverHandle, cubeName);
            OlapNativeImports.SetFlags(serverHandle, ServerConstants.PcUser);

            for (int i = 0; i < elements.Count; i++)
            {
                OlapNativeImports.SetElement(serverHandle, elements[i]);
            }

            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;

            if (error == (int)ClientSupportErrorCodes.ECI_OK || error == 66)
            {
                return true;
            }

            throw new OlapException("OlapApi::CubeDeleteCell", error);
        }

        /// <summary>
        /// Deletes the comment of a cell.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of a cube.</param>
        /// <param name="elements">A collection of strings representing the address of the cell.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if the comment has been deleted; false, otherwise.</returns>
        public static bool CubeDeleteCellComment(int clientSlot, int serverHandle, string cubeName, System.Collections.Specialized.StringCollection elements, IntPointer lastError)
        {
            int error = 0;
            OlapNativeImports.CallBegin(serverHandle, UniFunction.DelCommentText);
            OlapNativeImports.SetCube(serverHandle, cubeName);

            for (int i = 0; i < elements.Count; i++)
            {
                OlapNativeImports.SetElement(serverHandle, elements[i]);
            }

            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;

            if (error == (int)ClientSupportErrorCodes.ECI_OK || error == 66)
            {
                return true;
            }

            throw new OlapException("OlapApi::CubeDeleteCellComment", error);
        }

        /// <summary>
        /// Gets the weight (factor) of a child element related to its parent.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimensionName">The name of the dimension which owns both elements.</param>
        /// <param name="parentName">The name of the parent element.</param>
        /// <param name="childName">The name of the child element.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A value that represents the weight.</returns>
        public static double DimensionElementWeight(int clientSlot, int serverHandle, string dimensionName, string parentName, string childName, IntPointer lastError)
        {
            int error = 0;
            OlapElementInformation info = DimensionElementInformation(clientSlot, serverHandle, dimensionName, parentName, lastError);
            System.Collections.Specialized.StringCollection elements = null;
            double weight = 0.0;

            if (info != null)
            {
                elements = new System.Collections.Specialized.StringCollection();
                OlapNativeImports.CallBegin(serverHandle, UniFunction.GetSubElem);
                OlapNativeImports.SetDimension(serverHandle, dimensionName);
                OlapNativeImports.SetElement(serverHandle, parentName);
                OlapNativeImports.SetName(serverHandle, childName);
                lastError.Value = error = OlapNativeImports.CallEnd(serverHandle);

                if (error == (int)ClientSupportErrorCodes.ECI_OK)
                {
                    weight = OlapNativeImports.GetNValue(serverHandle); // weight
                }
            }
            return weight;
        }

        /// <summary>
        /// Gets the attribute tables for the specified dimension.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimensionName">The name of the dimension for which to get the attribute tables.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>An ArrayList that holds instances of the OlapAttributeTableDefintion class.</returns>
        public static System.Collections.ArrayList AttributeTables(int clientSlot, int serverHandle, string dimensionName, IntPointer lastError)
        {
            System.Collections.ArrayList tables = new System.Collections.ArrayList();

            int error = 0;

            for (int i = 1; i < 10; i++)
            {
                OlapNativeImports.CallBegin(serverHandle, UniFunction.AttrGetInfo);
                OlapNativeImports.SetDimension(serverHandle, dimensionName);
                OlapNativeImports.SetId(serverHandle, i);
                error = OlapNativeImports.CallEnd(serverHandle);
                lastError.Value = error;
                if (error == (int)ClientSupportErrorCodes.ECI_OK)
                {
                    OlapAttributeTableDefintion tableDef = new OlapAttributeTableDefintion();
                    tableDef.Id = i;
                    tableDef.TimeStamp = OlapNativeImports.GetStamp(serverHandle);
                    tableDef.RecordCount = OlapNativeImports.GetCount(serverHandle);
                    tableDef.FieldCount = OlapNativeImports.GetFieldsCount(serverHandle);
                    tableDef.Name = OlapNativeImports.GetLongName(serverHandle);
                    tables.Add(tableDef);
                }
                else
                {
                    // TODO 10.5: CBA/WKO -> pls. check if this works correct with TNG
                    // tables->Add(0);
                    break;
                }
            }
            if (tables.Count == 0)
            {
                tables = null;
            }

            return tables;
        }

        /// <summary>
        /// Gets the attribute table fields of the specified table and dimension.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimensionName">The name of the dimension for which to get the attribute table's fields.</param>
        /// <param name="tableId">The id of the attribute table.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>An ArrayList that holds instances of the OlapAttributeTableFieldDefinition struct.</returns>
        public static System.Collections.ArrayList AttributeTableFields(int clientSlot, int serverHandle, string dimensionName, int tableId, IntPointer lastError)
        {
            System.Collections.ArrayList fields = new System.Collections.ArrayList();
            int error = 0;
            OlapNativeImports.CallBegin(serverHandle, UniFunction.AttrGetInfo);
            OlapNativeImports.SetDimension(serverHandle, dimensionName);
            OlapNativeImports.SetId(serverHandle, tableId);
            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;
            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                int count = OlapNativeImports.GetFieldsCount(serverHandle);

                for (int i = 0; i < count; i++)
                {
                    OlapNativeImports.CallBegin(serverHandle, UniFunction.AttrGetFieldInfo);
                    OlapNativeImports.SetDimension(serverHandle, dimensionName);
                    OlapNativeImports.SetTableId(serverHandle, tableId);
                    OlapNativeImports.SetId(serverHandle, i + 1);
                    error = OlapNativeImports.CallEnd(serverHandle);
                    if (error == (int)ClientSupportErrorCodes.ECI_OK)
                    {
                        OlapAttributeTableFieldDefinition field = new OlapAttributeTableFieldDefinition();
                        field.Id = i + 1;
                        field.Type = OlapNativeImports.GetCType(serverHandle);
                        field.FieldName = OlapNativeImports.GetName(serverHandle);
                        field.FieldWidth = OlapNativeImports.GetSize(serverHandle);
                        field.Decimals = OlapNativeImports.GetDecimalPlaces(serverHandle);
                        fields.Add(field);
                    }
                    lastError.Value = error;
                }
                if (fields.Count == 0)
                {
                    fields = null;
                }
            }
            return fields;
        }

        /// <summary>
        /// Gets the attribute value for the specified attribute and field.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimensionName">The name of the dimension for which to get the attribute value.</param>
        /// <param name="tableId">The id of the attribute table.</param>
        /// <param name="fieldName">The attribute field name.</param>
        /// <param name="elementName">The element name.</param>
        /// <param name="fieldDefinition">The definition of the attribute field. At least type, width and decimals must be set.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>An object that holds the attribute value corresponding to the specified type.
        /// Returns null, if the value was not found.</returns>
        public static object AttributeGetValue(int clientSlot, int serverHandle, string dimensionName, int tableId, string fieldName, string elementName, OlapAttributeTableFieldDefinition fieldDefinition, IntPointer lastError)
        {
            System.DateTime dateTime;
            int error = 0;
            object result = null;

            OlapNativeImports.CallBegin(serverHandle, UniFunction.AttrGetField);
            OlapNativeImports.SetElement(serverHandle, elementName);
            OlapNativeImports.SetDimension(serverHandle, dimensionName);
            OlapNativeImports.SetTableId(serverHandle, tableId);
            OlapNativeImports.SetName(serverHandle, fieldName);

            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;
            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                char type = OlapNativeImports.GetCType(serverHandle);
                switch (type)
                {
                    case 'C':
                        result = OlapNativeImports.GetSValue(serverHandle);
                        break;

                    case 'N':
                        string resultValue = OlapNativeImports.GetSValue(serverHandle);
                        if (!string.IsNullOrEmpty(resultValue))
                        {
                            result = System.Convert.ToDouble(resultValue, System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            result = 0.0;
                        }
                        break;

                    case 'L':
                        {
                            string val = OlapNativeImports.GetSValue(serverHandle);
                            if (val[0] == 'T')
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        break;

                    case 'D':
                        {
                            // TODO 10.5: Double check this parsing logic!
                            string dateval = OlapNativeImports.GetSValue(serverHandle);
                            if (!string.IsNullOrEmpty(dateval))
                            {
                                int day = System.Convert.ToInt32(dateval.Substring(6, 2));
                                int month = System.Convert.ToInt32(dateval.Substring(4, 2));
                                int year = System.Convert.ToInt32(dateval.Substring(0, 4));
                                dateTime = new System.DateTime(year, month, day);
                            }
                            else
                            {
                                dateTime = new DateTime();
                            }
                            result = dateTime;
                        }
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// Sets the attribute value for the specified attribute and field.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimensionName">The name of the dimension for which to set the attribute value.</param>
        /// <param name="tableId">The id of the attribute table.</param>
        /// <param name="fieldName">The attribute field name.</param>
        /// <param name="elementName">The element name.</param>
        /// <param name="fieldDefinition">The definition of the attribute field. At least type, width and decimals must be set.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if the value has been written; false, otherwise.</returns>
        public static bool AttributeSetValue(int clientSlot, int serverHandle, string dimensionName, int tableId, string fieldName, string elementName, OlapAttributeTableFieldDefinition fieldDefinition, object value, IntPointer lastError)
        {
            System.DateTime dateTime;
            int error = 0;

            OlapNativeImports.CallBegin(serverHandle, UniFunction.AttrPutField);
            OlapNativeImports.SetTableId(serverHandle, tableId);
            OlapNativeImports.SetDimension(serverHandle, dimensionName);
            OlapNativeImports.SetName(serverHandle, fieldName);
            OlapNativeImports.SetElement(serverHandle, elementName);

            int fieldDefinitionType = (int)fieldDefinition.Type;
            switch (fieldDefinitionType)
            {
                case (int)OlapAttributeTableFieldType.OlapAttributeTableFieldTypeCharacter:
                    {
                        string v = (string)value;
                        if (v == null)
                        {
                            lastError.Value = -2;
                            return false;
                        }
                        OlapNativeImports.SetSValue(serverHandle, v);
                        error = OlapNativeImports.CallEnd(serverHandle);
                        lastError.Value = error;
                    }
                    break;

                case (int)OlapAttributeTableFieldType.OlapAttributeTableFieldTypeNumeric:
                    {
                        string sval = string.Empty;

                        if (value is int)
                        {
                            int dval = (int)value;
                            sval = System.Convert.ToString(dval, System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            double dval = (double)value;
                            sval = System.Convert.ToString(dval, System.Globalization.CultureInfo.InvariantCulture);
                        }

                        // TODO 10.5: Double check the following logic.
                        int pos = sval.IndexOf('.');
                        if (pos != -1)
                        {
                            if (pos > (fieldDefinition.FieldWidth - fieldDefinition.Decimals))
                            {
                                // too many places before the decimal point
                                // TODO 10.5: from resource
                                throw new OlapException("The number defined to write to the elements attribute has more digits before the decimal point as allowed.");
                            }
                        }
                        else
                        {
                            if (sval.Length > fieldDefinition.FieldWidth - fieldDefinition.Decimals)
                            {
                                // too many places before the decimal point
                                // TODO 10.5: from resource
                                throw new OlapException("The number defined to write to the elements attribute has more digits before the decimal point as allowed.");
                            }
                        }
                        OlapNativeImports.SetSValue(serverHandle, sval);
                        error = OlapNativeImports.CallEnd(serverHandle);
                        lastError.Value = error;
                    }
                    break;

                case (int)OlapAttributeTableFieldType.OlapAttributeTableFieldTypeDate:
                    {
                        // TODO 10.5: Double check the following parsing logic.
                        dateTime = (System.DateTime)value;
                        string date = dateTime.Year.ToString("0000");
                        date += dateTime.Month.ToString("00");
                        date += dateTime.Day.ToString("00");
                        OlapNativeImports.SetSValue(serverHandle, date);
                        error = OlapNativeImports.CallEnd(serverHandle);
                        lastError.Value = error;
                    }
                    break;

                case (int)OlapAttributeTableFieldType.OlapAttributeTableFieldTypeLogical:
                    {
                        bool v = (bool)value;
                        if (v)
                        {
                            OlapNativeImports.SetSValue(serverHandle, "T");
                        }
                        else
                        {
                            OlapNativeImports.SetSValue(serverHandle, "F");
                        }
                        error = OlapNativeImports.CallEnd(serverHandle);
                        lastError.Value = error;
                    }
                    break;
            }

            lastError.Value = error;
            return error == (int)ClientSupportErrorCodes.ECI_OK;
        }

        /// <summary>
        /// Gets the subsytems of the specified dimension on the specified server.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimensionName">The name of the dimension for which to get the subsets.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>An ArrayList that holds instances of the OlapSubsetDefinition struct. </returns>
        public static System.Collections.ArrayList DimensionSubsets(int clientSlot, int serverHandle, string dimensionName, IntPointer lastError)
        {
            System.Collections.ArrayList subsets = null;
            int error = 0;

            OlapNativeImports.CallBegin(serverHandle, UniFunction.SelectSubset);
            OlapNativeImports.SetDimension(serverHandle, dimensionName);
            OlapNativeImports.SetRefName(serverHandle, "*");
            OlapNativeImports.SetFlags(serverHandle, ServerConstants.SstAll);
            lastError.Value = error = OlapNativeImports.CallEnd(serverHandle);
            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                int count = OlapNativeImports.GetCount(serverHandle);
                if (count > 0)
                {
                    subsets = new System.Collections.ArrayList();
                    for (int i = 0; i < count; i++)
                    {
                        OlapNativeImports.CallBegin(serverHandle, UniFunction.FetchSubset);
                        OlapNativeImports.SetId(serverHandle, i + 1);
                        if (OlapNativeImports.CallEnd(serverHandle) == (int)ClientSupportErrorCodes.ECI_OK)
                        {
                            OlapSubsetDefinition def = new OlapSubsetDefinition();
                            def.RefName = OlapNativeImports.GetRefName(serverHandle);
                            def.LongName = OlapNativeImports.GetLongName(serverHandle);
                            def.CreatedByUser = OlapNativeImports.GetUser(serverHandle);
                            int saveRST = OlapNativeImports.GetFlags(serverHandle) & ServerConstants.SstSaveResult;
                            def.SaveResultSet = (saveRST == ServerConstants.SstSaveResult) ? true : false;
                            def.Type = OlapNativeImports.GetCType(serverHandle);
                            subsets.Add(def);
                        }
                    }
                    if (subsets.Count == 0)
                    {
                        subsets = null;
                    }
                }
            }

            return subsets;
        }

        /// <summary>
        /// Gets the elements of a subset.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="dimensionName">The name of the dimension for which to get the subsets.</param>
        /// <param name="subsetDefinition">Definition of the subset to be returned.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A collection of strings with the element names of the subset's elements. If the function fails
        /// the return value is null.</returns>
        public static System.Collections.Specialized.StringCollection DimensionSubsetElements(int clientSlot, int serverHandle, string dimensionName, OlapSubsetDefinition subsetDefinition, IntPointer lastError)
        {
            System.Collections.Specialized.StringCollection elements = null;
            int error = 0;

            OlapNativeImports.CallBegin(serverHandle, UniFunction.BuildSubset);
            OlapNativeImports.SetDimension(serverHandle, dimensionName);
            OlapNativeImports.SetName(serverHandle, subsetDefinition.LongName); // name can be specified in combination with dimension name
            OlapNativeImports.SetFlags(serverHandle, ServerConstants.SstAll);
            OlapNativeImports.SetParam(serverHandle, ServerConstants.MdspRefresh);  // + other MDSP_ flags
            OlapNativeImports.SetId(serverHandle, 0);

            lastError.Value = error = OlapNativeImports.CallEnd(serverHandle);
            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                int count = OlapNativeImports.GetCount(serverHandle);
                int index = 1;
                if (count > 0)
                {
                    elements = new System.Collections.Specialized.StringCollection();

                    while (count > 0)
                    {
                        OlapNativeImports.CallBegin(serverHandle, UniFunction.GetMultipleSubsetItems2);
                        OlapNativeImports.SetId(serverHandle, index); // one based start ordinal - default 1
                        lastError.Value = error = OlapNativeImports.CallEnd(serverHandle);
                        if (error == (int)ClientSupportErrorCodes.ECI_OK)
                        {
                            while (OlapNativeImports.GetNextItem(serverHandle))
                            {
                                elements.Add(OlapNativeImports.GetName(serverHandle));
                                count--;
                                index++;
                            }
                        }
                    }
                }
            }

            return elements;
        }

        /// <summary>
        /// Defines a new data area for a cube.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of the cube for which to define the data area.</param>
        /// <param name="dimensionElements">An array of string lists with one string list for each dimension.</param>
        /// <param name="def">A definition of additional parameters for the data area. Must be the same as for DataAreaFirst.</param>
        /// <param name="dataareaActive">A reference to a flag that will hold the status if a dataarea is already active for a specific connection.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if all definitions have been set correctly; false, otherwise. </returns>
        public static bool DataAreaDefine(int clientSlot, int serverHandle, string cubeName, System.Collections.Specialized.StringCollection[] dimensionElements, OlapApiDataAreaParameters def, BoolPointer dataareaActive, IntPointer lastError)
        {
            if (dataareaActive.Value)
            {
                throw new OlapException("You cannot define 2 data areas at the same time. Use deactivate before creating a new data area!");
            }
            dataareaActive.Value = false;
            int error = 0;
            OlapNativeImports.CallBegin(serverHandle, UniFunction.daCreateDataArea);
            OlapNativeImports.SetCube(serverHandle, cubeName);
            error = OlapNativeImports.CallEnd(serverHandle);
            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                lastError.Value = error;
                for (int i = 0; i < dimensionElements.Length; i++)
                {
                    System.Collections.Specialized.StringCollection elements = dimensionElements[i];
                    string currentElement = elements[0];

                    if (currentElement.Equals("*") || currentElement.ToUpper().Equals("B:*"))
                    {
                        OlapNativeImports.CallBegin(serverHandle, UniFunction.daSetDimElement);
                        OlapNativeImports.SetId(serverHandle, i);
                        OlapNativeImports.SetElement(serverHandle, currentElement.ToUpper());
                        error = OlapNativeImports.CallEnd(serverHandle);
                        if (error != (int)ClientSupportErrorCodes.ECI_OK)
                        {
                            lastError.Value = error;
                            return false;
                        }
                    }
                    else if (currentElement.Length == 0)
                    {
                        OlapNativeImports.CallBegin(serverHandle, UniFunction.daSetDimElement);
                        OlapNativeImports.SetId(serverHandle, i);

                        // first element empty so this must be element list
                        for (int j = 1; j < elements.Count; j++)
                        {
                            OlapNativeImports.SetElement(serverHandle, elements[j]);
                        }
                        error = OlapNativeImports.CallEnd(serverHandle);
                        if (error != (int)ClientSupportErrorCodes.ECI_OK)
                        {
                            lastError.Value = error;
                            return false;
                        }
                    }
                    else
                    {
                        // in this case we have a subset TODO
                        return false;
                    }
                }
            }
            lastError.Value = error;
            return dataareaActive.Value = lastError.Value == 0;
        }

        /// <summary>
        /// Destroys the previously created area.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="def">A definition of additional parameters for the data area. Must be the same as for DataAreaFirst.</param>
        /// <param name="dataareaActive">A reference to a flag that will hold the status if a dataarea is already active for a specific connection.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if successful; false; otherwise.</returns>
        public static bool DataAreaDestroy(int clientSlot, int serverHandle, OlapApiDataAreaParameters def, BoolPointer dataareaActive, IntPointer lastError)
        {
            int error = 0;
            OlapNativeImports.CallBegin(serverHandle, UniFunction.daDestroyDataArea);
            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;
            dataareaActive.Value = false;
            return error == (int)ClientSupportErrorCodes.ECI_OK;
        }

        /// <summary>
        /// Deletes values in a data area.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="def">A definition of additional parameters for the data area. Must be the same as for DataAreaFirst.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if successful; false; otherwise.</returns>
        public static bool DataAreaDelete(int clientSlot, int serverHandle, OlapApiDataAreaParameters def, IntPointer lastError)
        {
            OlapNativeImports.CallBegin(serverHandle, UniFunction.daSetValue);
            OlapNativeImports.SetParam(serverHandle, ServerConstants.MdsValueDelete);
            OlapNativeImports.SetFlags(serverHandle, ServerConstants.DwSuppressNa);
            int form = 0;
            if (def.SuppressNullValue)
            {
                if (!def.BaseValuesOnly && !def.SuppressConsolidated)
                {
                    form = ServerConstants.CsString | ServerConstants.CsNumeric | _calculatedCells;
                }
                else
                {
                    form = ServerConstants.CsString | ServerConstants.CsNumeric;
                }
            }
            else
            {
                if (!def.BaseValuesOnly && !def.SuppressConsolidated)
                {
                    form = ServerConstants.CsString | ServerConstants.CsNumeric | ServerConstants.CsMissing | _calculatedCells;
                }
                else
                {
                    form = ServerConstants.CsString | ServerConstants.CsNumeric | ServerConstants.CsMissing;
                }
            }

            OlapNativeImports.SetLeftCondition(serverHandle, (int)def.FirstOperator, def.FirstValue);
            OlapNativeImports.SetRightCondition(serverHandle, (int)def.SecondOperator, def.SecondValue);

            int error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;
            return error == (int)ClientSupportErrorCodes.ECI_OK || error == 66;
        }

        /// <summary>
        /// Sets values in a data area.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="def">A definition of additional parameters for the data area. Must be the same as for DataAreaFirst.</param>
        /// <param name="value">Value to which the cells in the data area are to be set.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if successful; false; otherwise.</returns>
        public static bool DataAreaSetValues(int clientSlot, int serverHandle, OlapApiDataAreaParameters def, double value, IntPointer lastError)
        {
            OlapNativeImports.CallBegin(serverHandle, UniFunction.daSetValue);
            OlapNativeImports.SetNValue(serverHandle, value);

            int form = 0;
            if (def.SuppressNullValue)
            {
                if (!def.BaseValuesOnly && !def.SuppressConsolidated)
                {
                    form = ServerConstants.CsString | ServerConstants.CsNumeric | _calculatedCells;
                }
                else
                {
                    form = ServerConstants.CsString | ServerConstants.CsNumeric;
                }
            }
            else
            {
                if (!def.BaseValuesOnly && !def.SuppressConsolidated)
                {
                    form = ServerConstants.CsString | ServerConstants.CsNumeric | ServerConstants.CsMissing | _calculatedCells;
                }
                else
                {
                    form = ServerConstants.CsString | ServerConstants.CsNumeric | ServerConstants.CsMissing;
                }
            }

            int oldflags = MakeLong((short)form, (short)1);
            OlapNativeImports.SetId(serverHandle, oldflags);

            OlapNativeImports.SetLeftCondition(serverHandle, (int)def.FirstOperator, def.FirstValue);
            OlapNativeImports.SetRightCondition(serverHandle, (int)def.SecondOperator, def.SecondValue);

            int error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;
            return error == (int)ClientSupportErrorCodes.ECI_OK || error == 66;
        }

        /// <summary>
        /// Sets values in a data area.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="def">A definition of additional parameters for the data area. Must be the same as for DataAreaFirst.</param>
        /// <param name="value">Value to which the cells in the data area are to be set.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if successful; false; otherwise.</returns>
        public static bool DataAreaSetValues(int clientSlot, int serverHandle, OlapApiDataAreaParameters def, string value, IntPointer lastError)
        {
            OlapNativeImports.CallBegin(serverHandle, UniFunction.daSetValue);
            OlapNativeImports.SetSValue(serverHandle, value);

            int form = 0;
            if (def.SuppressNullValue)
            {
                if (!def.BaseValuesOnly && !def.SuppressConsolidated)
                {
                    form = ServerConstants.CsString | ServerConstants.CsNumeric | _calculatedCells;
                }
                else
                {
                    form = ServerConstants.CsString | ServerConstants.CsNumeric;
                }
            }
            else
            {
                if (!def.BaseValuesOnly && !def.SuppressConsolidated)
                {
                    form = ServerConstants.CsString | ServerConstants.CsNumeric | ServerConstants.CsMissing | _calculatedCells;
                }
                else
                {
                    form = ServerConstants.CsString | ServerConstants.CsNumeric | ServerConstants.CsMissing;
                }
            }

            int oldflags = MakeLong((short)form, 1);
            OlapNativeImports.SetId(serverHandle, oldflags);
            int error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;
            return error == (int)ClientSupportErrorCodes.ECI_OK || error == 66;
        }

        /// <summary>
        /// Calculates a unique hash value for a data area.
        /// </summary>
        /// <param name="clientslot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="def">A definition of additional parameters for the data area. Must be the same as for DataAreaFirst.</param>
        /// <param name="key">Key to be used for hashing.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>A unique hash value for the data area.</returns>
        public static string DataAreaCalculateHash(int clientslot, int serverHandle, OlapApiDataAreaParameters def, string key, IntPointer lastError)
        {
            int error = 0;
            int flags = 0;

            if (def.SuppressNullValue)
            {
                flags |= ServerConstants.DwSuppressNa;
            }
            if (def.TreatZeroAsNA)
            {
                flags |= ServerConstants.DwZeroAsNa;
            }
            if (!def.SuppressConsolidated)
            {
                flags |= ServerConstants.DwCalcCells;
            }

            OlapNativeImports.CallBegin(serverHandle, UniFunction.CalculateHash); // TODO 10.5: Correct?
            OlapNativeImports.SetFlags(serverHandle, flags);
            OlapNativeImports.SetParam(serverHandle, 0); // TODO 10.5: Correct? Was Ipo.SetParam(serverHandle, false) before.
            OlapNativeImports.SetSValue(serverHandle, key);
            error = OlapNativeImports.CallEnd(serverHandle);
            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                string hash = OlapNativeImports.GetSValue(serverHandle);
                if (hash != null)
                {
                    return hash;
                }
            }
            lastError.Value = error;
            return null;
        }

        /// <summary>
        /// Gets the first elements of the previously defined data area. If the results consists of more than one
        /// value the result arraylist holds the number of values returned by the Olap function.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">The name of the cube for which to define the data area.</param>
        /// <param name="def">A definition of additional parameters for the data area. Must be the same as for DataAreaFirst.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>An ArrayList that include StringCollection as elements. Each included StringCollection
        /// has a list of strings with all dimension elements that reference the cell. The last string
        /// in the list is the actual value.</returns>
        public static System.Collections.ArrayList DataAreaFirst(int clientSlot, int serverHandle, string cubeName, OlapApiDataAreaParameters def, IntPointer lastError)
        {
            System.Collections.ArrayList results = null;
            int param = MakeLong(MakeWord(0, (byte)def.DecimalDelimiter), 0);
            int error = 0;

            int lId = ServerConstants.CsNumeric; // set flags
            if (!def.SuppressTextValues)
            {
                lId |= ServerConstants.CsString;
            }
            if (!def.BaseValuesOnly)
            {
                if (def.SuppressConsolidated)
                {
                    lId |= ServerConstants.CsGlobalRule | ServerConstants.CsLocalRule;
                }
                else
                {
                    lId |= ServerConstants.CsConsolidated | ServerConstants.CsGlobalRule | ServerConstants.CsLocalRule;
                }
            }

            if (!def.SuppressNullValue)
            {
                lId |= ServerConstants.CsMissing;
            }

            OlapNativeImports.CallBegin(serverHandle, UniFunction.daGetFirst);
            OlapNativeImports.SetId(serverHandle, lId);
            OlapNativeImports.SetParam(serverHandle, (uint)param); // TODO 10.5: Check, if uint cast is OK.

            // TODO 10.5: doesnt work in alea!!!
            OlapNativeImports.SetLeftCondition(serverHandle, (int)def.FirstOperator, def.FirstValue);
            OlapNativeImports.SetRightCondition(serverHandle, (int)def.SecondOperator, def.SecondValue);
            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;
            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                bool hasValue = !((OlapNativeImports.GetFlags(serverHandle) & ServerConstants.ResultEnd) == ServerConstants.ResultEnd);
                if (hasValue)
                {
                    results = new System.Collections.ArrayList();
                    System.Collections.Specialized.StringCollection dataSet = new System.Collections.Specialized.StringCollection();

                    while (OlapNativeImports.GetNextItem(serverHandle))
                    {
                        dataSet.Add(OlapNativeImports.GetSValue(serverHandle));
                    }
                    results.Add(dataSet);
                }
            }

            return results;
        }

        /// <summary>
        /// Gets the next elements of the previously defined data area. DataAreaFirst must be called first. If the
        /// results consists of more than one value the result arraylist holds the number of values returned by the
        /// Olap function.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="def">A definition of additional parameters for the data area. Must be the same as for DataAreaFirst.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>An ArrayList that include StringCollections as elements. Each included StringCollection
        /// has a list of strings with all dimension elements that reference the cell. The last string
        /// in the list is the actual value.</returns>
        public static System.Collections.ArrayList DataAreaNext(int clientSlot, int serverHandle, OlapApiDataAreaParameters def, IntPointer lastError)
        {
            System.Collections.ArrayList results = null;
            int error = 0;

            OlapNativeImports.CallBegin(serverHandle, UniFunction.daGetNext);
            error = OlapNativeImports.CallEnd(serverHandle);
            lastError.Value = error;
            if (error == (int)ClientSupportErrorCodes.ECI_OK)
            {
                bool hasValue = !((OlapNativeImports.GetFlags(serverHandle) & ServerConstants.ResultEnd) == ServerConstants.ResultEnd);
                if (hasValue)
                {
                    results = new System.Collections.ArrayList();
                    System.Collections.Specialized.StringCollection dataSet = new System.Collections.Specialized.StringCollection();

                    while (OlapNativeImports.GetNextItem(serverHandle))
                    {
                        dataSet.Add(OlapNativeImports.GetSValue(serverHandle));
                    }
                    results.Add(dataSet);
                }
            }
            return results;
        }

        /// <summary>
        /// Gets an error text corresponding to the specified OLAP error code.
        /// </summary>
        /// <param name="errorCode">An error code returned by OLAP.</param>
        /// <returns>The error description for the specified error.</returns>
        public static string ErrorDescription(int errorCode)
        {
            string message = new string(new char[4000]);
            int[] error = new int[1];
            int result = OlapNativeImports.MdsapiGetErrorMessageW(error, errorCode, message, 2000);
            return message;
        }

        /// <summary>
        /// Imports data into a cube and writes messages to an error file in case of any problems.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">Name of the cube to be imported.</param>
        /// <param name="importFile">Name of the import file.</param>
        /// <param name="errorFile">Name of the error file.</param>
        /// <param name="fields">List of the field names to be imported.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if the data could be imported. False, otherwise.</returns>
        public static bool CubeImport(int clientSlot, int serverHandle, string cubeName, string importFile, string errorFile, System.Collections.ArrayList fields, IntPointer lastError)
        {
            // TODO 10.5: implement
            return false;
        }

        /// <summary>
        /// Exports cube data into a file and writes messages to an error file in case of any problems.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="cubeName">Name of the cube to be exported.</param>
        /// <param name="importFile">Name of the export file.</param>
        /// <param name="errorFile">Name of the error file.</param>
        /// <param name="fields">List of the field names to be exported.</param>
        /// <param name="lastError">The last error that has occured.</param>
        /// <returns>True, if the data could be exported. False, otherwise.</returns>
        public static bool CubeExport(int clientSlot, int serverHandle, string cubeName, string importFile, string errorFile, System.Collections.ArrayList fields, IntPointer lastError)
        {
            // TODO 10.5: implement
            return false;
        }

        /// <summary>
        /// Runs an XML request.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="request">A XML request definition.</param>
        /// <returns>The XML returned from OLAP server.</returns>
        public static string XMLRequest(int clientSlot, int serverHandle, string request)
        {
            IntPtr ptr;
            int[] error = new int[1];
            int result = OlapNativeImports.MdsXMLRequestUTF8String(clientSlot, serverHandle, request, out ptr, error);
            string xx = Marshal.PtrToStringAnsi(ptr);
            return xx;
        }

        /// <summary>
        /// Resolves a unique name, that is converts it from XMLA to its native representation.
        /// </summary>
        /// <param name="clientSlot">A valid client slot.</param>
        /// <param name="serverHandle">A server handle.</param>
        /// <param name="name">Name to be resolved.</param>
        /// <returns>Native equivalent of the unique name.</returns>
        public static string ResolveUniqueName(int clientSlot, int serverHandle, string name)
        {
            string requestID = "1";
            XElement document = RequestBase.CreateDocumentBase();
            XElement request = ResolveUniqueNameRequest.CreateRequest(requestID, new string[] { name });
            document.Add(request);
            StringWriter requestString = new StringWriter();
            document.Save(requestString);

            OlapXmlResponse response = new OlapXmlResponse(XMLRequest(clientSlot, serverHandle, requestString.ToString()));
            if (response.HasErrors)
            {
                return string.Empty;
            }
            return response.CheckForErrorsInResolveUniqueName(name);
        }

        /// <summary>
        /// Sends an OLAP XML splash request to an OLAP server to write a value to a calculated cell.
        /// </summary>
        /// <param name="clientSlot">The client connection slot.</param>
        /// <param name="serverHandle">The handle of the server.</param>
        /// <param name="requestID">The id of the request.</param>
        /// <param name="cube">The cube to splash to.</param>
        /// <param name="value">The value to splash.</param>
        /// <param name="mode">The splash mode.</param>
        /// <param name="rounding">True, if values should be rounded, false if not.</param>
        /// <param name="decimals">The number of decimals to round, if rounding is true.</param>
        /// <param name="notDeleteOnZero">If true, sending the value 0 will not delete the leaf cells, but it will write the value 0. Otherwise sending the value 0 will delete the leaf cells.</param>
        /// <param name="first">The element for the first dimension.</param>
        /// <param name="second">The element for the second dimension.</param>
        /// <param name="elements">The variable elements for all other dimensions.</param>
        /// <returns>True, if successfull, false, if an error occurred.</returns>
        internal static bool SplashValue(int clientSlot, int serverHandle, string requestID, OlapCube cube, double value, string mode, bool rounding, int decimals, bool notDeleteOnZero, string first, string second, string[] elements)
        {
            string request = SplashRequest.CreateRequest(requestID, cube, value, mode, rounding, decimals, notDeleteOnZero, first, second, elements);
            OlapXmlResponse response = new OlapXmlResponse(XMLRequest(clientSlot, serverHandle, request));
            return !response.HasErrors;
        }

        /// <summary>
        /// This function starts the OLAP load from source function. Note that it returns immediately so the load process must not be finished then.
        /// </summary>
        /// <param name="clientSlot">The client connection slot.</param>
        /// <param name="serverHandle">The handle of the server.</param>
        /// <param name="requestID">The id of the request.</param>
        /// <returns>True, if the request was accepted by the server.</returns>
        internal static bool StartLoadFromSource(int clientSlot, int serverHandle, string requestID)
        {
            string request = TriggerLoadFromSourceRequest.CreateRequest(requestID);
            OlapXmlResponse response = new OlapXmlResponse(XMLRequest(clientSlot, serverHandle, request));
            return !response.HasErrors;
        }

        /// <summary>
        /// This function creates a new numerical dimension element.
        /// </summary>
        /// <param name="clientSlot">The client connection slot.</param>
        /// <param name="serverHandle">The handle of the server.</param>
        /// <param name="requestID">The id of the request.</param>
        /// <param name="dimension">The dimension name.</param>
        /// <param name="numericElement">If true, a numeric element will be created. If false, a text element will be created.</param>
        /// <param name="element">The element to create.</param>
        /// <param name="parentElement">The parent element under which the new element should be created. If empty the element will be created on the top level.</param>
        /// <param name="weight">The weight used to aggregate the new element into it's parent. Only used if parent element is provided.</param>
        /// <returns>True, if the element was created. False, if an error occurred.</returns>
        internal static bool CreateDimensionElement(int clientSlot, int serverHandle, string requestID, string dimension, bool numericElement, string element, string parentElement, double weight)
        {
            string request = EditDimensionElementRequest.CreateAddElementRequest(requestID, dimension, numericElement, element, parentElement, weight);
            OlapXmlResponse response = new OlapXmlResponse(XMLRequest(clientSlot, serverHandle, request));
            if (response.HasErrors)
            {
                LogError(LogEvent.ErrorInXmlResponse, "The element " + element + " could not be created in dimension " + dimension + ".");
                return false;
            }
            return !response.CheckForErrorsInCreateDimensionElement(dimension, element);
        }

        /// <summary>
        /// This function renames a dimension element.
        /// </summary>
        /// <param name="clientSlot">The client connection slot.</param>
        /// <param name="serverHandle">The handle of the server.</param>
        /// <param name="requestID">The id of the request.</param>
        /// <param name="dimension">The dimension name.</param>
        /// <param name="element">The element to rename.</param>
        /// <param name="newName">The new name of the element.</param>
        /// <returns>True, if the element was renamed. False, otherwise.</returns>
        internal static bool RenameDimensionElement(int clientSlot, int serverHandle, string requestID, string dimension, string element, string newName)
        {
            string request = EditDimensionElementRequest.CreateRenameElementRequest(requestID, dimension, element, newName);
            OlapXmlResponse response = new OlapXmlResponse(XMLRequest(clientSlot, serverHandle, request));
            if (response.HasErrors)
            {
                LogError(LogEvent.ErrorInXmlResponse, "The element " + element + " could not be renamed to '" + newName + "' in dimension " + dimension + ".");
                return false;
            }
            return !response.CheckForErrorsInRenameDimensionElement(dimension, element, newName);
        }

        /// <summary>
        /// This function deletes a dimension element.
        /// </summary>
        /// <param name="clientSlot">The client connection slot.</param>
        /// <param name="serverHandle">The handle of the server.</param>
        /// <param name="requestID">The id of the request.</param>
        /// <param name="dimension">The dimension name.</param>
        /// <param name="element">The element to delete.</param>
        /// <returns>True, if the element was deleted. False, if an error occurred.</returns>
        internal static bool DeleteDimensionElement(int clientSlot, int serverHandle, string requestID, string dimension, string element)
        {
            string request = EditDimensionElementRequest.CreateDeleteElementRequest(requestID, dimension, element);
            OlapXmlResponse response = new OlapXmlResponse(XMLRequest(clientSlot, serverHandle, request));
            return !response.HasErrors;
        }

        /// <summary>
        /// Converts a number with the OLAP function double to string so that internal rounding issues will not appear and always the same formatted string is produced.
        /// </summary>
        /// <param name="formattedNumber">The formatted number, if return value indicates no errors.</param>
        /// <param name="number">The number to convert.</param>
        /// <returns>An OLAP error code.</returns>
        internal static int ConvertDoubleToString(out string formattedNumber, double number)
        {
            return OlapNativeImports.MdsDoubleToString(out formattedNumber, number);
        }

        /// <summary>
        /// Logs a message with a specific event id on level error.
        /// </summary>
        /// <param name="eventId">The id to log.</param>
        /// <param name="message">The message to log.</param>
        public static void LogError(LogEvent eventId, string message)
        {
            Infor.BI.Common.Application.ApplicationContext.Log.Error(typeof(NativeOlapApi), (int)eventId, message);
        }
    }
}