namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines information for an Olap subset.
    /// </summary>
    public class OlapSubsetDefinition
    {
        public int Type
        {
            get;
            set;
        }

        public bool SaveResultSet
        {
            get;
            set;
        }

        public string RefName
        {
            get;
            set;
        }

        public string LongName
        {
            get;
            set;
        }

        public string CreatedByUser
        {
            get;
            set;
        }
    }
}
