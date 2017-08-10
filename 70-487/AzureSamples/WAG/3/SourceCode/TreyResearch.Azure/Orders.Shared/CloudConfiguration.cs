//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Shared
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using Microsoft.WindowsAzure.ServiceRuntime;

    public static class CloudConfiguration
    {
        internal const string CsConfigStringPrefix = "CSConfigName";

        public static string GetConfigurationSetting(string configurationString, string defaultValue)
        {
            return GetConfigurationSetting(configurationString, defaultValue, false);
        }

        /// <summary>
        /// Gets a configuration setting from application settings in the Web.config or App.config file. 
        ///   When running in a hosted environment, configuration settings are read from the settings specified in 
        ///   .cscfg files (i.e., the settings are read from the fabrics configuration system).
        /// </summary>
        /// <param name="configurationString">
        /// The configuration String.
        /// </param>
        /// <param name="defaultValue">
        /// The default Value.
        /// </param>
        /// <param name="throwIfNull">
        /// The throw If Null.
        /// </param>
        /// <returns>
        /// The get configuration setting.
        /// </returns>
        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "This for the Windows Azure SDK.")]
        public static string GetConfigurationSetting(string configurationString, string defaultValue, bool throwIfNull)
        {
            if (string.IsNullOrEmpty(configurationString))
            {
                throw new ArgumentException("The parameter configurationString cannot be null or empty.");
            }

            // first, try to read from appsettings
            string ret = TryGetAppSetting(configurationString);

            // settings in the csc file overload settings in Web.config
            if (RoleEnvironment.IsAvailable)
            {
                string cscRet = TryGetConfigurationSetting(configurationString);
                if (!string.IsNullOrEmpty(cscRet))
                {
                    ret = cscRet;
                }

                // if there is a csc config name in the app settings, this config name even overloads the 
                // setting we have right now
                string refWebRet = TryGetAppSetting(CsConfigStringPrefix + configurationString);
                if (!string.IsNullOrEmpty(refWebRet))
                {
                    cscRet = TryGetConfigurationSetting(refWebRet);
                    if (!string.IsNullOrEmpty(cscRet))
                    {
                        ret = cscRet;
                    }
                }
            }

            // if we could not retrieve any configuration string set return value to the default value
            if (string.IsNullOrEmpty(ret) && defaultValue != null)
            {
                ret = defaultValue;
            }

            if (string.IsNullOrEmpty(ret) && throwIfNull)
            {
                throw new ConfigurationErrorsException(string.Format(CultureInfo.InstalledUICulture, "Cannot find configuration string {0}.", configurationString));
            }

            return ret;
        }

        internal static bool GetBooleanValue(NameValueCollection config, string valueName, bool defaultValue)
        {
            string value = GetConfigurationSettingFromNameValueCollection(config, valueName);

            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            bool result;
            if (bool.TryParse(value, out result))
            {
                return result;
            }

            throw new ConfigurationErrorsException(string.Format(CultureInfo.InstalledUICulture, "The value must be boolean (true or false) for property '{0}'.", valueName));
        }

        internal static string GetConfigurationSettingFromNameValueCollection(NameValueCollection config, string valueName)
        {
            Guard.CheckArgumentNull(config, "config");
            Guard.CheckArgumentNull(valueName, "valueName");

            string value = config[valueName];

            if (RoleEnvironment.IsAvailable)
            {
                // settings in the hosting configuration are stronger than settings in app config
                string cscRet = TryGetConfigurationSetting(valueName);
                if (!string.IsNullOrEmpty(cscRet))
                {
                    value = cscRet;
                }

                // if there is a csc config name in the app settings, this config name even overloads the 
                // setting we have right now
                string refWebRet = config[CsConfigStringPrefix + valueName];
                if (!string.IsNullOrEmpty(refWebRet))
                {
                    cscRet = TryGetConfigurationSetting(refWebRet);
                    if (!string.IsNullOrEmpty(cscRet))
                    {
                        value = cscRet;
                    }
                }
            }

            return value;
        }

        internal static int GetIntValue(NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
        {
            string value = GetConfigurationSettingFromNameValueCollection(config, valueName);

            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            int valueAsInt;
            if (!int.TryParse(value, out valueAsInt))
            {
                if (zeroAllowed)
                {
                    throw new ConfigurationErrorsException(
                        string.Format(CultureInfo.InstalledUICulture, "The value must be a non-negative 32-bit integer for property '{0}'.", valueName));
                }

                throw new ConfigurationErrorsException(string.Format(CultureInfo.InstalledUICulture, "The value must be a positive 32-bit integer for property '{0}'.", valueName));
            }

            if (zeroAllowed && valueAsInt < 0)
            {
                throw new ConfigurationErrorsException(
                    string.Format(CultureInfo.InstalledUICulture, "The value must be a non-negative 32-bit integer for property '{0}'.", valueName));
            }

            if (!zeroAllowed && valueAsInt <= 0)
            {
                throw new ConfigurationErrorsException(string.Format(CultureInfo.InstalledUICulture, "The value must be a positive 32-bit integer for property '{0}'.", valueName));
            }

            if (maxValueAllowed > 0 && valueAsInt > maxValueAllowed)
            {
                throw new ConfigurationErrorsException(
                    string.Format(CultureInfo.InvariantCulture, "The value '{0}' can not be greater than '{1}'.", valueName, maxValueAllowed.ToString(CultureInfo.InstalledUICulture)));
            }

            return valueAsInt;
        }

        internal static string GetStringValue(NameValueCollection config, string valueName, string defaultValue, bool nullAllowed)
        {
            string value = GetConfigurationSettingFromNameValueCollection(config, valueName);

            if (string.IsNullOrEmpty(value) && nullAllowed)
            {
                return null;
            }

            if (string.IsNullOrEmpty(value) && defaultValue != null)
            {
                return defaultValue;
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ConfigurationErrorsException(string.Format(CultureInfo.InstalledUICulture, "The parameter '{0}' must not be empty.", valueName));
            }

            return value;
        }

        internal static string GetStringValueWithGlobalDefault(NameValueCollection config, string valueName, string defaultConfigString, string defaultValue, bool nullAllowed)
        {
            string value = GetConfigurationSettingFromNameValueCollection(config, valueName);

            if (string.IsNullOrEmpty(value))
            {
                value = GetConfigurationSetting(defaultConfigString, null);
            }

            if (string.IsNullOrEmpty(value) && nullAllowed)
            {
                return null;
            }

            if (string.IsNullOrEmpty(value) && defaultValue != null)
            {
                return defaultValue;
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ConfigurationErrorsException(string.Format(CultureInfo.InstalledUICulture, "The parameter '{0}' must not be empty.", valueName));
            }

            return value;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "Make sure that no error condition prevents environment from reading service configuration.")]
        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "This for the Windows Azure SDK.")]
        internal static string TryGetAppSetting(string configName)
        {
            string ret;
            try
            {
                ret = ConfigurationManager.AppSettings[configName];
            }
            catch (Exception)
            {
                // some exception happened when accessing the app settings section
                // most likely this is because there is no app setting file
                // this is not an error because configuration settings can also be located in the cscfg file, and explicitly 
                // all exceptions are captured here
                return null;
            }

            return ret;
        }

        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "This for the Windows Azure SDK.")]
        private static string TryGetConfigurationSetting(string configName)
        {
            string ret;
            try
            {
                ret = RoleEnvironment.GetConfigurationSettingValue(configName);
            }
            catch (RoleEnvironmentException)
            {
                return null;
            }

            return ret;
        }
    }
}