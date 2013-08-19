namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines the types of a subset.
    /// </summary>
    public enum OlapSubsetTypes
    {
        /// <summary>
        /// Global subset.
        /// </summary>
        OlapSubsetTypesPublic = 0x01,

        /// <summary>
        /// Private subset.
        /// </summary>
        OlapSubsetTypesPrivate = 0x02,

        /// <summary>
        /// Public and private subsets.
        /// </summary>
        OlapSubsetTypesAllSubsets = 0x04,

        /// <summary>
        /// Static subset.
        /// </summary>
        OlapSubsetTypesStatic = 0x08,

        /// <summary>
        /// Data query subset.
        /// </summary>
        OlapSubsetTypesDataQuery = 0x10,

        /// <summary>
        /// Atribute query subset.
        /// </summary>
        OlapSubsetTypesAttributeQuery = 0x20
    }
}