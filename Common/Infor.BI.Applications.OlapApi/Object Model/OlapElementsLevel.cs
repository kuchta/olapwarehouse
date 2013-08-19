namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines the element levels that can be represented by an Olap elements collection.
    /// </summary>
    public enum OlapElementsLevel
    {
        /// <summary>
        /// Provide all elements.
        /// </summary>
        OlapElementsLevelAll = 0,

        /// <summary>
        /// Provide parent elements.
        /// </summary>
        OlapElementsLevelParents = 1,

        /// <summary>
        /// Provide child elements.
        /// </summary>
        OlapElementsLevelChildren = 2,

        /// <summary>
        /// Provide top level elements.
        /// </summary>
        OlapElementsLevelTopLevel = 3,

        /// <summary>
        /// Provide subset elements.
        /// </summary>
        OlapElementsLevelSubset = 4,

        /// <summary>
        /// Provide flat elements.
        /// </summary>
        OlapElementsLevelFlat = 5
    }
}