namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines information for an Olap attribute table field.
    /// </summary>
    public class OlapAttributeTableFieldDefinition
    {
        public char Type
        {
            get;
            set;
        }

        public string FieldName
        {
            get;
            set;
        }

        public int FieldWidth
        {
            get;
            set;
        }

        public int Decimals
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }
    }
}
