using System;
using System.Collections.Specialized;
using System.Web;
using System.Xml;

namespace AspNetClientBlocker.Provider
{
    /// <summary>
    /// XML implementation for data provider.
    /// </summary>
    public class XmlDataProvider : DataProvider
    {
        private string _filePath = string.Empty;

        /// <summary>
        /// Initialization.
        /// </summary>
        /// <param name="name">The name of the provider.</param>
        /// <param name="config">A NameValueCollection of custom configuration attributes and their values.</param>
        public override void Initialize(string name, NameValueCollection config)
        {
            if (!string.IsNullOrEmpty(config["filePath"]))
            {
                string virtualPath = config["filePath"];

                HttpContext context = HttpContext.Current;
                this._filePath = context.Request.MapPath(virtualPath);
            }
            else
                throw new ArgumentNullException("File path must be set.");
            base.Initialize(name, config);
        }

        /// <summary>
        /// Checks a single IP.
        /// </summary>
        /// <param name="ip">IP to check.</param>
        /// <param name="type">Check type.</param>
        /// <returns>Returns true if IP could be found in the list and false if it couldn't be found.</returns>
        public override bool CheckSingleIP(string ip, CheckType type)
        {
            if (type == CheckType.Trusted)
                return IsTrusted(ip, ValidationType.Single);
            else
                return IsBlocked(ip, ValidationType.Single);
        }

        /// <summary>
        /// Checks an IP range.
        /// </summary>
        /// <param name="ip">IP to check.</param>
        /// <param name="type">Check type.</param>
        /// <returns>Returns true if IP could be found in the list and false if it couldn't be found.</returns>
        public override bool CheckRangeIP(string ip, CheckType type)
        {
            if (type == CheckType.Trusted)
                return IsTrusted(ip, ValidationType.Range);
            else
                return IsBlocked(ip, ValidationType.Range);
        }

        /// <summary>
        /// Checks an IP to see if it's trusted.
        /// </summary>
        /// <param name="ip">IP to check.</param>
        /// <param name="type">Validation type.</param>
        /// <returns>Returns true if IP is trusted.</returns>
        private bool IsTrusted(string ip, ValidationType type)
        {
            if (type == ValidationType.Single)
                return CheckSingle(ip, "/ips/trusted/ip");
            else
                return CheckRange(ip, "/ips/trusted/range");
        }

        /// <summary>
        /// Checks an IP to see if it's blocked.
        /// </summary>
        /// <param name="ip">IP to check.</param>
        /// <param name="type">Validation type.</param>
        /// <returns>Returns true if IP is blocked.</returns>
        private bool IsBlocked(string ip, ValidationType type)
        {
            if (type == ValidationType.Single)
                return CheckSingle(ip, "/ips/blocked/ip");
            else
                return CheckRange(ip, "/ips/blocked/range");
        }

        /// <summary>
        /// Checks an IP to see if it's in the list of single IPs.
        /// </summary>
        /// <param name="ip">IP to check.</param>
        /// <param name="query">XPath query string.</param>
        /// <returns>Returns true if IP is in the list.</returns>
        private bool CheckSingle(string ip, string query)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(this._filePath);


            XmlNodeList nodeList = xml.SelectNodes(query);


            foreach (XmlNode item in nodeList)
            {
                if (item.Attributes["value"].InnerText == ip)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks an IP to see if it's in the list of IP ranges.
        /// </summary>
        /// <param name="ip">IP to check.</param>
        /// <param name="query">XPath query string.</param>
        /// <returns>Returns true if IP is in the list.</returns>
        private bool CheckRange(string ip, string query)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(this._filePath);


            XmlNodeList nodeList = xml.SelectNodes(query);

            foreach (XmlNode item in nodeList)
            {
                if ((CompareIps.IsGreaterOrEqual(ip, item.Attributes["lower"].InnerText)) &&
                    (CompareIps.IsLessOrEqual(ip, item.Attributes["upper"].InnerText)))
                    return true;
            }

            return false;
        }
    }
}
