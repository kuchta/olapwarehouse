using Infor.BI.Applications.OlapApi.Native;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents an Olap element.
    /// </summary>
    // [System.Reflection.DefaultMember("AttributeValue")] // TODO: Check, if this is necessary.
    public class OlapElement : OlapObjectBase
    {
        /// <summary>
        /// Holds the dimension that owns the element.
        /// </summary>
        private OlapDimension _dimension;

        /// <summary>
        /// Holds additional information about the element.
        /// </summary>
        private OlapElementInformation _elementInformation;

        /// <summary>
        /// Holds the name of the element.
        /// </summary>
        private string _name;

        /// <summary>
        /// Holds the uppercase name of the element.
        /// </summary>
        private string _upperName;

        /// <summary>
        /// Holds the parent elements of an element.
        /// </summary>
        private OlapElements _parents;

        /// <summary>
        /// Holds the child elements of an element.
        /// </summary>
        private OlapElements _children;

        /// <summary>
        /// Holds all child elements of an element.
        /// </summary>
        private OlapElements _allChildren;

        /// <summary>
        /// Indicates whether the element can be accessed by the connected user.
        /// </summary>
        private bool _canAccess;

        protected static readonly string KeySeparator = "#";

        /// <summary>
        /// Initializes a new instance of the OlapElement class.
        /// </summary>
        /// <param name="dimension">The dimension that owns the element.</param>
        /// <param name="name">The name of the element.</param>
        public OlapElement(OlapDimension dimension, string name)
        {
            _dimension = dimension;
            _name = name;
            _canAccess = true;
            _upperName = null;
        }

        /// <summary>
        /// Gets the dimension the element belongs to.
        /// </summary>
        public OlapDimension Dimension
        {
            get
            {
                return _dimension;
            }
        }

        /// <summary>
        /// Gets the children of the element. If there are no children the method will return null.
        /// </summary>
        public OlapElements Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new OlapElements(_dimension, this, OlapElementsLevel.OlapElementsLevelChildren);
                }
                return _children;
            }
        }

        /// <summary>
        /// Gets the parents of the element. If there are no parents the method will return null.
        /// </summary>
        public OlapElements Parents
        {
            get
            {
                if (_parents == null)
                {
                    _parents = new OlapElements(_dimension, this, OlapElementsLevel.OlapElementsLevelParents);
                }
                return _parents;
            }
        }

        /// <summary>
        /// Gets the all child elements as flat collection.
        /// </summary>
        /// <param name="type">An element type to specifiy which elements to get.</param>
        /// <returns>All children that have the specified type.</returns>
        public OlapElements ChildrenFlatHierarchy(OlapElementType type)
        {
            return AllChildren.FlatHierarchy(type);
        }

        /// <summary>
        /// Gets the name of the element.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the uppercased name of the element.
        /// </summary>
        public string UpcasedName
        {
            get
            {
                if (_upperName == null)
                {
                    _upperName = _name.ToUpper();
                }
                return _upperName;
            }
        }

        /// <summary>
        /// Gets additional information about the element.
        /// </summary>
        public OlapElementInformation ElementInformation
        {
            get
            {
                if (_elementInformation == null)
                {
                    _elementInformation = NativeOlapApi.DimensionElementInformation(_dimension.Server.Store.ClientSlot, _dimension.Server.ServerHandle, _dimension.Name, _name, _dimension.Server.LastErrorInternal);
                }
                return _elementInformation;
            }
        }

        /// <summary>
        /// Gets the specified attribute field value for the element.
        /// </summary>
        /// <param name="table">The attribute table that has the field.</param>
        /// <param name="field">The field for which to get the value.</param>
        /// <returns>An object that holds the value. Returns null, if the value was not found.</returns>
        // [System::Runtime::CompilerServices::IndexerNameAttribute("Attribute")]
        public object this[OlapAttributeTable table, OlapAttributeTableField field]
        {
            get
            {
                string key = string.Empty;
                bool cacheAttribute = false;
                int tableId = table.Id;

                if (_dimension.CacheAttributes)
                {
                    key = string.Concat(_name, KeySeparator, field.Name);
                    cacheAttribute = true;
                    bool found = false;

                    switch (tableId)
                    {
                        case 1:
                            found = _dimension.Attribute1Cache.ContainsKey(key);
                            if (found)
                            {
                                return _dimension.Attribute1Cache[key];
                            }
                            break;
                        case 2:
                            found = _dimension.Attribute2Cache.ContainsKey(key);
                            if (found)
                            {
                                return _dimension.Attribute2Cache[key];
                            }
                            break;
                        case 3:
                            found = _dimension.Attribute3Cache.ContainsKey(key);
                            if (found)
                            {
                                return _dimension.Attribute3Cache[key];
                            }
                            break;
                        default:
                            throw new OlapException("Invalid attribute table requested");
                    }
                }

                OlapAttributeTableFieldDefinition def = new OlapAttributeTableFieldDefinition();
                def.Type = (char)field.Type;
                def.Decimals = field.Decimals;
                def.FieldWidth = field.Width;
                object result = NativeOlapApi.AttributeGetValue(_dimension.Server.Store.ClientSlot, _dimension.Server.ServerHandle, _dimension.Name, tableId, field.Name, _name, def, _dimension.Server.LastErrorInternal);

                if (cacheAttribute)
                {
                    switch (tableId)
                    {
                        case 1:
                            _dimension.Attribute1Cache.Add(key, result);
                            break;
                        case 2:
                            _dimension.Attribute2Cache.Add(key, result);
                            break;
                        case 3:
                            _dimension.Attribute3Cache.Add(key, result);
                            break;
                    }
                }

                return result;
            }

            set
            {
                OlapAttributeTableFieldDefinition def = new OlapAttributeTableFieldDefinition();
                def.Type = (char)field.Type;
                def.Decimals = field.Decimals;
                def.FieldWidth = field.Width;
                NativeOlapApi.AttributeSetValue(_dimension.Server.Store.ClientSlot, _dimension.Server.ServerHandle, _dimension.Name, table.Id, field.Name, _name, def, value, _dimension.Server.LastErrorInternal);
            }
        }

        /// <summary>
        /// Gets the specified attribute field value for the element.
        /// </summary>
        /// <param name="tableIndex">The attribute table index that holds the specified field.</param>
        /// <param name="fieldName">The field name for which to get the value.</param>
        /// <returns>An object that holds the value. Returns null, if the value was not found.</returns>
        // [System::Runtime::CompilerServices::IndexerNameAttribute("Attribute")]
        public object this[int tableIndex, string fieldName]
        {
            get
            {
                OlapAttributeTable table = _dimension.AttributeTables[tableIndex];
                OlapAttributeTableField field = null;

                if (table == null)
                {
                    throw new OlapException(string.Format("The attribute table {0} does not exist", tableIndex.ToString()));
                }

                for (int i = 0; i < table.FieldCount; i++)
                {
                    field = table.Fields[i];
                    if (fieldName.ToLower().Equals(field.Name.ToLower()))
                    {
                        return this[table, field];
                    }
                }

                throw new OlapException(string.Format("The field {0} does not exist", fieldName));
            }

            set
            {
                OlapAttributeTable table = _dimension.AttributeTables[tableIndex];
                OlapAttributeTableField field = null;

                if (table == null)
                {
                    throw new OlapException(string.Format("The attribute table {0} does not exist", tableIndex.ToString()));
                }

                for (int i = 0; i < table.FieldCount; i++)
                {
                    field = table.Fields[i];
                    if (fieldName.ToLower().Equals(field.Name.ToLower()))
                    {
                        this[table, field] = value;
                        return;
                    }
                }

                throw new OlapException(string.Format("The field {0} does not exist", fieldName));
            }
        }

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns>Textual representation of the Olap element. That is its name.</returns>
        public override string ToString()
        {
            return _name;
        }

        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="obj">The object to compare this object to.</param>
        /// <returns>True, if the two OlapElement objects are equal. False, otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            OlapElement p = obj as OlapElement;
            return Name == p.Name;
        }

        /// <summary>
        /// Gets the hash code for an object.
        /// </summary>
        /// <returns>The hash for the object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Gets all children of the element regardless of any permissions. If there are no children the method will return null.
        /// </summary>
        internal OlapElements AllChildren
        {
            get
            {
                if (_allChildren == null)
                {
                    _allChildren = new OlapElements(_dimension, this, OlapElementsLevel.OlapElementsLevelChildren);
                    _allChildren.IgnorePermissionsForChildren = true;
                }
                return _allChildren;
            }
        }

        /// <summary>
        /// Gets a flag that indicates whether access is granted; false, if not.
        /// </summary>
        internal bool CanAccess
        {
            get
            {
                return _canAccess;
            }

            set
            {
                _canAccess = value;
            }
        }
    }
}
