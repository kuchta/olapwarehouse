using Infor.BI.Applications.OlapApi.Native;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents an Olap cube.
    /// </summary>
    public class OlapCube : OlapObjectBase
    {
        /// <summary>
        /// Holds the server that owns the cube.
        /// </summary>
        private OlapServer _server;

        /// <summary>
        /// Holds a collection of dimensions that belong to the cube.
        /// </summary>
        private OlapDimensions _dimensions;

        /// <summary>
        /// Holds additional information about the cube.
        /// </summary>
        private OlapCubeInformation _cubeInformation;

        /// <summary>
        /// Holds the name of the cube.
        /// </summary>
        private string _name;

        /// <summary>
        /// Holds the uppercase name of the element.
        /// </summary>
        private string _upperName;

        /// <summary>
        /// Holds the server handle.
        /// </summary>
        private int _serverHandle;

        private string CreateExceptionString(string message)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder(message);
            result.Append(ToString());
            return result.ToString();
        }

        private static string _elementMismatch = "The elements provided to this function do not match the number of dimensions of this cube";

        /// <summary>
        /// Initializes a new instance of the OlapCube class.
        /// </summary>
        /// <param name="server">The server that owns the cube.</param>
        /// <param name="name">The name of the cube.</param>
        /// <param name="serverHandle">The server handle.</param>
        public OlapCube(OlapServer server, string name, int serverHandle)
        {
            _serverHandle = serverHandle;
            _server = server;
            _name = name;
            _upperName = null;
        }

        /// <summary>
        /// Save the cube data to disk.
        /// </summary>
        /// <returns>True, if the data has been saved; false, otherwise.</returns>
        public bool Save()
        {
            return NativeOlapApi.CubeSave(_server.Store.ClientSlot, _serverHandle, _name, _server.LastErrorInternal);
        }

        /// <summary>
        /// Refreshes the cube.
        /// </summary>
        /// <returns>True, if the cube has been refreshed; false, otherwise.</returns>
        public bool Refresh()
        {
            return NativeOlapApi.CubeRefresh(_server.Store.ClientSlot, _serverHandle, _name, _server.LastErrorInternal);
        }

        /// <summary>
        /// Saves the cube to disc and removes it from memory.
        /// </summary>
        /// <returns>True, if the cube has been removed; false, otherwise.</returns>
        public bool Clear()
        {
            return NativeOlapApi.CubeClear(_server.Store.ClientSlot, _serverHandle, _name, _server.LastErrorInternal);
        }

        /// <summary>
        /// Exports the entire cube data to a text file.
        /// </summary>
        /// <param name="exportFile">The name of the text file to export.</param>
        /// <param name="errorFile">The name of file to write the list of error that occured during the export.</param>
        /// <param name="fields">An array of integers that describe the order of columns in the text file to iomport.</param>
        /// <returns>True, if the values have been exported.</returns>
        public bool Export(string exportFile, string errorFile, System.Collections.ArrayList fields)
        {
            return true;
        }

        /// <summary>
        /// Imports the entire cube data from a text file.
        /// </summary>
        /// <param name="importFile">The name of the text file to export.</param>
        /// <param name="errorFile">The name of file to write the list of error that occured during the export.</param>
        /// <param name="fields">An array of integers that describe the order of columns in the text file to iomport.</param>
        /// <returns>True, if the values have been imported.</returns>
        public bool Import(string importFile, string errorFile, System.Collections.ArrayList fields)
        {
            return NativeOlapApi.CubeImport(Server.Store.ClientSlot, Server.ServerHandle, _name, importFile, errorFile, fields, Server.LastErrorInternal);
        }

        /// <summary>
        /// Gets a cell value of the cube.
        /// </summary>
        /// <param name="elements">A list of dimension element names that reference the cell.</param>
        /// <returns>An object representing the cell value. If the cell did not exist the return value is null.</returns>
        public object GetCell(System.Collections.Specialized.StringCollection elements)
        {
            if (Dimensions == null)
            {
                return null;
            }
            if (_dimensions.Count != elements.Count)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }
            return NativeOlapApi.CubeGetCell(_server.Store.ClientSlot, _serverHandle, _name, elements, _server.LastErrorInternal);
        }

        /// <summary>
        /// Gets a cell value of the cube.
        /// </summary>
        /// <param name="firstElement">The first element name.</param>
        /// <param name="secondElement">The second element name.</param>
        /// <param name="elements">A variable list of additional dimension elements that reference the cell.</param>
        /// <returns>An object representing the cell value. If the cell did not exist the return value is null.</returns>
        public object GetCell(string firstElement, string secondElement, params string[] elements)
        {
            if (Dimensions == null)
            {
                return null;
            }

            int total = 2;
            if (elements != null)
            {
                total += elements.Length;
            }

            if (_dimensions.Count != total)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }

            return NativeOlapApi.CubeGetCell(_server.Store.ClientSlot, _serverHandle, _name, firstElement, secondElement, elements, _server.LastErrorInternal);
        }

        /// <summary>
        /// Gets the comment for a specified cell of the cube.
        /// </summary>
        /// <param name="elements">A list of dimension elements that reference the cell.</param>
        /// <returns>A string with the cell comment. If the cell comment did not exist the return value is null.</returns>
        public string GetCellComment(System.Collections.Specialized.StringCollection elements)
        {
            if (Dimensions == null)
            {
                return null;
            }

            if (_dimensions.Count != elements.Count)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }
            string result = NativeOlapApi.CubeGetCellComment(_server.Store.ClientSlot, _serverHandle, _name, elements, _server.LastErrorInternal);
            if (result == null)
            {
                result = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// Gets the comment for a specified cell of the cube.
        /// </summary>
        /// <param name="firstElement">The first element name.</param>
        /// <param name="secondElement">The second element name.</param>
        /// <param name="elements">A variable list of additional dimension elements that reference the cell.</param>
        /// <returns>A string representing the cell comment. If the cell did not exist the return value is null.</returns>
        public string GetCellComment(string firstElement, string secondElement, params string[] elements)
        {
            if (Dimensions == null)
            {
                return null;
            }
            System.Collections.Specialized.StringCollection elementNames = new System.Collections.Specialized.StringCollection();
            elementNames.Add(firstElement);
            elementNames.Add(secondElement);
            if (elements != null)
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    elementNames.Add(elements[i]);
                }
            }
            if (_dimensions.Count != elementNames.Count)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }
            string result = NativeOlapApi.CubeGetCellComment(_server.Store.ClientSlot, _serverHandle, _name, elementNames, _server.LastErrorInternal);
            elementNames = null;
            return result;
        }

        /// <summary>
        /// Sets a cell value of the cube.
        /// </summary>
        /// <param name="value">The cell value, which might by of type int, double or string. In any case,
        /// the type must be the same as the actual cell type.</param>
        /// <param name="elements">A list of dimension elements that reference the cell.</param>
        /// <returns>True, if the value has been set or false, otherwise.</returns>
        public bool PutCell(object value, System.Collections.Specialized.StringCollection elements)
        {
            if (Dimensions == null)
            {
                return false;
            }
            if (_dimensions.Count != elements.Count)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }
            return NativeOlapApi.CubePutCell(_server.Store.ClientSlot, _serverHandle, _name, elements, value, false, _server.LastErrorInternal);
        }

        /// <summary>
        /// Sets a cell value of the cube.
        /// </summary>
        /// <param name="value">The cell value, which might be of type int, double or string. In any case,
        /// the type must be the same as the actual cell type.</param>
        /// <param name="firstElement">The first element name.</param>
        /// <param name="secondElement">The second element name.</param>
        /// <param name="elements">A variable list of additional dimension elements that reference the cell.</param>
        /// <returns>True, if the value has been set or false, otherwise.</returns>
        public bool PutCell(object value, string firstElement, string secondElement, params string[] elements)
        {
            if (Dimensions == null)
            {
                return false;
            }

            System.Collections.Specialized.StringCollection elementNames = new System.Collections.Specialized.StringCollection();
            elementNames.Add(firstElement);
            elementNames.Add(secondElement);
            if (elements != null)
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    elementNames.Add(elements[i]);
                }
            }
            if (_dimensions.Count != elementNames.Count)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }
            bool result = NativeOlapApi.CubePutCell(_server.Store.ClientSlot, _serverHandle, _name, elementNames, value, false, _server.LastErrorInternal);
            elementNames = null;
            return result;
        }

        /// <summary>
        /// Increment a cell value of the cube.
        /// </summary>
        /// <param name="value">The cell value, which might be of type int, double or string. In any case,
        /// the type must be the same as the actual cell type.</param>
        /// <param name="elements">A variable list of additional dimension elements that reference the cell.</param>
        /// <returns>True, if the value has been set or false, otherwise.</returns>
        public bool IncrementCell(object value, System.Collections.Specialized.StringCollection elements)
        {
            if (Dimensions == null)
            {
                return false;
            }
            if (_dimensions.Count != elements.Count)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }
            return NativeOlapApi.CubePutCell(_server.Store.ClientSlot, _serverHandle, _name, elements, value, true, _server.LastErrorInternal);
        }

        /// <summary>
        /// Increment a cell value of the cube.
        /// </summary>
        /// <param name="value">The cell value, which might be of type int, double or string. In any case,
        /// the type must be the same as the actual cell type.</param>
        /// <param name="firstElement">The first element name.</param>
        /// <param name="secondElement">The second element name.</param>
        /// <param name="elements">A variable list of additional dimension elements that reference the cell.</param>
        /// <returns>True, if the value has been set or false, otherwise.</returns>
        public bool IncrementCell(object value, string firstElement, string secondElement, params string[] elements)
        {
            if (Dimensions == null)
            {
                return false;
            }

            System.Collections.Specialized.StringCollection elementNames = new System.Collections.Specialized.StringCollection();
            elementNames.Add(firstElement);
            elementNames.Add(secondElement);
            if (elements != null)
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    elementNames.Add(elements[i]);
                }
            }
            if (_dimensions.Count != elementNames.Count)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }
            bool result = NativeOlapApi.CubePutCell(_server.Store.ClientSlot, _serverHandle, _name, elementNames, value, true, _server.LastErrorInternal);
            elementNames = null;
            return result;
        }

        /// <summary>
        /// Sets the comment for a sepcified cell of the cube.
        /// </summary>
        /// <param name="comment">The cell comment with a maximum length of 127.</param>
        /// <param name="elements">A list of dimension elements that reference the cell.</param>
        /// <returns>True, if the value has been set or false, otherwise.</returns>
        public bool PutCellComment(string comment, System.Collections.Specialized.StringCollection elements)
        {
            if (Dimensions == null)
            {
                return false;
            }
            if (_dimensions.Count != elements.Count)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }
            return NativeOlapApi.CubePutCellComment(_server.Store.ClientSlot, _serverHandle, _name, elements, comment, _server.LastErrorInternal);
        }

        /// <summary>
        /// Sets the comment for a sepcified cell of the cube.
        /// </summary>
        /// <param name="comment">The cell comment with a maximum length of 127.</param>
        /// <param name="firstElement">The first element name.</param>
        /// <param name="secondElement">The second element name.</param>
        /// <param name="elements">A variable list of additional dimension elements that reference the cell.</param>
        /// <returns>True, if the value has been set or false, otherwise.</returns>
        public bool PutCellComment(string comment, string firstElement, string secondElement, params string[] elements)
        {
            if (Dimensions == null)
            {
                return false;
            }

            System.Collections.Specialized.StringCollection elementNames = new System.Collections.Specialized.StringCollection();
            elementNames.Add(firstElement);
            elementNames.Add(secondElement);

            if (elements != null)
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    elementNames.Add(elements[i]);
                }
            }
            if (_dimensions.Count != elementNames.Count)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }
            bool result = NativeOlapApi.CubePutCellComment(_server.Store.ClientSlot, _serverHandle, _name, elementNames, comment, _server.LastErrorInternal);
            elementNames = null;
            return result;
        }

        /// <summary>
        /// Deletes the value of a cell of the cube.
        /// </summary>
        /// <param name="elements">A list of dimension elements that reference the cell.</param>
        /// <returns>True, if the value has been deleted or false, otherwise.</returns>
        public bool DeleteCell(System.Collections.Specialized.StringCollection elements)
        {
            if (Dimensions == null)
            {
                return false;
            }
            if (_dimensions.Count != elements.Count)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }
            return NativeOlapApi.CubeDeleteCell(_server.Store.ClientSlot, _serverHandle, _name, elements, _server.LastErrorInternal);
        }

        /// <summary>
        /// Deletes the value of a cell of the cube.
        /// </summary>
        /// <param name="firstElement">The first element name.</param>
        /// <param name="secondElement">The second element name.</param>
        /// <param name="elements">A variable list of additional dimension elements that reference the cell.</param>
        /// <returns>True, if the value has been deleted or false, otherwise.</returns>
        public bool DeleteCell(string firstElement, string secondElement, params string[] elements)
        {
            if (Dimensions == null)
            {
                return false;
            }

            System.Collections.Specialized.StringCollection elementNames = new System.Collections.Specialized.StringCollection();
            elementNames.Add(firstElement);
            elementNames.Add(secondElement);
            if (elements != null)
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    elementNames.Add(elements[i]);
                }
            }
            if (_dimensions.Count != elementNames.Count)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }
            bool result = NativeOlapApi.CubeDeleteCell(_server.Store.ClientSlot, _serverHandle, _name, elementNames, _server.LastErrorInternal);
            elementNames = null;
            return result;
        }

        /// <summary>
        /// Deletes the comment of a cell of the cube.
        /// </summary>
        /// <param name="elements">A list of dimension elements that reference the cell.</param>
        /// <returns>True, if the comment has been deleted or false, otherwise.</returns>
        public bool DeleteCellComment(System.Collections.Specialized.StringCollection elements)
        {
            if (Dimensions == null)
            {
                return false;
            }
            if (_dimensions.Count != elements.Count)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }
            return NativeOlapApi.CubeDeleteCellComment(_server.Store.ClientSlot, _serverHandle, _name, elements, _server.LastErrorInternal);
        }

        /// <summary>
        /// Deletes the comment of a cell of the cube.
        /// </summary>
        /// <param name="firstElement">The first element name.</param>
        /// <param name="secondElement">The second element name.</param>
        /// <param name="elements">A variable list of additional dimension elements that reference the cell.</param>
        /// <returns>True, if the comment has been deleted or false, otherwise.</returns>
        public bool DeleteCellComment(string firstElement, string secondElement, params string[] elements)
        {
            if (Dimensions == null)
            {
                return false;
            }

            System.Collections.Specialized.StringCollection elementNames = new System.Collections.Specialized.StringCollection();
            elementNames.Add(firstElement);
            elementNames.Add(secondElement);
            if (elements != null)
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    elementNames.Add(elements[i]);
                }
            }
            if (_dimensions.Count != elementNames.Count)
            {
                throw new System.ArgumentException(CreateExceptionString(_elementMismatch));
            }
            bool result = NativeOlapApi.CubeDeleteCellComment(_server.Store.ClientSlot, _serverHandle, _name, elementNames, _server.LastErrorInternal);
            elementNames = null;
            return result;
        }

        /// <summary>
        /// Sends an OLAP XML splash request to an OLAP server to write a value to a calculated cell.
        /// </summary>
        /// <param name="value">The value to splash.</param>
        /// <param name="mode">The splash mode.</param>
        /// <param name="rounding">True, if values should be rounded, false if not.</param>
        /// <param name="decimals">The number of decimals to round, if rounding is true.</param>
        /// <param name="notDeleteOnZero">If true, sending the value 0 will not delete the leaf cells, but it will write the value 0. Otherwise sending the value 0 will delete the leaf cells.</param>
        /// <param name="firstElement">The first element name.</param>
        /// <param name="secondElement">The second element name.</param>
        /// <param name="elements">A variable list of additional dimension elements that reference the cell.</param>
        /// <returns>True, if successful; false, otherwise.</returns>
        public bool SplashCell(double value, string mode, bool rounding, int decimals, bool notDeleteOnZero, string firstElement, string secondElement, string[] elements)
        {
            return NativeOlapApi.SplashValue(_server.Store.ClientSlot, _serverHandle, "1", this, value, mode, rounding, decimals, notDeleteOnZero, firstElement, secondElement, elements);
        }

        /// <summary>
        /// Creates a data area on the cube for the specified elements.
        /// </summary>
        /// <param name="dimensionElements">A two-dimensional list of element name that define
        /// the data area.</param>
        /// <returns>A data area with specified settings.</returns>
        public OlapDataArea CreateDataArea(System.Collections.Specialized.StringCollection[] dimensionElements)
        {
            return new OlapDataArea(this, dimensionElements);
        }

        /// <summary>
        /// Creates a data area on the cube with all dimension elements selected.
        /// </summary>
        /// <returns>A data area with specified settings.</returns>
        public OlapDataArea CreateDataArea()
        {
            return new OlapDataArea(this);
        }

        /// <summary>
        /// Gets additional information about the cube.
        /// </summary>
        /// <returns>An instance of the OlapCubeInformation class. If, for any reasons the values
        /// could not be read, the result is null.</returns>
        public OlapCubeInformation CubeInformation
        {
            get
            {
                if (_cubeInformation == null)
                {
                    _cubeInformation = NativeOlapApi.CubeInformation(_server.Store.ClientSlot, _serverHandle, _name, _server.LastErrorInternal);
                }
                return _cubeInformation;
            }
        }

        /// <summary>
        /// Gets the name of the cube.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the uppercased name of the element.
        /// </summary>
        public string UpcasedName
        {
            get
            {
                if (_upperName != null)
                {
                    _upperName = _name.ToUpper();
                }
                return _upperName;
            }
        }

        /// <summary>
        /// Gets the server to which the cube belongs to.
        /// </summary>
        /// <returns> </returns>
        public OlapServer Server
        {
            get
            {
                return _server;
            }
        }

        /// <summary>
        /// Gets the dimensions that belong to the cube.
        /// </summary>
        public OlapDimensions Dimensions
        {
            get
            {
                if (_dimensions == null)
                {
                    _dimensions = new OlapDimensions(_server, _name);
                }

                return _dimensions;
            }
        }

        /// <summary>
        /// Returns a string that represents the cube.
        /// </summary>
        /// <returns>A sring that represents the cube.</returns>
        public override string ToString()
        {
            return _name;
        }
    }
}