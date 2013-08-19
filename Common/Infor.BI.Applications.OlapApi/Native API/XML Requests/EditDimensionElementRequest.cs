using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Infor.BI.Applications.OlapApi.Native
{
    /// <summary>
    /// This class implements all methods to manage requests to modify OLAP dimensions.
    /// </summary>
    internal class EditDimensionElementRequest : RequestBase
    {
        /// <summary>
        /// Creates an OLAP XML Request to create a new numerical OLAP Element in a dimension.
        /// </summary>
        /// <param name="requestID">The request ID.</param>
        /// <param name="dimension">The dimension name.</param>
        /// <param name="numericElement">If true, a numeric element will be created. If false, a text element will be created.</param>
        /// <param name="element">The element to create.</param>
        /// <param name="parentElement">The parent element under which the new element should be created. If empty the element will be created on the top level.</param>
        /// <param name="weight">The weight used to aggregate the new element into it's parent. Only used if parent element is provided.</param>
        /// <returns>A XML document completely filled and ready to send.</returns>
        internal static string CreateAddElementRequest(string requestID, string dimension, bool numericElement, string element, string parentElement, double weight)
        {
            /* Sample request
                <?xml version="1.0" encoding="UTF-16" standalone="no"?>
                <Alea:Document xmlns:Alea="http://www.misag.com">
                    <Alea:Request RequestID="001" Class="Dimension" Method="Write">
                        <Alea:Dimension Name="Dim A" FirstBatch="true" LastBatch="true">
                            <Alea:Description>Dim A</Alea:Description>
                            <Alea:DimensionTemplate Name="Dim A"/>
                            <Alea:Elements>N          Elt_11
                            </Alea:Elements>
                        </Alea:Dimension>
                    </Alea:Request>
                </Alea:Document>
            */
            XElement document = RequestBase.CreateDocumentBase();
            XElement request = RequestBase.CreateRequestDocument(requestID, "Dimension", "Write");
            XElement dimensionSection = new XElement(RequestBase.OlapNamespace + "Dimension");

            request.Add(dimensionSection);
            document.Add(request);

            dimensionSection.SetAttributeValue("Name", dimension);
            dimensionSection.SetAttributeValue("FirstBatch", "true");
            dimensionSection.SetAttributeValue("LastBatch", "true");

            XElement descriptionSection = new XElement(RequestBase.OlapNamespace + "Description");
            dimensionSection.Add(descriptionSection);
            descriptionSection.SetValue(dimension);

            XElement templateSection = new XElement(RequestBase.OlapNamespace + "DimensionTemplate");
            dimensionSection.Add(templateSection);
            templateSection.SetAttributeValue("Name", dimension);

            XElement elementSection = new XElement(RequestBase.OlapNamespace + "Elements");
            dimensionSection.Add(elementSection);

            string elementText = string.Empty;

            if (numericElement)
            {
                // numeric element N
                elementText = "N\t" + element;
            }
            else
            {
                // text element S
                elementText = "S\t" + element;
            }

            if (!string.IsNullOrEmpty(parentElement))
            {
                if (numericElement)
                {
                    elementText += "\r\n" + string.Format("!\t{0}\t{1}\t{2}", parentElement, element, weight.ToString("g", CultureInfo.InvariantCulture));
                }
                else
                {
                    elementText += "\r\n" + string.Format("!\t{0}\t{1}", parentElement, element);
                }
            }

            elementSection.SetValue(elementText);

            StringWriter requestString = new StringWriter();
            document.Save(requestString);
            return requestString.ToString();
        }

        /// <summary>
        /// Creates an OLAP XML Request to delete an OLAP element in a dimension.
        /// </summary>
        /// <param name="requestID">The request ID.</param>
        /// <param name="dimension">The dimension name.</param>
        /// <param name="element">The element to delete.</param>
        /// <returns>A XML document completely filled and ready to send.</returns>
        internal static string CreateDeleteElementRequest(string requestID, string dimension, string element)
        {
            /* Sample request
                <?xml version="1.0" encoding="UTF-16" standalone="no"?>
                <Alea:Document xmlns:Alea="http://www.misag.com">
                    <Alea:Request RequestID="001" Class="Dimension" Method="Write">
                        <Alea:Dimension Name="Dim A" FirstBatch="true" LastBatch="true">
                            <Alea:Description>Dim A</Alea:Description>
                            <Alea:DimensionTemplate Name="YearsOld" Time="Mar/26/2007 12:48:03,462">
                                <Alea:DeleteElement Name="ToBeDeleted" />  // zero or more
                             </Alea:DimensionTemplate>
                        </Alea:Dimension>
                    </Alea:Request>
                </Alea:Document>
            */
            XElement document = RequestBase.CreateDocumentBase();
            XElement request = RequestBase.CreateRequestDocument(requestID, "Dimension", "Write");
            XElement dimensionSection = new XElement(RequestBase.OlapNamespace + "Dimension");

            request.Add(dimensionSection);
            document.Add(request);

            dimensionSection.SetAttributeValue("Name", dimension);
            dimensionSection.SetAttributeValue("FirstBatch", "true");
            dimensionSection.SetAttributeValue("LastBatch", "true");

            XElement descriptionSection = new XElement(RequestBase.OlapNamespace + "Description");
            dimensionSection.Add(descriptionSection);
            descriptionSection.SetValue(dimension);

            XElement templateSection = new XElement(RequestBase.OlapNamespace + "DimensionTemplate");
            dimensionSection.Add(templateSection);
            templateSection.SetAttributeValue("Name", dimension);
            templateSection.SetAttributeValue("Time", DateTime.Now.ToString("G", CultureInfo.InvariantCulture));

            XElement elementSection = new XElement(RequestBase.OlapNamespace + "DeleteElement");
            templateSection.Add(elementSection);
            elementSection.SetAttributeValue("Name", element);

            StringWriter requestString = new StringWriter();
            document.Save(requestString);
            return requestString.ToString();
        }

        /// <summary>
        /// Creates an OLAP XML Request to rename an OLAP element in a dimension.
        /// </summary>
        /// <param name="requestID">The request ID.</param>
        /// <param name="dimension">The dimension name.</param>
        /// <param name="element">The element to rename.</param>
        /// <param name="newName">The element's new name.</param>
        /// <returns>A XML document completely filled and ready to send.</returns>
        internal static string CreateRenameElementRequest(string requestID, string dimension, string element, string newName)
        {
            /* Sample request
                <?xml version="1.0" encoding="UTF-16" standalone="no"?>
                <Alea:Document xmlns:Alea="http://www.misag.com">
                    <Alea:Request RequestID="7" Class="Dimension" Method="RenameElements">
                        <Alea:Dimension Name="Dim B">
                            <Alea:Element Name="B 1" NewName="B 1a"/>
                        </Alea:Dimension>
                    </Alea:Request>
                </Alea:Document>

                Response:
                <?xml version="1.0" encoding="UTF-16" standalone="no"?>
                <Alea:Document xmlns:Alea="http://www.misag.com">
                    <Alea:Request RequestID="7">
                        <Alea:Return>
                            <Alea:User Name="Admin" Time="Mar/19/2013 13:53:01,410"/>
                                <Alea:Dimension Name="Dim B">
                                    <Alea:Element Name="B 1" NewName="B 1a"/>
                                </Alea:Dimension>
                                <Alea:ChangedCubes>
                                    <Alea:Cube Name="AB"/>
                                </Alea:ChangedCubes>
                            </Alea:Return>
                    </Alea:Request>
                </Alea:Document>
            */
            XElement document = RequestBase.CreateDocumentBase();
            XElement request = RequestBase.CreateRequestDocument(requestID, "Dimension", "RenameElements");
            XElement dimensionSection = new XElement(RequestBase.OlapNamespace + "Dimension");

            request.Add(dimensionSection);
            document.Add(request);

            dimensionSection.SetAttributeValue("Name", dimension);

            XElement elementSection = new XElement(RequestBase.OlapNamespace + "Element");
            dimensionSection.Add(elementSection);
            elementSection.SetAttributeValue("Name", element);
            elementSection.SetAttributeValue("NewName", newName);

            StringWriter requestString = new StringWriter();
            document.Save(requestString);
            return requestString.ToString();
        }
    }
}