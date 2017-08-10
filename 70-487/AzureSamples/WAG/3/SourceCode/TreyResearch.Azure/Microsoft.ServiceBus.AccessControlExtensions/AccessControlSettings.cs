//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


// 
// (c) Microsoft Corporation. All rights reserved.
// 
namespace Microsoft.ServiceBus.AccessControlExtensions
{
    public class AccessControlSettings
    {
        string accessControlManagementPath = "v2/mgmt/service/";
        string managementServiceIdentityName = "SBManagementClient";
        string accessControlServiceAddress = "accesscontrol.windows.net";
        
        public AccessControlSettings(string namespaceName, string managementKey)
        {
            this.ServiceNamespace = namespaceName + "-sb";
            this.AccessControlServiceAddress = ServiceBusEnvironment.DefaultIdentityHostName;
            this.ManagementServiceIdentityKey = managementKey;
        }

        public string AccessControlManagementPath
        {
            get { return this.accessControlManagementPath; }
            set { this.accessControlManagementPath = value; }
        }

        public string ManagementServiceIdentityName
        {
            get { return this.managementServiceIdentityName; }
            set { this.managementServiceIdentityName = value; }
        }

        public string ManagementServiceIdentityKey { get; set; }

        public string AccessControlServiceAddress
        {
            get { return this.accessControlServiceAddress; }
            set { this.accessControlServiceAddress = value; }
        }

        public string ServiceNamespace { get; set; }
    }
}