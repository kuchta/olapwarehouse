namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents a single cell of an Olap cube.
    /// </summary>
    public class OlapCell : OlapObjectBase, System.ICloneable
    {
        /// <summary>
        /// Holds the last read value.
        /// </summary>
        private object _cachedValue;

        /// <summary>
        /// Holds the cube the cell belongs to.
        /// </summary>
        private OlapCube _cube;

        /// <summary>
        /// Holds a collection of elements that reference the cell.
        /// </summary>
        private System.Collections.Specialized.StringCollection _elements;

        /// <summary>
        /// Creates a new instance of the OlapCell class.
        /// </summary>
        /// <param name="cube">The cube to which the cell belongs.</param>
        /// <param name="elements">A collection of elements that reference the cell.</param>
        public OlapCell(OlapCube cube, System.Collections.Specialized.StringCollection elements)
        {
            _cube = cube;
            _elements = elements;
            _cachedValue = null;
        }

        /// <summary>
        /// Creates a new instance of the OlapCell class.
        /// </summary>
        /// <param name="cube">The cube to which the cell belongs.</param>
        /// <param name="firstElement">The first element name.</param>
        /// <param name="secondElement">The second element name.</param>
        /// <param name="elements">A collection of elements that reference the cell.</param>
        public OlapCell(OlapCube cube, string firstElement, string secondElement, params string[] elements)
        {
            _cachedValue = null;
            _cube = cube;
            _elements = new System.Collections.Specialized.StringCollection();
            _elements.Add(firstElement);
            _elements.Add(secondElement);
            if (elements != null)
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    _elements.Add(elements[i]);
                }
            }
        }

        /// <summary>
        /// Defines an interface to apply transformer classes on a cell.
        /// </summary>
        /// <param name="transformer">A tranformer implementation.</param>
        public void Transform(IOlapCellTransformer transformer)
        {
            transformer.Transform(this);
        }

        /// <summary>
        /// Gets an element name of the cell by the specified dimension index.
        /// </summary>
        /// <param name="index">The dimension index of the element.</param>
        /// <returns>The name of the element.</returns>
        public string this[int index]
        {
            get
            {
                return _elements[index];
            }

            set
            {
                _elements[index] = value;
            }
        }

        /// <summary>
        /// Gets an element name of the cell by the specified dimension index.
        /// </summary>
        /// <param name="name">The dimension name of the element.</param>
        /// <returns>The name of the element.</returns>
        public string this[string name]
        {
            get
            {
                string nameUpper = name.ToUpper();
                for (int i = 0; i < _cube.Dimensions.Count; i++)
                {
                    if (_cube.Dimensions[i].UpcasedName.Equals(nameUpper))
                    {
                        return this[i];
                    }
                }
                return null;
            }

            set
            {
                string nameUpper = name.ToUpper();
                for (int i = 0; i < _cube.Dimensions.Count; i++)
                {
                    if (_cube.Dimensions[i].UpcasedName.Equals(nameUpper))
                    {
                        this[i] = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the value that was read at the latest. 
        /// </summary>
        public object CachedValue
        {
            get
            {
                return _cachedValue;
            }
        }

        /// <summary>
        /// Gets the cube the cell belongs to.
        /// </summary>
        public OlapCube Cube
        {
            get
            {
                return _cube;
            }
        }

        /// <summary>
        /// Returns whether the cell is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                for (int i = 0; i < _elements.Count; i++)
                {
                    OlapDimension dim = _cube.Dimensions[i];
                    if (dim.Elements[_elements[i]].ElementInformation.IsConsolidationElement)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Returns whether the cell is a text cell.
        /// </summary>
        public bool IsTextCell
        {
            get
            {
                int lastElement = _elements.Count - 1;
                return _cube.Dimensions[lastElement].Elements[_elements[lastElement]].ElementInformation.IsTextElement;
            }
        }

        /// <summary>
        /// Gets the value of the cell.
        /// </summary>
        public object Value
        {
            get
            {
                _cachedValue = _cube.GetCell(_elements);
                return _cachedValue;
            }

            set
            {
                _cube.PutCell(value, _elements);
                _cachedValue = value;
            }
        }

        /// <summary>
        /// Gets the value of the cell as numeric value.
        /// </summary>
        public double NumericCachedValue
        {
            get
            {
                if (_cachedValue == null)
                {
                    return 0.0;
                }
                return System.Convert.ToDouble((string)_cachedValue, System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the comment for the cell.
        /// </summary>
        public string Comment
        {
            get
            {
                return _cube.GetCellComment(_elements);
            }

            set
            {
                _cube.PutCellComment(value, _elements);
            }
        }

        /// <summary>
        /// Deletes the value of the cell.
        /// </summary>
        /// <returns>True, if successful; false, otherwise.</returns>
        public bool DeleteValue()
        {
            return _cube.DeleteCell(_elements);
        }

        /// <summary>
        /// Deletes the comment for the cell.
        /// </summary>
        /// <returns>True, if successful; false, otherwise.</returns>
        public bool DeleteComment()
        {
            return _cube.DeleteCellComment(_elements);
        }

        /// <summary>
        /// Increments the value of the cell.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void IncrementCellValue(object value)
        {
            _cube.IncrementCell(value, _elements);
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance. 
        /// </summary>
        /// <returns> A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            System.Collections.Specialized.StringCollection elements = new System.Collections.Specialized.StringCollection();
            for (int i = 0; i < _elements.Count; i++)
            {
                elements.Add(_elements[i]);
            }

            OlapCell cloned = new OlapCell(_cube, elements);
            cloned._cachedValue = _cachedValue;
            cloned.Tag = Tag;
            return cloned;
        }

        /// <summary>
        /// Sets the cached value.
        /// </summary>
        /// <param name="val">The cached value.</param>
        internal void SetCachedValue(object val)
        {
            _cachedValue = val;
        }
    }
}