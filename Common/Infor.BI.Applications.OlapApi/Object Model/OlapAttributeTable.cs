namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents an Olap attribute table.
    /// </summary>
    public class OlapAttributeTable : OlapObjectBase
    {
        /// <summary>
        /// Holds the Olap dimension to which the attribute table belongs.
        /// </summary>
        private OlapDimension _dimension;

        /// <summary>
        /// Holds a collection of fields that belong to the attribute table.
        /// </summary>
        private OlapAttributeTableFields _fields;

        /// <summary>
        /// Holds the name of the attribute table.
        /// </summary>
        private string _name;

        /// <summary>
        /// Holds the id of the attribute table.
        /// </summary>
        private int _id;

        /// <summary>
        /// Holds the number of fields of the table.
        /// </summary>
        private int _fieldCount;

        /// <summary>
        /// Holds the number of records of the table.
        /// </summary>
        private int _recordCount;

        /// <summary>
        /// Initializes a new instance of the OlapAttributeTable class.
        /// </summary>
        /// <param name="dimension">The Olap dimension to which the attribute table belongs.</param>
        /// <param name="name">The name of the attribute table.</param>
        /// <param name="id">The id of the attribute table, i.e. the index of the table as it is stored in
        /// Olap.</param>
        /// <param name="fieldCount">The number of fields of the table.</param>
        /// <param name="recordCount">The number of records in the table.</param>
        public OlapAttributeTable(OlapDimension dimension, string name, int id, int fieldCount, int recordCount)
        {
            _dimension = dimension;
            _name = name;
            _id = id;
            _fieldCount = fieldCount;
            _recordCount = recordCount;
        }

        /// <summary>
        /// Gets the dimension the attribute table belongs to.
        /// </summary>
        public OlapDimension Dimension
        {
            get
            {
                return _dimension;
            }
        }

        /// <summary>
        /// Gets the attribute table name.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the attribute table id, i.e. the index of the table as it is stored in
        /// Olap.
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets the number of fields of the table.
        /// </summary>
        public int FieldCount
        {
            get
            {
                return _fieldCount;
            }
        }

        /// <summary>
        /// Gets the number of record in the table.
        /// </summary>
        public int RecordCount
        {
            get
            {
                return _recordCount;
            }
        }

        /// <summary>
        /// Gets the fields that belong to the attribute table.
        /// </summary>
        public OlapAttributeTableFields Fields
        {
            get
            {
                if (_fields == null)
                {
                    _fields = new OlapAttributeTableFields(this);
                }
                return _fields;
            }
        }
    }
}
