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
    /// This class implements all methods to manage splash requests.
    /// </summary>
    internal class SplashRequest : RequestBase
    {
        /// <summary>
        /// Creates an OLAP XML Request for splashing a value to a calculated cell in a cube.
        /// </summary>
        /// <param name="requestID">The id of the request.</param>
        /// <param name="cube">The cube to splash to.</param>
        /// <param name="value">The value to splash.</param>
        /// <param name="mode">The splash mode.</param>
        /// <param name="rounding">True, if values should be rounded, false if not.</param>
        /// <param name="decimals">The number of decimals to round, if rounding is true.</param>
        /// <param name="notDeleteOnZero">If true, sending the value 0 will not delete the leaf cells, but it will write the value 0. Otherwise sending the value 0 will delete the leaf cells.</param>
        /// <param name="first">The element for the first dimension.</param>
        /// <param name="second">The element for the second dimension.</param>
        /// <param name="elementNames">The variable elements for all other dimensions.</param>
        /// <returns>A string containing the XML request document ready to send.</returns>
        internal static string CreateRequest(string requestID, OlapCube cube, double value, string mode, bool rounding, int decimals, bool notDeleteOnZero, string first, string second, string[] elementNames)
        {
            if (cube.Dimensions.Count != elementNames.Length + 2)
            {
                throw new OlapException("Splash Value: The number of elements must match the number and order of dimensions in the cube " + cube.Name);
            }

            /* Sample Request
                <Alea:Document xmlns:Alea="http://www.misag.com">
                <Alea:Request RequestID="1" Class="Cube" Method="Splashing">
                    <Alea:Splashing AllocationMode="Equal" Rounding="true" DecimalPlaces="2" ErrorCorrection="true" Undo="true" DoNotDeleteOnZero="true">
                        <Alea:CellCoordinates Purpose="Target" Cube="TOTSALES">
                            <Alea:Element Dimension="YEARS" Name="2004"/>
                            <Alea:Element Dimension="ACTVSBUD" Name="Actual"/>
                            <Alea:Element Dimension="REGION" Name="Usa"/>
                            <Alea:Element Dimension="PRODUCT" Name="Total"/>
                            <Alea:Element Dimension="MONTHS" Name="May"/>
                            <Alea:Element Dimension="MEASURES" Name="Sales"/>
                        </Alea:CellCoordinates>
                        <Alea:Value>2.560000</Alea:Value>
                    </Alea:Splashing>
                </Alea:Request>
                </Alea:Document>
            */

            XElement document = RequestBase.CreateDocumentBase();
            XElement request = RequestBase.CreateRequestDocument(requestID, "Cube", "Splashing");
            XElement splashingSection = new XElement(RequestBase.OlapNamespace + "Splashing");

            request.Add(splashingSection);
            document.Add(request);

            splashingSection.SetAttributeValue("AllocationMode", mode);
            splashingSection.SetAttributeValue("Rounding", rounding ? "true" : "false");
            splashingSection.SetAttributeValue("DecimalPlaces", decimals.ToString());
            splashingSection.SetAttributeValue("ErrorCorrection", "true");
            splashingSection.SetAttributeValue("Undo", "false");
            splashingSection.SetAttributeValue("DoNotDeleteOnZero", notDeleteOnZero ? "true" : "false");

            XElement cellCoordinateSection = new XElement(RequestBase.OlapNamespace + "CellCoordinates");
            splashingSection.Add(cellCoordinateSection);

            cellCoordinateSection.SetAttributeValue("Purpose", "Target");
            cellCoordinateSection.SetAttributeValue("Cube", cube.Name);

            XElement element = new XElement(RequestBase.OlapNamespace + "Element");
            element.SetAttributeValue("Dimension", cube.Dimensions[0].Name);
            element.SetAttributeValue("Name", first);
            cellCoordinateSection.Add(element);

            element = new XElement(RequestBase.OlapNamespace + "Element");
            element.SetAttributeValue("Dimension", cube.Dimensions[1].Name);
            element.SetAttributeValue("Name", second);
            cellCoordinateSection.Add(element);

            for (int i = 0; i < cube.Dimensions.Count - 2; i++)
            {
                element = new XElement(RequestBase.OlapNamespace + "Element");
                element.SetAttributeValue("Dimension", cube.Dimensions[i + 2].Name);
                element.SetAttributeValue("Name", elementNames[i]);
                cellCoordinateSection.Add(element);
            }

            XElement valueSection = new XElement(RequestBase.OlapNamespace + "Value");
            splashingSection.Add(valueSection);

            valueSection.SetValue(value.ToString("G", CultureInfo.InvariantCulture));

            StringWriter requestString = new StringWriter();
            document.Save(requestString);
            return requestString.ToString();
        }
    }
}