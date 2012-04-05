namespace AspNetClientBlocker
{
    /// <summary>
    /// Enumeration to check for single IPs or ranges
    /// </summary>
    public enum ValidationType
    {
        /// <summary>
        /// Single IP.
        /// </summary>
        Single = 0,
        /// <summary>
        /// An IP range.
        /// </summary>
        Range = 1
    }
}