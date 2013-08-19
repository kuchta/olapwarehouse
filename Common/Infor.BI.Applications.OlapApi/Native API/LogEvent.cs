namespace Infor.BI.Applications.OlapApi.Native
{
    /// <summary>
    /// Provides log event ids for the Olap API.
    /// </summary>
    public enum LogEvent : int
    {
        /// <summary>
        /// The base id for errors in the Olap API.
        /// </summary>
        ErrorBase = 1500000,

        /// <summary>
        /// Gets logged if the Olap server returns an error document.
        /// </summary>
        ErrorInXmlResponse = ErrorBase + 1,

        /// <summary>
        /// Gets logged if the Olap server's response contains no element for a native name.
        /// </summary>
        ResponseDoesNotContainNativeNameElement = ErrorBase + 2,

        /// <summary>
        /// Gets logged if the Olap server's response contains a native name element with an error attribute.
        /// </summary>
        NameElementHasErrorAttribute = ErrorBase + 3,

        /// <summary>
        /// Gets logged if the Olap server's response contains no attribute for a native name.
        /// </summary>
        ResponseDoesNotContainNativeNameAttribute = ErrorBase + 4,

        /// <summary>
        /// Gets logged if a connection to an OLAP server could not be destroyed.
        /// </summary>
        CouldNotDestroyOlapServer = ErrorBase + 5
    }
}