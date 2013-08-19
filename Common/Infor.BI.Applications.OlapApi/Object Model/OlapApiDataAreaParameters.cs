namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines additional parameters for a data area definition.
    /// </summary>
    public class OlapApiDataAreaParameters
    {
        /// <summary>
        /// Holds a combination of flags that define the element types to retrieve from the data area.
        /// </summary>
        private bool _suppressConsolidated;

        /// <summary>
        /// The first operator to restrict the results.
        /// </summary>
        private OlapApiDataAreaOperator _firstOperator;

        /// <summary>
        /// The second operator to restrict the results.
        /// </summary>
        private OlapApiDataAreaOperator _secondOperator;

        /// <summary>
        /// The first value corresponding to the first operator used to restrict the results.
        /// </summary>
        private double _firstValue;

        /// <summary>
        /// The second value corresponding to the second operator used to restrict the results.
        /// </summary>
        private double _secondValue;

        /// <summary>
        /// A flag that indicates whether to suppress empty cells.
        /// </summary>
        private bool _suppressNull;

        /// <summary>
        /// A flag that indicates whether only base values are to return.
        /// </summary>
        private bool _baseValuesOnly;

        /// <summary>
        /// A flag that indicates whether string value are to quote.
        /// </summary>
        private bool _quoteStrings;

        /// <summary>
        /// A flag that indicates whether to handle NA the same as zero in hash calculations.
        /// </summary>
        private bool _treatZeroAsNa;

        /// <summary>
        /// The decimal limiter for numbers. Must be dot or comma.
        /// </summary>
        private char _decimalDelimiter;

        /// <summary>
        /// If true, text cells will be excluded.
        /// </summary>
        private bool _excludeTextElements;

        /// <summary>
        /// Creates a new instance of the OlapDataAreaParameters class.
        /// <br></br>
        /// The operators and corresponding values to restrict the results are set to 0
        /// and operator none, so no restrictions are defined.
        /// Base values are included, string are quoted, null values are suppressed
        /// by default. The default decimal delimiter is '.'.
        /// </summary>
        public OlapApiDataAreaParameters()
        {
            _firstOperator = OlapApiDataAreaOperator.OlapDataAreaOperatorNone;
            _secondOperator = OlapApiDataAreaOperator.OlapDataAreaOperatorNone;
            _firstValue = 0;
            _secondValue = 0;
            _baseValuesOnly = false;
            _quoteStrings = false;
            _decimalDelimiter = '.';
            _suppressNull = true;
            _suppressConsolidated = false;
            _excludeTextElements = false;
        }

        /// <summary>
        /// Gets the first operator to restrict the results.
        /// </summary>
        public OlapApiDataAreaOperator FirstOperator
        {
            get
            {
                return _firstOperator;
            }

            set
            {
                _firstOperator = value;
            }
        }

        /// <summary>
        /// Gets the second operator to restrict the results.
        /// </summary>
        public OlapApiDataAreaOperator SecondOperator
        {
            get
            {
                return _secondOperator;
            }

            set
            {
                _secondOperator = value;
            }
        }

        /// <summary>
        /// Gets the first value corresponding to the first operator used to restrict the results.
        /// </summary>
        public double FirstValue
        {
            get
            {
                return _firstValue;
            }

            set
            {
                _firstValue = value;
            }
        }

        /// <summary>
        /// Gets the second value corresponding to the second operator used to restrict the results.
        /// </summary>
        public double SecondValue
        {
            get
            {
                return _secondValue;
            }

            set
            {
                _secondValue = value;
            }
        }

        /// <summary>
        /// Gets a flag that indicates whether to suppress empty cells.
        /// </summary>
        public bool SuppressNullValue
        {
            get
            {
                return _suppressNull;
            }

            set
            {
                _suppressNull = value;
            }
        }

        /// <summary>
        /// Gets a flag that indicates whether to suppress text cells.
        /// </summary>
        public bool SuppressTextValues
        {
            get
            {
                return _excludeTextElements;
            }

            set
            {
                _excludeTextElements = value;
            }
        }

        /// <summary>
        /// Gets a flag that indicates whether to suppress consolidated elements.
        /// </summary>
        public bool SuppressConsolidated
        {
            get
            {
                return _suppressConsolidated;
            }

            set
            {
                _suppressConsolidated = value;
            }
        }

        /// <summary>
        /// Gets a flag that indicates whether only base values are to return.
        /// </summary>
        public bool BaseValuesOnly
        {
            get
            {
                return _baseValuesOnly;
            }

            set
            {
                _baseValuesOnly = value;
            }
        }

        /// <summary>
        /// Gets a flag that indicates whether string value are to quote.
        /// </summary>
        public bool QuoteStrings
        {
            get
            {
                return _quoteStrings;
            }

            set
            {
                _quoteStrings = value;
            }
        }

        /// <summary>
        /// Gets a flag that indicates whether to handle zero as NA in hash calculation.
        /// </summary>
        public bool TreatZeroAsNA
        {
            get
            {
                return _treatZeroAsNa;
            }

            set
            {
                _treatZeroAsNa = value;
            }
        }

        /// <summary>
        /// Gets the decimal limiter for numbers. Must be dot or comma.
        /// </summary>
        public char DecimalDelimiter
        {
            get
            {
                return _decimalDelimiter;
            }

            set
            {
                if (value != '.' || value != '.')
                {
                    // TODO 10.5: from resource
                    throw new OlapException("Invalid decimal separator!");
                }
                _decimalDelimiter = value;
            }
        }
    }
}
