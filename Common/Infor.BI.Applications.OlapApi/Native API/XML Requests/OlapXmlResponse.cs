using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Infor.BI.Applications.OlapApi.Native;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Centralizes the error handling of XML responses sent by the OLAP server.
    /// </summary>
    public class OlapXmlResponse
    {
        private const string ErrorIdAttribute = "ErrorID";

        private XDocument _document;
        private XmlNamespaceManager _namespaceManager;
        private List<string> _errors;

        /// <summary>
        /// Creates a new OlapXmlResponse instance for a certain response document.
        /// Every response document will bec checked for a global error element
        /// immediately.
        /// </summary>
        /// <param name="response">Response document to be evaluated.</param>
        public OlapXmlResponse(string response)
        {
            _errors = new List<string>();
            _document = XDocument.Parse(response);
            _namespaceManager = new XmlNamespaceManager(new NameTable());
            _namespaceManager.AddNamespace("Alea", RequestBase.OlapNamespace.NamespaceName);
            XElement error = _document.XPathSelectElement("/Alea:Document/Alea:Request/Alea:Error", _namespaceManager);
            if (error != null)
            {
                string errorId = "unknown";
                if (error.Attribute(ErrorIdAttribute) != null)
                {
                    errorId = error.Attribute(ErrorIdAttribute).Value;
                    _errors.Add(errorId);
                }
                NativeOlapApi.LogError(LogEvent.ErrorInXmlResponse, "Olap server returned the following error: " + errorId);
            }
        }

        /// <summary>
        /// Checks for errors that are specific to the CreateDimensionElement request.
        /// </summary>
        /// <param name="dimension">Dimension to create element for.</param>
        /// <param name="element">The element to be created.</param>
        /// <returns>True, if the response document contains any errors. False, otherwise.</returns>
        public bool CheckForErrorsInCreateDimensionElement(string dimension, string element)
        {
            bool hasErrors = false;
            XElement errorElement = _document.XPathSelectElement("/Alea:Document/Alea:Request/Alea:Return/Alea:Dimension/Alea:Elements", _namespaceManager);
            if (errorElement != null)
            {
                string[] returnValues = errorElement.Value.Split('\t');
                string olapError = string.Empty;
                if (returnValues != null && returnValues.Length > 0)
                {
                    olapError = returnValues[0];
                    _errors.Add(olapError);
                    hasErrors = true;
                }
                // TODO 10.5: localize error messages
                string message = "The element " + element + " could not be created in dimension " + dimension + ". The error code from the server was: " + olapError;
                NativeOlapApi.LogError(LogEvent.ErrorInXmlResponse, message);
            }
            return hasErrors;
        }

        /// <summary>
        /// Checks for errors that are specific to the RenameDimensionElement request.
        /// </summary>
        /// <param name="dimension">Dimension to rename element for.</param>
        /// <param name="element">The element to be renamed.</param>
        /// <param name="newName">The element's new name.</param>
        /// <returns>True, if the response document contains any errors. False, otherwise.</returns>
        public bool CheckForErrorsInRenameDimensionElement(string dimension, string element, string newName)
        {
            bool hasErrors = false;
            XElement errorElement = _document.XPathSelectElement("/Alea:Document/Alea:Request/Alea:Return/Alea:Dimension/Alea:Error", _namespaceManager);
            if (errorElement != null)
            {
                hasErrors = true;
                string errorID = "unknown";
                XAttribute errorAttr = errorElement.Attribute("ErrorID");
                if (errorAttr.Value != null)
                {
                    errorID = errorAttr.Value;
                }
                // TODO 10.5: localize error messages
                string message = "The element " + element + " could not be renamed to '" + newName + "' in dimension " + dimension + ". The error code from the server was: " + errorID;
                NativeOlapApi.LogError(LogEvent.ErrorInXmlResponse, message);
            }
            return hasErrors;
        }

        /// <summary>
        /// Checks for errors that are specific to the ResolveUniqueName request.
        /// </summary>
        /// <param name="name">The name of the elment to be resolved.</param>
        /// <returns>The element's unique name, if everything is fine. Otherwise, an empty string will be returned.</returns>
        public string CheckForErrorsInResolveUniqueName(string name)
        {
            XElement nativeNameElement = _document.XPathSelectElement("/Alea:Document/Alea:Request/Alea:Return/Alea:Element", _namespaceManager);
            if (nativeNameElement == null)
            {
                // TODO 10.5: localize error messages
                AddError(LogEvent.ResponseDoesNotContainNativeNameElement, "Could not resolve unique name '" + name + "', because the server response does not contain a name element.");
                return string.Empty;
            }

            XAttribute errorAttr = nativeNameElement.Attribute("Error");
            if (errorAttr != null)
            {
                // TODO 10.5: localize error messages
                AddError(LogEvent.NameElementHasErrorAttribute, "Could not resolve unique name '" + name + "', because the name element in the server response contains an error attribute: " + errorAttr.Value);
                return string.Empty;
            }

            XAttribute nameAttr = nativeNameElement.Attribute("Name");
            if (nameAttr == null)
            {
                // TODO 10.5: localize error messages
                AddError(LogEvent.ResponseDoesNotContainNativeNameAttribute, "Could not resolve unique name '" + name + "', because the server response does not contain a name attribute.");
                return string.Empty;
            }

            return nameAttr.Value;
        }

        /// <summary>
        /// Returns the parsed XML response.
        /// </summary>
        public XDocument Document
        {
            get
            {
                return _document;
            }
        }

        /// <summary>
        /// Returns true, if the response contains at least one error.
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return _errors.Count != 0;
            }
        }

        /// <summary>
        /// Returns the list of all errors that have been found in the response.
        /// </summary>
        public List<string> Errors
        {
            get
            {
                return _errors;
            }
        }

        private void AddError(LogEvent eventId, string message)
        {
            _errors.Add(message);
            NativeOlapApi.LogError(eventId, message);
        }
    }
}
