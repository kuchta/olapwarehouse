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
    /// This class implements all methods to manage load from source requests.
    /// </summary>
    internal class TriggerLoadFromSourceRequest : RequestBase
    {
        /// <summary>
        /// Creates an OLAP XML Request for splashing a value to a calculated cell in a cube.
        /// </summary>
        /// <param name="requestID">The id of the request.</param>
        /// <returns>A string containing the XML request document ready to send.</returns>
        internal static string CreateRequest(string requestID)
        {
            /* Sample Request
                Input:
                <Alea:Document>
                  <Alea:Request RequestID="001" Class="Database" Method="StartDBLoad" />
                </Alea:Document>

                Output:
                <Alea:Document>
                  <Alea:Request RequestID="001">
                    <Alea:Return/>
                  </Alea:Request>
                </Alea:Document>

                Error:
                <Alea:Document>
                 <Alea:Request RequestID="001">
                    <Alea:Error ErrorID="error_code"/>
                  </Alea:Request>
                </Alea:Document>
            */

            XElement document = RequestBase.CreateDocumentBase();
            XElement request = RequestBase.CreateRequestDocument(requestID, "Database", "StartDBLoad");
            StringWriter requestString = new StringWriter();

            document.Add(request);
            document.Save(requestString);
            return requestString.ToString();
        }
    }
}