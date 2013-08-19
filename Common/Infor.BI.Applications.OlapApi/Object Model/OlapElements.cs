using Infor.BI.Applications.OlapApi.Native;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents a collection of Olap elements.
    /// </summary>
    public class OlapElements : OlapCollectionObjectBase<OlapElement>, System.Collections.IEnumerable
    {
        /// <summary>
        /// Holds the dimension that owns the element collection.
        /// </summary>
        private OlapDimension _dimension;

        /// <summary>
        /// Holds the subset, if the elements collection belongs to a subset.
        /// </summary>
        private OlapSubset _subset;

        /// <summary>
        /// Holds an element if the collection should represent parent or child elements.
        /// </summary>
        private OlapElement _element;

        /// <summary>
        /// Holds a flag that indicates which kind of elements are represented by the collection.
        /// </summary>
        private OlapElementsLevel _elementsLevel;

        /// <summary>
        /// Holds a flag that indicates that all elements will be loaded and permissions ignored.
        /// </summary>
        private bool _ignorePermissionsForChildren;

        /// <summary>
        /// Initializes a new instance of the OlapElements class.
        /// </summary>
        /// <param name="dimension">The dimension that owns the collection.</param>
        /// <param name="elementsLevel">Defines which elements should be represented by the collection.</param>
        public OlapElements(OlapDimension dimension, OlapElementsLevel elementsLevel)
        {
            _dimension = dimension;
            _subset = null;
            _element = null;
            _elementsLevel = elementsLevel;
            _ignorePermissionsForChildren = false;

            switch (elementsLevel)
            {
                case OlapElementsLevel.OlapElementsLevelTopLevel:
                case OlapElementsLevel.OlapElementsLevelAll:
                    break;
                default:

                    // TODO 10.5: from resource
                    throw new OlapException("wrong parameter level");
            }
        }

        /// <summary>
        /// Initializes a new instance of the OlapElements class.
        /// </summary>
        /// <param name="dimension">The dimension that owns the collection.</param>
        /// <param name="subset">A subset for which to represnt the elements.</param>
        public OlapElements(OlapDimension dimension, OlapSubset subset)
        {
            _dimension = dimension;
            _subset = subset;
            _element = null;
            _elementsLevel = OlapElementsLevel.OlapElementsLevelSubset;
            _ignorePermissionsForChildren = false;
        }

        /// <summary>
        /// Initializes a new instance of the OlapElements class.
        /// </summary>
        /// <param name="dimension">The dimension that owns the collection.</param>
        /// <param name="element">An element for which the collection will represent child or
        /// parent elements.</param>
        /// <param name="elementsLevel">Defines which elements should be represented by the collection.</param>
        public OlapElements(OlapDimension dimension, OlapElement element, OlapElementsLevel elementsLevel)
        {
            _dimension = dimension;
            _subset = null;
            _element = element;
            _elementsLevel = elementsLevel;
            _ignorePermissionsForChildren = false;

            switch (elementsLevel)
            {
                case OlapElementsLevel.OlapElementsLevelChildren:
                case OlapElementsLevel.OlapElementsLevelParents:
                case OlapElementsLevel.OlapElementsLevelFlat:
                    break;
                default:

                    // TODO 10.5: resource
                    throw new OlapException("wrong parameter level");
            }
        }

        /// <summary>
        /// Gets the elements of the collection and all its children as flat collection.
        /// </summary>
        /// <returns>All elements and their children store in this collection.</returns>
        public OlapElements FlatHierarchy()
        {
            OlapElements flatElements = null;

            Load();

            if (_elementsLevel == OlapElementsLevel.OlapElementsLevelAll)
            {
                flatElements = new OlapElements(_dimension, null, OlapElementsLevel.OlapElementsLevelFlat);
                flatElements.IgnorePermissionsForChildren = true;
                flatElements.Initialized = true;
                flatElements.Invalid = false;
                for (int i = 0; i < Collection.Count; i++)
                {
                    OlapElement element = Collection[i];
                    flatElements.Collection.Add(element);
                }
            }
            else
            {
                flatElements = new OlapElements(_dimension, _element, OlapElementsLevel.OlapElementsLevelFlat);
                flatElements.IgnorePermissionsForChildren = true;
                flatElements.Initialized = true;
                flatElements.Invalid = false;
                for (int i = 0; i < Collection.Count; i++)
                {
                    OlapElement element = Collection[i];
                    flatElements.Collection.Add(element);
                    MakeFlat(element, flatElements);
                }
            }
            return flatElements;
        }

        /// <summary>
        /// Gets the elements of the collection and all its children as flat collection.
        /// </summary>
        /// <param name="type">An element type to specifiy which elements to get.</param>
        /// <returns>All elements and their children store in this collection that have
        /// the specified type.</returns>
        public OlapElements FlatHierarchy(OlapElementType type)
        {
            OlapElements flatElements = null;

            Load();

            // if this collection includes all elements, making it flat means put all elements
            // into the result and filter by type in addition.
            if (_elementsLevel == OlapElementsLevel.OlapElementsLevelAll)
            {
                flatElements = new OlapElements(_dimension, null, OlapElementsLevel.OlapElementsLevelFlat);
                flatElements.IgnorePermissionsForChildren = true;
                flatElements.Initialized = true;
                flatElements.Invalid = false;
                for (int i = 0; i < Collection.Count; i++)
                {
                    OlapElement element = Collection[i];
                    if (type == element.ElementInformation.ElementType)
                    {
                        flatElements.Collection.Add(element);
                    }
                }
            }
            else
            {
                flatElements = new OlapElements(_dimension, _element, OlapElementsLevel.OlapElementsLevelFlat);
                flatElements.IgnorePermissionsForChildren = true;
                flatElements.Initialized = true;
                flatElements.Invalid = false;
                for (int i = 0; i < Collection.Count; i++)
                {
                    OlapElement element = Collection[i];
                    if (element.ElementInformation.ElementType == type)
                    {
                        flatElements.Collection.Add(element);
                    }
                    MakeFlat(element, flatElements, type);
                }
            }

            OlapElements result = null;
            result = new OlapElements(_dimension, null, OlapElementsLevel.OlapElementsLevelFlat);
            result.IgnorePermissionsForChildren = true;
            result.Initialized = true;
            result.Invalid = false;

            for (int i = 0; i < flatElements.Count; i++)
            {
                if (flatElements[i].CanAccess)
                {
                    OlapElement element = flatElements[i];
                    if (!result.Collection.Contains(element))
                    {
                        result.Collection.Add(element);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the element at the specified index from the collection.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The OlapElement or null, if the index was invalid.</returns>
        public OlapElement this[int index]
        {
            get
            {
                Load();
                if (Collection.Count > index && index >= 0)
                {
                    return Collection[index];
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the element with the specified name from the collection.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <returns>The OlapElement or null, if the name was invalid.</returns>
        public OlapElement this[string name]
        {
            get
            {
                Load();
                string nameUpper = name.ToUpper();

                // TODO 10.5: implement better, this might be unnecessary slow
                for (int i = 0; i < Collection.Count; i++)
                {
                    OlapElement element = Collection[i];
                    if (nameUpper.Equals(element.UpcasedName))
                    {
                        return element;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the number of elements currently in the collection.
        /// </summary>
        /// <returns>The number of elements in the collection.</returns>
        public int Count
        {
            get
            {
                Load();
                return Collection.Count;
            }
        }

        /// <summary>
        /// Gets an enumerator to iterate through the collection.
        /// </summary>
        /// <returns>An enumerator for looping over the OLAP elements.</returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            Load();
            return Collection.GetEnumerator();
        }

        /// <summary>
        /// Loads the elements collection.
        /// </summary>
        protected void Load()
        {
            if (Invalid)
            {
                IsInitialized = false;
            }

            if (!IsInitialized)
            {
                Collection = new System.Collections.Generic.List<OlapElement>(0);
                System.Collections.Specialized.StringCollection elementNames = null;
                if (_subset != null)
                {
                    OlapSubsetDefinition subsetDef = new OlapSubsetDefinition();
                    subsetDef.LongName = _subset.LongName;
                    subsetDef.RefName = _subset.ReferenceName;
                    elementNames = NativeOlapApi.DimensionSubsetElements(_dimension.Server.Store.ClientSlot, _dimension.Server.ServerHandle, _dimension.Name, subsetDef, _dimension.Server.LastErrorInternal);
                }
                else if (_element != null)
                {
                    switch (_elementsLevel)
                    {
                        case OlapElementsLevel.OlapElementsLevelParents:
                            elementNames = NativeOlapApi.DimensionElementParents(_dimension.Server.Store.ClientSlot, _dimension.Server.ServerHandle, _dimension.Name, _element.Name, _dimension.Server.LastErrorInternal);
                            break;

                        case OlapElementsLevel.OlapElementsLevelChildren:
                            if (!_ignorePermissionsForChildren)
                            {
                                elementNames = NativeOlapApi.DimensionElementChildren(_dimension.Server.Store.ClientSlot, _dimension.Server.ServerHandle, _dimension.Name, _element.Name, _dimension.Server.LastErrorInternal);
                            }
                            else
                            {
                                System.Collections.Generic.Dictionary<string, bool> allElementNames = NativeOlapApi.DimensionElementAllChildren(_dimension.Server.Store.ClientSlot, _dimension.Server.ServerHandle, _dimension.Name, _element.Name, _dimension.Server.LastErrorInternal);
                                if (allElementNames != null)
                                {
                                    System.Collections.Generic.Dictionary<string, bool>.Enumerator enumNames = allElementNames.GetEnumerator();

                                    while (enumNames.MoveNext())
                                    {
                                        System.Collections.Generic.KeyValuePair<string, bool> de = enumNames.Current;
                                        OlapElement newElement = new OlapElement(_dimension, de.Key);
                                        if (de.Value)
                                        {
                                            newElement.CanAccess = true;
                                        }
                                        else
                                        {
                                            newElement.CanAccess = false;
                                        }
                                        Collection.Add(newElement);
                                    }
                                }
                                else
                                {
                                    if (_dimension.Server.LastErrorInternal.Value != 0)
                                    {
                                        throw new OlapException("Receiving the element collection failed!", _dimension.Server.LastErrorInternal.Value);
                                    }
                                }
                                Invalid = false;
                                IsInitialized = true;
                                return;
                            }
                            break;

                        case OlapElementsLevel.OlapElementsLevelTopLevel:
                            elementNames = NativeOlapApi.DimensionTopLevelElements(_dimension.Server.Store.ClientSlot, _dimension.Server.ServerHandle, _dimension.Name, _dimension.Server.LastErrorInternal);
                            break;
                    }
                }
                else
                {
                    switch (_elementsLevel)
                    {
                        case OlapElementsLevel.OlapElementsLevelTopLevel:
                            elementNames = NativeOlapApi.DimensionTopLevelElements(_dimension.Server.Store.ClientSlot, _dimension.Server.ServerHandle, _dimension.Name, _dimension.Server.LastErrorInternal);
                            break;

                        case OlapElementsLevel.OlapElementsLevelAll:
                            elementNames = NativeOlapApi.DimensionElements(_dimension.Server.Store.ClientSlot, _dimension.Server.ServerHandle, _dimension.Name, _dimension.Server.LastErrorInternal);
                            break;
                    }
                }

                if (elementNames != null)
                {
                    for (int i = 0; i < elementNames.Count; i++)
                    {
                        OlapElement newElement = new OlapElement(_dimension, elementNames[i]);
                        newElement.CanAccess = true;
                        Collection.Add(newElement);
                    }
                }
                else
                {
                    if (_dimension.Server.LastErrorInternal.Value != 0)
                    {
                        throw new OlapException("Receiving the element collection failed!", _dimension.Server.LastErrorInternal.Value);
                    }
                }
                Invalid = false;
                IsInitialized = true;
            }
        }

        /// <summary>
        /// Sets whether permissions will be ignored during loading.
        /// </summary>
        internal bool IgnorePermissionsForChildren
        {
            set
            {
                _ignorePermissionsForChildren = value;
            }
        }

        /// <summary>
        /// Creates a flat representation of the hierarchy starting with the specifed element.
        /// </summary>
        /// <param name="element">The element to make flat.</param>
        /// <param name="collection">The collection that will store the flat elements.</param>
        private void MakeFlat(OlapElement element, OlapElements collection)
        {
            for (int i = 0; i < element.AllChildren.Count; i++)
            {
                collection.Collection.Add(element.Children[i]);
                MakeFlat(element.AllChildren[i], collection);
            }
        }

        /// <summary>
        /// Creates a flat representation of the hierarchy starting with the specifed element.
        /// </summary>
        /// <param name="element">The element to make flat.</param>
        /// <param name="collection">The collection that will store the flat elements.</param>
        /// <param name="type">An element type to specifiy which elements to get.</param>
        private void MakeFlat(OlapElement element, OlapElements collection, OlapElementType type)
        {
            for (int i = 0; i < element.AllChildren.Count; i++)
            {
                OlapElement element1 = element.AllChildren[i];
                if (element1.ElementInformation.ElementType == type)
                {
                    collection.Collection.Add(element1);
                }
                MakeFlat(element1, collection, type);
            }
        }
    }
}