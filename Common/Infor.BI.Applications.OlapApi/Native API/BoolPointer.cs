namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Workaround for passing managed reference to unmanaged methods.
    /// </summary>
    public class BoolPointer
    {
        private bool _val;

        /// <summary>
        /// Initializes a new instance of the BoolPointer class.
        /// </summary>
        public BoolPointer()
        {
            _val = false;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public bool Value
        {
            get
            {
                return _val;
            }

            set
            {
                _val = value;
            }
        }
    }
}
