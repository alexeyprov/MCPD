//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Providers
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Globalization;
    using System.Security.Permissions;
    using System.Text;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.ServiceRuntime;

    internal static class Configuration
    {
        internal const string CsConfigStringPrefix = "CSConfigName";
        internal const string DefaultMembershipTableNameConfigurationString = "DefaultMembershipTableName";
        internal const string DefaultRoleTableNameConfigurationString = "DefaultRoleTableName";
        internal const string DefaultSessionTableNameConfigurationString = "DefaultSessionTableName";
        internal const string DefaultSessionContainerNameConfigurationString = "DefaultSessionContainerName";
        internal const string DefaultProfileContainerNameConfigurationString = "DefaultProfileContainerName";
        internal const string DefaultProviderApplicationNameConfigurationString = "DefaultProviderApplicationName";

        internal const string DefaultMembershipTableName = "Membership";
        internal const string DefaultRoleTableName = "Roles";
        internal const string DefaultSessionTableName = "Sessions";
        internal const string DefaultSessionContainerName = "sessionprovidercontainer";
        internal const string DefaultProfileContainerName = "profileprovidercontainer";
        internal const string DefaultProviderApplicationName = "appname";

        // internal static readonly string DefaultTableStorageEndpointConfigurationString = "TableStorageEndpoint";
        // internal static readonly string DefaultAccountNameConfigurationString = "AccountName";
        // internal static readonly string DefaultAccountSharedKeyConfigurationString = "AccountSharedKey";
        // internal static readonly string DefaultBlobStorageEndpointConfigurationString = "BlobStorageEndpoint";
        internal static readonly string DefaultStorageConfigurationString = "DataConnectionString";

        internal static readonly DateTime MinSupportedDateTime = DateTime.FromFileTime(0).ToUniversalTime().AddYears(200);
        internal static readonly int MaxStringPropertySizeInBytes = 64 * 1024;
        internal static readonly int MaxStringPropertySizeInChars = MaxStringPropertySizeInBytes / 2;        

        internal static string GetConfigurationSetting(string configurationString, string defaultValue)
        {
            return GetConfigurationSetting(configurationString, defaultValue, false);
        }

        /// <summary>
        /// Gets a configuration setting from application settings in the Web.config or App.config file. 
        /// When running in a hosted environment, configuration settings are read from the settings specified in 
        /// .cscfg files (i.e., the settings are read from the fabrics configuration system).
        /// </summary>
        [SecurityPermission(SecurityAction.Demand)]
        internal static string GetConfigurationSetting(string configurationString, string defaultValue, bool throwIfNull)
        {
            if (string.IsNullOrEmpty(configurationString))
            {
                throw new ArgumentException("The parameter configurationString cannot be null or empty.");
            }

            string ret = CloudConfigurationManager.GetSetting(configurationString);

            if (RoleEnvironment.IsAvailable)
            {
                // if there is a csc config name in the app settings, this config name even overloads the 
                // setting we have right now
                string refWebRet = TryGetAppSetting(CsConfigStringPrefix + configurationString);
                if (!string.IsNullOrEmpty(refWebRet))
                {
                    var cscRet = TryGetConfigurationSetting(refWebRet);
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

        [SecurityPermissionAttribute(SecurityAction.Demand)]
        internal static string GetConfigurationSettingFromNameValueCollection(NameValueCollection config, string valueName)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            if (valueName == null)
            {
                throw new ArgumentNullException("valueName");
            }

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

        internal static int GetIntValue(NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
        {
            string value = GetConfigurationSettingFromNameValueCollection(config, valueName);

            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            int valueAsInt;
            if (!Int32.TryParse(value, out valueAsInt))
            {
                if (zeroAllowed)
                {
                    throw new ConfigurationErrorsException(string.Format(CultureInfo.InstalledUICulture, "The value must be a non-negative 32-bit integer for property '{0}'.", valueName));
                }

                throw new ConfigurationErrorsException(string.Format(CultureInfo.InstalledUICulture, "The value must be a positive 32-bit integer for property '{0}'.", valueName));
            }

            if (zeroAllowed && valueAsInt < 0)
            {
                throw new ConfigurationErrorsException(string.Format(CultureInfo.InstalledUICulture, "The value must be a non-negative 32-bit integer for property '{0}'.", valueName));
            }

            if (!zeroAllowed && valueAsInt <= 0)
            {
                throw new ConfigurationErrorsException(string.Format(CultureInfo.InstalledUICulture, "The value must be a positive 32-bit integer for property '{0}'.", valueName));
            }

            if (maxValueAllowed > 0 && valueAsInt > maxValueAllowed)
            {
                throw new ConfigurationErrorsException(string.Format(CultureInfo.InstalledUICulture, "The value '{0}' can not be greater than '{1}'.", valueName, maxValueAllowed.ToString(CultureInfo.InstalledUICulture)));
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

        internal static string GetInitExceptionDescription(StorageCredentials credentials, Uri tableBaseUri, Uri blobBaseUri)
        {
            var builder = new StringBuilder();
            builder.Append(GetInitExceptionDescription(credentials, tableBaseUri, "table storage configuration"));
            builder.Append(GetInitExceptionDescription(credentials, blobBaseUri, "blob storage configuration"));
            return builder.ToString();
        }

        internal static string GetInitExceptionDescription(StorageCredentials info, Uri baseUri, string desc)
        {
            var builder = new StringBuilder();
            builder.Append("The reason for this exception is typically that the endpoints are not correctly configured. " + Environment.NewLine);
            if (info == null)
            {
                builder.Append("The current " + desc + " is null. Please specify a table endpoint!" + Environment.NewLine);
            }
            else
            {
                builder.Append("The current " + desc + " is: " + baseUri + Environment.NewLine);
                builder.Append("Please also make sure that the account name and the shared key are specified correctly. This information cannot be shown here because of security reasons.");
            }

            return builder.ToString();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Make sure that no error condition prevents environment from reading service configuration.")]
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

        [SecurityPermissionAttribute(SecurityAction.Demand)]
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
