namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents an Olap subset.
    /// </summary>
    public class OlapSubset : OlapObjectBase
    {
        /// <summary>
        /// Holds the dimension that owns the subset.
        /// </summary>
        private OlapDimension _dimension;

        /// <summary>
        /// Holds the type of the subset.
        /// </summary>
        private OlapSubsetTypes _type;

        /// <summary>
        /// Holds a flag that indicates whether the subset saves the result set or not.
        /// </summary>
        private bool _saveResultSet;

        /// <summary>
        /// Holds the reference name of the subset.
        /// </summary>
        private string _referenceName;

        /// <summary>
        /// Holds the long name of the subset.
        /// </summary>
        private string _longName;

        /// <summary>
        /// The name of the user which created the subset.
        /// </summary>
        private string _createdByUser;

        /// <summary>
        /// Holds the elements of the subset.
        /// </summary>
        private OlapElements _elements;

        /// <summary>
        /// Initializes a new instance of the OlapSubset class.
        /// </summary>
        /// <param name="dimension">The dimension that owns the subset.</param>
        /// <param name="refName">The reference name of the subset.</param>
        /// <param name="longName">The long name of the subset.</param>
        /// <param name="userName">The name of the user that created the subset.</param>
        /// <param name="type">The type of the subset.</param>
        /// <param name="saveResultSet">A flag that indicates whether the subset save the result sets or not.</param>
        public OlapSubset(OlapDimension dimension, string refName, string longName, string userName, OlapSubsetTypes type, bool saveResultSet)
        {
            _createdByUser = userName;
            _dimension = dimension;
            _longName = longName;
            _referenceName = refName;
            _saveResultSet = saveResultSet;
            _type = type;
        }

        /// <summary>
        /// Gets the reference name of the subset.
        /// </summary>
        public string ReferenceName
        {
            get
            {
                return _referenceName;
            }
        }

        /// <summary>
        /// Gets the long name of the subset.
        /// </summary>
        public string LongName
        {
            get
            {
                return _longName;
            }
        }

        /// <summary>
        /// Gets the name of the user which created the subset.
        /// </summary>
        public string UserName
        {
            get
            {
                return _createdByUser;
            }
        }

        /// <summary>
        /// Gets the type of the subset.
        /// </summary>
        public OlapSubsetTypes SubsetType
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Gets a flag that indicates whether subset save result sets or not.
        /// </summary>
        public bool SaveResultSets
        {
            get
            {
                return _saveResultSet;
            }
        }

        /// <summary>
        /// Gets the elements of the subset.
        /// </summary>
        public OlapElements Elements
        {
            get
            {
                if (_elements == null)
                {
                    _elements = new OlapElements(_dimension, this);
                }
                return _elements;
            }
        }
    }
}
