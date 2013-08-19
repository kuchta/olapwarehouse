namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Workaround for passing managed reference to unmanaged methods.
    /// </summary>
    public class IntPointer
    {
        private int _val;

        /// <summary>
        /// Initializes a new instance of the IntPointer class.
        /// </summary>
        public IntPointer()
        {
            _val = 0;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public int Value
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
