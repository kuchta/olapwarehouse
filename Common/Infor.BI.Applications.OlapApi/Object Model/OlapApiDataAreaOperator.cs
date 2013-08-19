namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines operators for the Olap data area functionality.
    /// </summary>
    public enum OlapApiDataAreaOperator
    {
        /// <summary>
        /// No operator.
        /// </summary>
        OlapDataAreaOperatorNone = 0,

        /// <summary>
        /// The equal operator.
        /// </summary>
        OlapDataAreaOperatorEqual = 1,

        /// <summary>
        /// The greater or equal operator.
        /// </summary>
        OlapDataAreaOperatorGreaterOrEqual = 2,

        /// <summary>
        /// The greater operator.
        /// </summary>
        OlapDataAreaOperatorGreater = 3,

        /// <summary>
        /// The less or equal operator.
        /// </summary>
        OlapDataAreaOperatorLessOrEqual = 4,

        /// <summary>
        /// The less operator.
        /// </summary>
        OlapDataAreaOperatorLess = 5,

        /// <summary>
        /// The not equal operator.
        /// </summary>
        OlapDataAreaOperatorNotEqual = 6,

        /// <summary>
        /// The like operator.
        /// </summary>
        OlapDataAreaOperatorLike = 7
    }
}
