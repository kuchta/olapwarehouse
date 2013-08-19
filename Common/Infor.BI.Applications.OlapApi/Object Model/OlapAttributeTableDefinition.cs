namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines information for an Olap attribute table.
    /// </summary>
    public class OlapAttributeTableDefintion
    {
        public int Id
        {
            get;
            set;
        }

        public int TimeStamp
        {
            get;
            set;
        }

        public int RecordCount
        {
            get;
            set;
        }

        public int FieldCount
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}