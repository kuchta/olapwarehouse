namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines information of an Olap dimension.
    /// </summary>
    public class OlapDimensionInformation
    {
        /// <summary>
        /// Holds a timestamp of when the dimension was updated the last time.
        /// </summary>
        private int _lastUpdate;

        /// <summary>
        /// Holds the name of the data access cube of the dimension.
        /// </summary>
        private string _dataAccessCubeName;

        /// <summary>
        /// Holds the default name of the dimension.
        /// </summary>
        private string _longName;

        /// <summary>
        /// Initializes a new instance of the OlapCubeInformation class.
        /// </summary>
        /// <param name="lastUpdate">When the dimension was updated the last time.</param>
        /// <param name="dataAccessCube">The corresponding data access cube.</param>
        /// <param name="longName">The dimension's long name.</param>
        public OlapDimensionInformation(int lastUpdate, string dataAccessCube, string longName)
        {
            _lastUpdate = lastUpdate;
            _dataAccessCubeName = dataAccessCube;
            _longName = longName;
        }

        /// <summary>
        /// Gets a timestamp when the dimension was updated the last time.
        /// </summary>
        public int LastUpdate
        {
            get
            {
                return _lastUpdate;
            }
        }

        /// <summary>
        /// Gets the long name of the dimensions. This is the connection dependend default 
        /// language specific name which can be defined in multiple languages.
        /// </summary>
        public string LongName
        {
            get
            {
                return _longName;
            }
        }

        /// <summary>
        /// Gets the data access cube name of the dimension.
        /// </summary>
        public string DataAccessCube
        {
            get
            {
                return _dataAccessCubeName;
            }
        }

        /// <summary>
        /// Creates the string that represents the data of this class.
        /// </summary>
        /// <returns>A string that represents the data of this class.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            result.Append("LastUpdate=");
            result.Append(_lastUpdate);
            return result.ToString();
        }
    }
}
