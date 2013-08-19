namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines the types of an Olap element.
    /// </summary>
    public enum OlapElementType
    {
        /// <summary>
        /// Base element.
        /// </summary>
        OlapElementTypeBase = 1,

        /// <summary>
        /// Consolidated element.
        /// </summary>
        OlapElementTypeConsolidation = 2,

        /// <summary>
        /// Text element.
        /// </summary>
        OlapElementTypeText = 3,

        /// <summary>
        /// Rule element.
        /// </summary>
        OlapElementTypeRule = 4
    }
}