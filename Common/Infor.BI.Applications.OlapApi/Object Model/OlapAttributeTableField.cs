namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents an Olap attribute table field.
    /// </summary>
    public class OlapAttributeTableField : OlapObjectBase
    {
        /// <summary>
        /// Holds the attribute table that owns the field.
        /// </summary>
        private OlapAttributeTable _attributeTable;

        /// <summary>
        /// Holds the type of the field.
        /// </summary>
        private OlapAttributeTableFieldType _type;

        /// <summary>
        /// Holds the name of the attribute table.
        /// </summary>
        private string _name;

        /// <summary>
        /// Holds the id of the attribute table field.
        /// </summary>
        private int _id;

        /// <summary>
        /// Holds the witdh of the attribute table field.
        /// </summary>
        private int _fieldWidth;

        /// <summary>
        /// Holds the decimals of the attribute table field.
        /// </summary>
        private int _decimals;

        /// <summary>
        /// Initializes a new instance of the OlapAttributeTable class.
        /// </summary>
        /// <param name="attributeTable">The attribute table to which the attribute field belongs.</param>
        /// <param name="name">The name of the attribute table field.</param>
        /// <param name="id">The id of the attribute table field.</param>
        /// <param name="fieldWidth">The width in bytes of the attribute table field.</param>
        /// <param name="decimals">The number of decimals of the attribute table field.</param>
        /// <param name="type">The type of the attribute table field.</param>
        public OlapAttributeTableField(OlapAttributeTable attributeTable, string name, int id, int fieldWidth, int decimals, OlapAttributeTableFieldType type)
        {
            _attributeTable = attributeTable;
            _name = name;
            _id = id;
            _fieldWidth = fieldWidth;
            _decimals = decimals;
            _type = type;
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
        /// Gets the type of the attribute field.
        /// </summary>
        public OlapAttributeTableFieldType Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Returns the number of decimals in case of the field type is numeric.
        /// </summary>
        public int Decimals
        {
            get
            {
                return _decimals;
            }
        }

        /// <summary>
        /// Gets the width of the total number including the decimal point in case of the field type is numeric.
        /// </summary>
        public int Width
        {
            get
            {
                return _fieldWidth;
            }
        }
    }
}
