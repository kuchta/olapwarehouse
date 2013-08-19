namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines an interface for classes doing transformations on OlapCell objects.
    /// </summary>
    public interface IOlapCellTransformer
    {
        /// <summary>
        /// Transforms an OlapCell object.
        /// </summary>
        /// <param name="cell">Object to be transformed.</param>
        void Transform(OlapCell cell);
    }
}
