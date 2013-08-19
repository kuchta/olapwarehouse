namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines information of an Olap element.
    /// </summary>
    public class OlapElementInformation
    {
        /// <summary>
        /// Holds the type of the element.
        /// </summary>
        private OlapElementType _elementType;

        /// <summary>
        /// Holds the number of parent elements.
        /// </summary>
        private int _parentCount;

        /// <summary>
        /// Holds the number of child elements.
        /// </summary>
        private int _childCount;

        /// <summary>
        /// Initializes a new instance of the OlapElementInformation class.
        /// </summary>
        /// <param name="elementType">The type of the element.</param>
        /// <param name="parentCount">The number of parent elements.</param>
        /// <param name="childCount">The number of child elements.</param>
        public OlapElementInformation(OlapElementType elementType, int parentCount, int childCount)
        {
            _elementType = elementType;
            _parentCount = parentCount;
            _childCount = childCount;
        }

        /// <summary>
        /// Gets the type of the element.
        /// </summary>
        public OlapElementType ElementType
        {
            get
            {
                return _elementType;
            }
        }

        /// <summary>
        /// Gets a flag that indicates if the element is a base element.
        /// </summary>
        public bool IsBaseElement
        {
            get
            {
                return _elementType == OlapElementType.OlapElementTypeBase;
            }
        }

        /// <summary>
        /// Gets a flag that indicates if the element is a consolidation element.
        /// </summary>
        public bool IsConsolidationElement
        {
            get
            {
                return _elementType == OlapElementType.OlapElementTypeConsolidation;
            }
        }

        /// <summary>
        /// Gets a flag that indicates if the element is a text element.
        /// </summary>
        public bool IsTextElement
        {
            get
            {
                return _elementType == OlapElementType.OlapElementTypeText;
            }
        }

        /// <summary>
        /// Gets a flag that indicates if the element is a rule element.
        /// </summary>
        public bool IsRuleElement
        {
            get
            {
                return _elementType == OlapElementType.OlapElementTypeRule;
            }
        }

        /// <summary>
        /// Gets the number of child elements.
        /// </summary>
        public int ChildCount
        {
            get
            {
                return _childCount;
            }
        }

        /// <summary>
        /// Gets the number of parent elements.
        /// </summary>
        public int ParentCount
        {
            get
            {
                return _parentCount;
            }
        }

        /// <summary>
        /// Creates the string that represents the data of this class.
        /// </summary>
        /// <returns>A string that represents the data of this class.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            result.Append("ElementType=");
            result.Append(_elementType);
            result.Append(", Parent Elements=");
            result.Append(_parentCount);
            result.Append(", Child Elements=");
            result.Append(_childCount);
            return result.ToString();
        }
    }
}
