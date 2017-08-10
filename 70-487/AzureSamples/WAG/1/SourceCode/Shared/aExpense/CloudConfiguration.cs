//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense
{
    using System.Configuration;
    using Microsoft.WindowsAzure;

    public static class CloudConfiguration
    {
        public static CloudStorageAccount GetStorageAccount(string settingName)
        {
            var connString = CloudConfigurationManager.GetSetting(settingName);
            return CloudStorageAccount.Parse(connString);
        }   
     
        public static string GetConnectionString(string settingName)
        {
            // Get the connection string from the service configuration file.
            // If it fails, will look for the setting in the appSettings section of the web.config
            var connString = CloudConfigurationManager.GetSetting(settingName);

            if (string.IsNullOrWhiteSpace(connString))
            {
                // Fall back to the connectionStrings section of the web.config
                return ConfigurationManager.ConnectionStrings[settingName].ConnectionString;
            }

            return connString;
        }
    }
}
