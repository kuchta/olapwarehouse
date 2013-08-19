using Infor.BI.Applications.OlapApi.Native;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents an Olap data area.
    /// </summary>
    public class OlapDataArea : System.IDisposable, System.Collections.IEnumerable
    {
        /// <summary>
        /// Defines the additional data area parameters.
        /// </summary>
        private OlapApiDataAreaParameters _parameters;

        /// <summary>
        /// Holds the cube for which the data area is defined.
        /// </summary>
        private OlapCube _cube;

        /// <summary>
        /// Holds a flag that indicates if the object has already been disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Holds the elements table that define the data area.
        /// </summary>
        private System.Collections.Specialized.StringCollection[] _elements;

        /// <summary>
        /// Holds the data returned from the data area.
        /// </summary>
        private System.Collections.ArrayList _data;

        /// <summary>
        /// Holds a pointer to the last returned data set.
        /// </summary>
        private int _currentDataSet;

        /// <summary>
        /// Holds the current cell of the data area.
        /// </summary>
        private OlapCell _currentCell;

        /// <summary>
        /// Flag which indicates whether the data area has been activated.
        /// </summary>
        private bool _isActivated;

        /// <summary>
        /// A default object cannot be instantiated.
        /// </summary>
        private OlapDataArea()
        {
        }

        /// <summary>
        /// Resets the internal data.
        /// </summary>
        private void Reset()
        {
            _disposed = false;
            _cube = null;
            _parameters = new OlapApiDataAreaParameters();
            _data = null;
            _currentDataSet = -1;
            _elements = null;
            _isActivated = false;
        }

        private void MdsSetValueProlog()
        {
            if (_isActivated)
            {
                Deactivate();
                _isActivated = true;
            }

            if (!NativeOlapApi.DataAreaDefine(_cube.Server.Store.ClientSlot, _cube.Server.ServerHandle, _cube.Name, _elements, _parameters, _cube.Server.DataAreaActive, _cube.Server.LastErrorInternal))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("The data area could not be defined!");
                sb.Append(", Error: ");
                sb.Append(System.Convert.ToString(_cube.Server.LastErrorInternal.Value));
                sb.Append(", ");
                sb.Append(ToString());
                throw new OlapException(sb.ToString(), _cube.Server.LastErrorInternal.Value);
            }
        }

        private void MdsSetValueEpilog()
        {
            Deactivate();

            if (_isActivated)
            {
                Activate();
            }
        }

        /// <summary>
        /// Creates a new instance of the OlapDataArea class. This version of the constructor
        /// creates a data area that selects all elements of all dimensions of the cube.
        /// </summary>
        /// <param name="cube">The cube for which to create the data area.</param>
        public OlapDataArea(OlapCube cube)
        {
            Reset();
            _cube = cube;
            int dimensions = _cube.Dimensions.Count;
            _elements = new System.Collections.Specialized.StringCollection[dimensions];

            // select all dimension elements
            for (int i = 0; i < dimensions; i++)
            {
                _elements[i] = new System.Collections.Specialized.StringCollection();
                _elements[i].Add("*");
            }
        }

        /// <summary>
        /// Creates a new instance of the OlapDataArea class. This version of the constructor
        /// creates a data area that selects the specified elements.
        /// </summary>
        /// <param name="cube">The cube for which to create the data area.</param>
        /// <param name="elements">An array of string collections where each array
        /// field represents a column for the dimension elements of th i-th dimension
        /// of the specified cube.</param>
        public OlapDataArea(OlapCube cube, System.Collections.Specialized.StringCollection[] elements)
        {
            Reset();
            _cube = cube;
            _elements = elements;
        }

        /// <summary>
        /// Destroy the object and releases all resources.
        /// </summary>
        public void Dispose()
        {
            FreeResources();
        }

        /// <summary>
        /// Activates the data area on the specified cube on the Olap server. This method can be called only
        /// once and deactivate must be called when done. If activate is called two times without calling deactivate
        /// in between an exception is thrown.
        /// </summary>
        public void Activate()
        {
            if (!NativeOlapApi.DataAreaDefine(_cube.Server.Store.ClientSlot, _cube.Server.ServerHandle, _cube.Name, _elements, _parameters, _cube.Server.DataAreaActive, _cube.Server.LastErrorInternal))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("The data area could not be activated!");
                sb.Append(", Error: ");
                sb.Append(System.Convert.ToString(_cube.Server.LastErrorInternal.Value));
                sb.Append(", ");
                sb.Append(ToString());
                throw new OlapException(sb.ToString(), _cube.Server.LastErrorInternal.Value);
            }
            _data = NativeOlapApi.DataAreaFirst(_cube.Server.Store.ClientSlot, _cube.Server.ServerHandle, _cube.Name, _parameters, _cube.Server.LastErrorInternal);
            _currentDataSet = -1;
            _isActivated = true;
        }

        /// <summary>
        /// Deactivates the data area on the specified cube on the Olap server.
        /// </summary>
        public void Deactivate()
        {
            if (_isActivated)
            {
                NativeOlapApi.DataAreaDestroy(_cube.Server.Store.ClientSlot, _cube.Server.ServerHandle, _parameters, _cube.Server.DataAreaActive, _cube.Server.LastErrorInternal);
                _isActivated = false;
            }
        }

        /// <summary>
        /// Deletes all values in the data area.
        /// </summary>
        public void DeleteValues()
        {
            MdsSetValueProlog();

            if (!NativeOlapApi.DataAreaDelete(_cube.Server.Store.ClientSlot, _cube.Server.ServerHandle, _parameters, _cube.Server.LastErrorInternal))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("The data area values could not be deleted!");
                sb.Append(", Error: ");
                sb.Append(System.Convert.ToString(_cube.Server.LastErrorInternal.Value));
                sb.Append(", ");
                sb.Append(ToString());
                throw new OlapException(sb.ToString(), _cube.Server.LastErrorInternal.Value);
            }

            MdsSetValueEpilog();
        }

        /// <summary>
        /// Set values in the data area.
        /// </summary>
        /// <param name="value">Value which is to be set.</param>
        public void SetValues(double value)
        {
            MdsSetValueProlog();

            if (!NativeOlapApi.DataAreaSetValues(_cube.Server.Store.ClientSlot, _cube.Server.ServerHandle, _parameters, value, _cube.Server.LastErrorInternal))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("The data area values could not be set!");
                sb.Append(", Error: ");
                sb.Append(System.Convert.ToString(_cube.Server.LastErrorInternal.Value));
                sb.Append(", ");
                sb.Append(ToString());
                throw new OlapException(sb.ToString(), _cube.Server.LastErrorInternal.Value);
            }

            MdsSetValueEpilog();
        }

        /// <summary>
        /// Set values in the data area.
        /// </summary>
        /// <param name="value">Value which is to be set.</param>
        public void SetValues(string value)
        {
            MdsSetValueProlog();

            if (!NativeOlapApi.DataAreaSetValues(_cube.Server.Store.ClientSlot, _cube.Server.ServerHandle, _parameters, value, _cube.Server.LastErrorInternal))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("The data area values could not be set!");
                sb.Append(", Error: ");
                sb.Append(System.Convert.ToString(_cube.Server.LastErrorInternal.Value));
                sb.Append(", ");
                sb.Append(ToString());
                throw new OlapException(sb.ToString(), _cube.Server.LastErrorInternal.Value);
            }

            MdsSetValueEpilog();
        }

        /// <summary>
        /// Calculate the hash value for the data area.
        /// </summary>
        /// <param name="key">A key to calculate the hash. The default value is an empty string.</param>
        /// <param name="treatZeroAsNa">If true, the hash calculation will produce the same hash values for #NA and zero, or #NA and empty string for text cells.</param>
        /// <returns>A unique hash value for the data area.</returns>
        public string CalculateHash(string key, bool treatZeroAsNa)
        {
            MdsSetValueProlog();

            string result = NativeOlapApi.DataAreaCalculateHash(_cube.Server.Store.ClientSlot, _cube.Server.ServerHandle, _parameters, key, _cube.Server.LastErrorInternal);

            if (_cube.Server.LastErrorInternal.Value != 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("The hash value could not be calculated!");
                sb.Append(", Error: ");
                sb.Append(System.Convert.ToString(_cube.Server.LastErrorInternal.Value));
                sb.Append(", ");
                sb.Append(ToString());
                throw new OlapException(sb.ToString(), _cube.Server.LastErrorInternal.Value);
            }

            MdsSetValueEpilog();
            _isActivated = false;
            _cube.Server.DataAreaActive.Value = false;

            return result;
        }

        /// <summary>
        /// Gets the next data set from the data area. If there is no more data the method
        /// returns 0. Before using this method Activate must be called.
        /// </summary>
        public OlapCell NextData
        {
            get
            {
                if (_data != null)
                {
                    // as long as we have cached elements return them
                    if (_currentDataSet < _data.Count - 1)
                    {
                        _currentDataSet++;
                        System.Collections.Specialized.StringCollection list = (System.Collections.Specialized.StringCollection)_data[_currentDataSet];
                        object val = list[list.Count - 1];
                        list.RemoveAt(list.Count - 1);
                        OlapCell cell = new OlapCell(_cube, list);
                        cell.SetCachedValue(val);
                        _currentCell = cell;
                        return (OlapCell)cell.Clone();
                    }
                    // check, if there is more data
                    _data = NativeOlapApi.DataAreaNext(_cube.Server.Store.ClientSlot, _cube.Server.ServerHandle, _parameters, _cube.Server.LastErrorInternal);
                    _currentDataSet = -1;
                    if (_data != null)
                    {
                        return NextData;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the current data set from the data area. If NextData has not been called before the
        /// method will always return null. This method will return a cloned version of the actuall cell.
        /// </summary>
        public OlapCell CurrentData
        {
            get
            {
                if (_currentCell == null)
                {
                    return null;
                }

                return (OlapCell)_currentCell.Clone();
            }
        }

        /// <summary>
        /// Gets the parameters object that defines additional parameters for the data area.
        /// </summary>
        public OlapApiDataAreaParameters Parameters
        {
            get
            {
                return _parameters;
            }
        }

        /// <summary>
        /// Gets the elements that define the data area.
        /// </summary>
        public System.Collections.Specialized.StringCollection[] Elements
        {
            get
            {
                return _elements;
            }
        }

        /// <summary>
        /// Gets an IEnumerator to iterate through the data area.
        /// </summary>
        /// <returns>Enumerator for looping over the data area.</returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            return new OlapDataAreaEnumerator(this);
        }

        /// <summary>
        /// Creates a string that includes information about the dataarea.
        /// </summary>
        /// <returns>A string containing information about the dataarea.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();

            result.Append("Cube: ");
            if (_cube != null)
            {
                result.Append(_cube.Name);
            }
            else
            {
                result.Append("not defined");
            }

            result.Append(", Elements:");

            if (_elements != null)
            {
                for (int i = 0; i < _elements.Length; i++)
                {
                    result.Append("(");

                    for (int j = 0; j < _elements[i].Count; j++)
                    {
                        result.Append("\"");
                        result.Append(_elements[i][j]);
                        result.Append("\"");
                        if (j < _elements[i].Count - 1)
                        {
                            result.Append(",");
                        }
                    }

                    result.Append(")");
                    if (i < _elements.Length - 1)
                    {
                        result.Append(",");
                    }
                }
            }
            else
            {
                result.Append(")");
            }

            return result.ToString();
        }

        /// <summary>
        /// Frees resources immediately.
        /// </summary>
        public void FreeResources()
        {
            if (!_disposed)
            {
                _disposed = true;
                Deactivate();
                Reset();
                _disposed = true; // was changed by reset
            }
        }
    }
}
