namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Implements an iterator over an OlapDataArea.
    /// </summary>
    public class OlapDataAreaEnumerator : System.Collections.IEnumerator
    {
        /// <summary>
        /// Holds the data area to iterate over.
        /// </summary>
        private OlapDataArea _dataArea;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="dataArea">The data area to iterate over.</param>
        public OlapDataAreaEnumerator(OlapDataArea dataArea)
        {
            _dataArea = dataArea;
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>True if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        public bool MoveNext()
        {
            return _dataArea.NextData != null;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection. 
        /// </summary>
        public void Reset()
        {
            _dataArea.Deactivate();
            _dataArea.Activate();
        }

        /// <summary>
        /// Applies a transformer to the entire data area. The data area will be reset and iteration started from
        /// the beginning no matter what was the state before the call. After the call the enumerator will point
        /// after the end of the collection.
        /// </summary>
        /// <param name="transformer">A transformer implementation to apply to the data area.</param>
        public void ForEach(IOlapCellTransformer transformer)
        {
            Reset();
            while (MoveNext())
            {
                ((OlapCell)Current).Transform(transformer);
            }
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns> </returns>
        public object Current
        {
            get
            {
                return _dataArea.CurrentData;
            }
        }
    }
}
