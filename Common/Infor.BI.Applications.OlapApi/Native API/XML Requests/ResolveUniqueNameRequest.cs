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
    /// This class implements all methods to manage requests to resolve unique names.
    /// </summary>
    internal class ResolveUniqueNameRequest : RequestBase
    {
        /// <summary>
        /// Creates a request to resolve a unique name.
        /// </summary>
        /// <param name="requestID">The request ID.</param>
        /// <param name="uniqueNames">The names to resolve.</param>
        /// <returns>The native OLAP IDs for the unique names.</returns>
        internal static XElement CreateRequest(string requestID, string[] uniqueNames)
        {
            XElement request = RequestBase.CreateRequestDocument(requestID, "Database", "ResolveUniqueName");
            foreach (string name in uniqueNames)
            {
                XElement uniqueName = new XElement(RequestBase.OlapNamespace + "UniqueName");
                uniqueName.Value = name;
                request.Add(uniqueName);
            }
            return request;
        }
    }
}