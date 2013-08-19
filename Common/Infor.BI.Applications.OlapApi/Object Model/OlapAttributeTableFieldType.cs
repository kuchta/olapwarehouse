namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines the types of an attribute table field.
    /// </summary>
    public enum OlapAttributeTableFieldType
    {
        /// <summary>
        /// A text field.
        /// </summary>
        OlapAttributeTableFieldTypeCharacter = 1,

        /// <summary>
        /// A numeric field.
        /// </summary>
        OlapAttributeTableFieldTypeNumeric = 2,

        /// <summary>
        /// A field containing a date.
        /// </summary>
        OlapAttributeTableFieldTypeDate = 3,

        /// <summary>
        /// A boolean field.
        /// </summary>
        OlapAttributeTableFieldTypeLogical = 4
    }
}