namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines the base class for all Olap related collection classes.
    /// </summary>
    /// <typeparam name="T">Base type to create a collection for.</typeparam>
    public class OlapCollectionObjectBase<T> : OlapObjectBase
    {
        /// <summary>
        /// Holds a flag that indicates if the collection has been initialized.
        /// </summary>
        private bool _initialized;

        protected bool Initialized
        {
            get
            {
                return _initialized;
            }

            set
            {
                _initialized = value;
            }
        }

        /// <summary>
        /// Holds a flag that indicates if the collection is invalid.
        /// </summary>
        private bool _invalid;

        /// <summary>
        /// Holds the collection.
        /// </summary>
        private System.Collections.Generic.List<T> _collection;

        protected System.Collections.Generic.List<T> Collection
        {
            get
            {
                return _collection;
            }

            set
            {
                _collection = value;
            }
        }

        /// <summary>
        /// Sets a flag that indicates whether the collection has been initialized.
        /// </summary>
        /// <param name="value">True, if initialized; false, if not. </param>
        protected bool IsInitialized
        {
            get
            {
                return _initialized;
            }

            set
            {
                _initialized = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the OlapCollectionObjectBase class.
        /// </summary>
        public OlapCollectionObjectBase()
        {
            _collection = new System.Collections.Generic.List<T>(0);
            _invalid = true;
            _initialized = false;
        }

        /// <summary>
        /// Gets a flag that indicates if the collection is invalid.
        /// </summary>
        /// <returns>A flag that indicates if the collection is invalid.</returns>
        public bool Invalid
        {
            get
            {
                return _invalid;
            }

            set
            {
                _invalid = value;
            }
        }

        /// <summary>
        /// Gets a flag that indicates whether the specified object is contained in the
        /// collection or not.
        /// </summary>
        /// <param name="obj">The object to look for.</param>
        /// <returns>True, if the object is contained; false, otherwise.</returns>
        public bool Contains(T obj)
        {
            if (_initialized && !_invalid)
            {
                return _collection.Contains(obj);
            }
            return false;
        }
    }
}
