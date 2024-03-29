﻿using System.Configuration;

namespace AspNetClientBlocker.Provider
{
    /// <summary>
    /// Represents the configuration section for DataProvider.
    /// </summary>
    public class DataProviderConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Represents the "providers" section.
        /// </summary>
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get
            {
                return (ProviderSettingsCollection)base["providers"];
            }
        }

        /// <summary>
        /// Represents the "default" section for default provider.
        /// </summary>
        [ConfigurationProperty("default", DefaultValue = "XmlProvider")]
        public string Default
        {
            get
            {
                return (string)base["default"];
            }
            set
            {
                base["default"] = value;
            }
        }
    }
}
