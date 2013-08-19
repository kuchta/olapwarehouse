using Infor.BI.Applications.OlapApi.Native;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents an Olap dimension.
    /// </summary>
    public class OlapDimension : OlapObjectBase
    {
        /// <summary>
        /// Holds the dimension elements in their flat structure.
        /// </summary>
        private OlapElements _elements;

        /// <summary>
        /// Holds the dimension elements in their hierarchical structure.
        /// </summary>
        private OlapElements _hierarchyElements;

        /// <summary>
        /// Holds additional information about a dimension.
        /// </summary>
        private OlapDimensionInformation _dimensionInformation;

        /// <summary>
        /// Holds the attribute tables for the dimension.
        /// </summary>
        private OlapAttributeTables _attributeTables;

        /// <summary>
        /// Holds the subets of the dimension.
        /// </summary>
        private OlapSubsets _subsets;

        /// <summary>
        /// Holds the name of the dimension.
        /// </summary>
        private string _name;

        /// <summary>
        /// Holds the uppercase name of the element.
        /// </summary>
        private string _upperName;

        /// <summary>
        /// Holds the server that has the dimension.
        /// </summary>
        private OlapServer _server;

        /// <summary>
        /// Caches the attributes that have already been retrieved.
        /// </summary>
        private System.Collections.Generic.Dictionary<string, object> _attributeTable1Cache;

        private System.Collections.Generic.Dictionary<string, object> _attributeTable2Cache;
        private System.Collections.Generic.Dictionary<string, object> _attributeTable3Cache;

        /// <summary>
        /// Defines whether attributes should be cached or not.
        /// </summary>
        private bool _cacheAttributeValues;

        /// <summary>
        /// Creates a new instance of the OlapDimension class.
        /// </summary>
        /// <param name="server">The Olap server that owns this dimension.</param>
        /// <param name="name">The name of the dimension.</param>
        public OlapDimension(OlapServer server, string name)
        {
            _attributeTable1Cache = new System.Collections.Generic.Dictionary<string, object>(0);
            _attributeTable2Cache = new System.Collections.Generic.Dictionary<string, object>(0);
            _attributeTable3Cache = new System.Collections.Generic.Dictionary<string, object>(0);
            _cacheAttributeValues = true;
            _server = server;
            _name = name;
            _subsets = null;
            _dimensionInformation = null;
            _elements = null;
            _attributeTables = null;
            _hierarchyElements = null;
            _upperName = null;
        }

        /// <summary>
        /// Gets the weight of a child element to its specified parent element.
        /// </summary>
        /// <param name="parentName">The name of the parent element.</param>
        /// <param name="childName">The name of the child element.</param>
        /// <returns>The weight.</returns>
        public double Weight(string parentName, string childName)
        {
            return NativeOlapApi.DimensionElementWeight(_server.Store.ClientSlot, _server.ServerHandle, _name, parentName, childName, _server.LastErrorInternal);
        }

        /// <summary>
        /// Gets additional information about the dimension.
        /// </summary>
        public OlapDimensionInformation DimensionInformation
        {
            get
            {
                if (_dimensionInformation == null)
                {
                    _dimensionInformation = NativeOlapApi.DimensionInformation(_server.Store.ClientSlot, _server.ServerHandle, _name, _server.LastErrorInternal);
                }
                return _dimensionInformation;
            }
        }

        /// <summary>
        /// Gets the name of the dimension-.
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
        /// Gets the server that has the dimension.
        /// </summary>
        public OlapServer Server
        {
            get
            {
                return _server;
            }
        }

        /// <summary>
        /// Gets a flag that defines whether attributes will be cached.
        /// </summary>
        public bool CacheAttributes
        {
            get
            {
                return _cacheAttributeValues;
            }

            set
            {
                _cacheAttributeValues = value;
            }
        }

        /// <summary>
        /// Removes all currently cached attributes from the attribute cache.
        /// </summary>
        public void ClearAttributeCache()
        {
            _attributeTable1Cache = new System.Collections.Generic.Dictionary<string, object>(0);
            _attributeTable2Cache = new System.Collections.Generic.Dictionary<string, object>(0);
            _attributeTable3Cache = new System.Collections.Generic.Dictionary<string, object>(0);
        }

        /// <summary>
        /// This function creates a new dimension element.
        /// </summary>
        /// <param name="numericElement">If true, a numeric element will be created. If false, a text element will be created.</param>
        /// <param name="element">The element to create.</param>
        /// <param name="parentElement">The parent element under which the new element should be created. If empty the element will be created on the top level.</param>
        /// <param name="weight">The weight used to aggregate the new element into it's parent. Only used if parent element is provided.</param>
        /// <returns>True, if the element was created. False, if an error occurred.</returns>
        public bool CreateDimensionElement(bool numericElement, string element, string parentElement, double weight)
        {
            return NativeOlapApi.CreateDimensionElement(_server.Store.ClientSlot, _server.ServerHandle, "1", _name, numericElement, element, parentElement, weight);
        }

        /// <summary>
        /// This function renames a dimension element.
        /// </summary>
        /// <param name="element">The element to rename.</param>
        /// <param name="newName">The new name for the element.</param>
        /// <returns>True, if the element was renamed. False, if an error occurred.</returns>
        public bool RenameDimensionElement(string element, string newName)
        {
            return NativeOlapApi.RenameDimensionElement(_server.Store.ClientSlot, _server.ServerHandle, "1", _name, element, newName);
        }

        /// <summary>
        /// This function deletes a dimension element.
        /// </summary>
        /// <param name="element">The element to delete.</param>
        /// <returns>True, if the element was deleted. False, if an error occurred.</returns>
        public bool DeleteDimensionElement(string element)
        {
            return NativeOlapApi.DeleteDimensionElement(_server.Store.ClientSlot, _server.ServerHandle, "1", _name, element);
        }

        /// <summary>
        /// Gets the Olap elements of the dimension in a flat representation.
        /// </summary>
        public OlapElements Elements
        {
            get
            {
                if (_elements == null)
                {
                    _elements = new OlapElements(this, OlapElementsLevel.OlapElementsLevelAll);
                }

                return _elements;
            }
        }

        /// <summary>
        /// Gets the Olap elements of the dimension in their hierarchical representation.
        /// </summary>
        public OlapElements HierarchyElements
        {
            get
            {
                if (_hierarchyElements == null)
                {
                    _hierarchyElements = new OlapElements(this, OlapElementsLevel.OlapElementsLevelTopLevel);
                }

                return _hierarchyElements;
            }
        }

        /// <summary>
        /// Gets the attribute tables for the dimension.
        /// </summary>
        public OlapAttributeTables AttributeTables
        {
            get
            {
                if (_attributeTables == null)
                {
                    _attributeTables = new OlapAttributeTables(this);
                }
                return _attributeTables;
            }
        }

        /// <summary>
        /// Gets the subsets of the dimension.
        /// </summary>
        public OlapSubsets Subsets
        {
            get
            {
                if (_subsets == null)
                {
                    _subsets = new OlapSubsets(this);
                }
                return _subsets;
            }
        }

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns>Textual representation of the Olap dimension. That is its name.</returns>
        public override string ToString()
        {
            return _name;
        }

        /// <summary>
        /// Returns the cache for the attribute table 1 values.
        /// </summary>
        internal System.Collections.Generic.Dictionary<string, object> Attribute1Cache
        {
            get
            {
                return _attributeTable1Cache;
            }
        }

        /// <summary>
        /// Returns the cache for the attribute table 2 values.
        /// </summary>
        internal System.Collections.Generic.Dictionary<string, object> Attribute2Cache
        {
            get
            {
                return _attributeTable2Cache;
            }
        }

        /// <summary>
        /// Returns the cache for the attribute table 3 values.
        /// </summary>
        internal System.Collections.Generic.Dictionary<string, object> Attribute3Cache
        {
            get
            {
                return _attributeTable3Cache;
            }
        }
    }
}