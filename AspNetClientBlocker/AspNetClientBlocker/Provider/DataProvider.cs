using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration.Provider;

namespace AspNetClientBlocker.Provider
{
    /// <summary>
    /// Base provider class for storage system.
    /// </summary>
    public abstract class DataProvider : ProviderBase
    {
        /// <summary>
        /// Checks a single IP.
        /// </summary>
        /// <param name="ip">IP to check.</param>
        /// <param name="type">Determines if IP should be checked for trust level or blocked level.</param>
        /// <returns>Returns true if IP could be found in the list.</returns>
        public abstract bool CheckSingleIP(string ip, CheckType type);

        /// <summary>
        /// Checks a range of IPs.
        /// </summary>
        /// <param name="ip">IP to check.</param>        
        /// <param name="type">Determines if the range should be checked for trust level or blocked level.</param>
        /// <returns>Returns true if IP could be found in the list.</returns>
        public abstract bool CheckRangeIP(string ip, CheckType type);
    }
}
