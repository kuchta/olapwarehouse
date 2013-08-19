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
    /// Provides functions for managing XML requests.
    /// </summary>
    internal class RequestBase
    {
        /// <summary>
        /// Namespace used in all XML documents consumed and created by the OLAP server.
        /// </summary>
        internal static readonly XNamespace OlapNamespace = "http://www.misag.com";

        /// <summary>
        /// Creates a XML document intitialized as OLAP XML Request.
        /// </summary>
        /// <returns>The XML initialized for all OLAP XML Requests.</returns>
        internal static XElement CreateDocumentBase()
        {
            XElement document = new XElement(RequestBase.OlapNamespace + "Document");
            document.SetAttributeValue(XNamespace.Xmlns + "Alea", RequestBase.OlapNamespace);
            return document;
        }

        /// <summary>
        /// Creates a basic request document specifying the class and method of the request.
        /// </summary>
        /// <param name="requestID">The request ID.</param>
        /// <param name="requestClass">The request class.</param>
        /// <param name="method">The request method.</param>
        /// <returns>A request document that might need to be filled with further information.</returns>
        internal static XElement CreateRequestDocument(string requestID, string requestClass, string method)
        {
            XElement request = new XElement(RequestBase.OlapNamespace + "Request");
            request.SetAttributeValue("RequestID", requestID);
            request.SetAttributeValue("Class", requestClass);
            request.SetAttributeValue("Method", method);
            return request;
        }

        /// <summary>
        /// Checks whether a response for a XML request holds an error code.
        /// </summary>
        /// <param name="response">The response for any XML request.</param>
        /// <returns>The error code, if available. If an error was included but the code could not be read the function returns -1.
        /// If no error was included the function returns 0.</returns>
        internal static int OlapResponseErrorCode(string response)
        {
            XDocument responseDoc = XDocument.Parse(response);
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("Alea", OlapNamespace.NamespaceName);
            XElement error = responseDoc.XPathSelectElement("/Alea:Document/Alea:AleaRequest/Alea:Error", namespaceManager);
            if (error != null)
            {
                int errorCode = -1;
                int parseError = 0;
                string errorId = error.Attribute("ErrrorID").Value;
                if (int.TryParse(errorId, out parseError))
                {
                    errorCode = parseError;
                }
                return errorCode;
            }
            return 0;
        }
    }
}