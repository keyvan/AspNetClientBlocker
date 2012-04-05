using AspNetClientBlocker.Provider;

namespace AspNetClientBlocker
{
    /// <summary>
    /// This class checks an IP to:
    /// 1- Be in trusted IPs
    /// 2- Be in blocked range
    /// Then returns true if it has been blocked and false if not
    /// </summary>
    internal class CheckIPs
    {
        /// <summary>
        /// Public constrcutor.
        /// </summary>
        public CheckIPs()
        {
        }

        /// <summary>
        /// Checks an IP to see if it's blocked or not.
        /// </summary>
        /// <param name="ip">IP address.</param>
        /// <returns>If true then IP is trusted and if false IP is blocked.</returns>
        public bool CheckIP(string ip)
        {
            if ((IsTrusted(ip, ValidationType.Single) == true) ||
                (IsTrusted(ip, ValidationType.Range) == true))
            {
                return true;
            }
            if ((IsBlocked(ip, ValidationType.Single) == true) ||
                (IsBlocked(ip, ValidationType.Range) == true))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks an IP address to see if it's trusted.
        /// </summary>
        /// <param name="ip">IP to check.</param>
        /// <param name="type">Single IP or a range.</param>
        /// <returns>Boolean value that determines if IP is trusted.</returns>
        private bool IsTrusted(string ip, ValidationType type)
        {
            if (type == ValidationType.Single)
                return DataProviderManager.Provider.CheckSingleIP(ip, CheckType.Trusted);
            else
                return DataProviderManager.Provider.CheckRangeIP(ip, CheckType.Trusted);
        }

        /// <summary>
        /// Checks an IP address to see if it's blocked.
        /// </summary>
        /// <param name="ip">IP to check.</param>
        /// <param name="type">Single IP or a range.</param>
        /// <returns>Boolean value that determines if IP is blocked.</returns>
        private bool IsBlocked(string ip, ValidationType type)
        {
            if (type == ValidationType.Single)
                return DataProviderManager.Provider.CheckSingleIP(ip, CheckType.Blocked);
            else
                return DataProviderManager.Provider.CheckRangeIP(ip, CheckType.Blocked);
        }
    }
}
