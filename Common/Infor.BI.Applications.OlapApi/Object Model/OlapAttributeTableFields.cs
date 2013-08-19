using System.Collections;
using Infor.BI.Applications.OlapApi.Native;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Represents a collection of Olap attribute table fields.
    /// </summary>
    public class OlapAttributeTableFields : OlapCollectionObjectBase<OlapAttributeTableField>, IEnumerable
    {
        /// <summary>
        /// Holds the OlapAttributeTable that owns this attribute field collection.
        /// </summary>
        private OlapAttributeTable _attributeTable;

        private void Load()
        {
            if (Invalid)
            {
                IsInitialized = false;
            }
            
            if (!IsInitialized)
                {
                Collection = new System.Collections.Generic.List<OlapAttributeTableField>(0);
                System.Collections.ArrayList fields = NativeOlapApi.AttributeTableFields(_attributeTable.Dimension.Server.Store.ClientSlot, _attributeTable.Dimension.Server.ServerHandle, this._attributeTable.Dimension.Name, _attributeTable.Id, _attributeTable.Dimension.Server.LastErrorInternal);
                if (fields != null)
                {
                    for (int i = 0; i < fields.Count; i++)
                    {
                        OlapAttributeTableFieldDefinition fieldDef = (OlapAttributeTableFieldDefinition)fields[i];
                        OlapAttributeTableFieldType type;
                        switch (fieldDef.Type)
                        {
                            case 'C':
                                type = OlapAttributeTableFieldType.OlapAttributeTableFieldTypeCharacter;
                                break;
                            case 'N':
                                type = OlapAttributeTableFieldType.OlapAttributeTableFieldTypeNumeric;
                                break;
                            case 'D':
                                type = OlapAttributeTableFieldType.OlapAttributeTableFieldTypeDate;
                                break;
                            case 'L':
                                type = OlapAttributeTableFieldType.OlapAttributeTableFieldTypeLogical;
                                break;
                            default:
                                throw new OlapException("Found unknown attribute field type: " + fieldDef.Type);
                        }
                        Collection.Add(new OlapAttributeTableField(_attributeTable, fieldDef.FieldName, fieldDef.Id, fieldDef.FieldWidth, fieldDef.Decimals, type));
                    }
                }
                else
                {
                    if (_attributeTable.Dimension.Server.LastErrorInternal.Value != 0)
                    {
                        throw new OlapException("Receiving the attribute table field collection failed!", _attributeTable.Dimension.Server.LastErrorInternal.Value);
                    }
                }
                Invalid = false;
                IsInitialized = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the OlapAttributeTables class.
        /// </summary>
        /// <param name="attributeTable">The Olap attribute table that owns this collection.</param>
        public OlapAttributeTableFields(OlapAttributeTable attributeTable)
        {
            _attributeTable = attributeTable;
        }

        /// <summary>
        /// Gets the Olap attribute table field at the specified index of the collection.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>The OlapAttributeTableField or null, if th index was invalid.</returns>
        public OlapAttributeTableField this[int index]
        {
            get
            {
                Load();
                if (0 <= index && index < Collection.Count)
                {
                    return Collection[index];
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the number of items currently in the collection.
        /// </summary>
        /// <returns>The number of items in the collection.</returns>
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
        /// <returns>An IEnumerator of the collection.</returns>
        public IEnumerator GetEnumerator()
        {
            Load();
            return Collection.GetEnumerator();
        }
    }
}
