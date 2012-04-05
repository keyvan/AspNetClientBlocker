namespace AspNetClientBlocker
{
    /// <summary>
    /// Enumeration to check for trusted or blocked IPs and ranges
    /// </summary>
    public enum CheckType
    {
        /// <summary>
        /// Trusted.
        /// </summary>
        Trusted = 0,
        /// <summary>
        /// Blocked.
        /// </summary>
        Blocked = 1
    }
}