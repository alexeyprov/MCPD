//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Permissions;
    using Microsoft.WindowsAzure.Diagnostics;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Orders.Shared.Helpers;

    public class WebRole : RoleEntryPoint
    {
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public override bool OnStart()
        {
            RoleEnvironment.Changing += this.RoleEnvironmentChanging;
            RoleEnvironment.Changed += this.RoleEnvironmentChanged;

            ConfigureTraceListener(RoleEnvironment.GetConfigurationSettingValue("TraceEventTypeFilter"));

            return base.OnStart();
        }

        private static void ConfigureTraceListener(string traceEventTypeFilter)
        {
            SourceLevels sourceLevels;
            if (Enum.TryParse(traceEventTypeFilter, true, out sourceLevels))
            {
                TraceHelper.Configure(sourceLevels);
            }
        }

        private void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            // for any configuration setting change except TraceEventTypeFilter
            if (e.Changes.OfType<RoleEnvironmentConfigurationSettingChange>().Any(change => change.ConfigurationSettingName != "TraceEventTypeFilter"))
            {
                // Set e.Cancel to true to restart this role instance
                e.Cancel = true;
            }
        }

        private void RoleEnvironmentChanged(object sender, RoleEnvironmentChangedEventArgs e)
        {
            // configure trace listener for any changes to EnableTableStorageTraceListener 
            if (e.Changes.OfType<RoleEnvironmentConfigurationSettingChange>().Any(change => change.ConfigurationSettingName == "TraceEventTypeFilter"))
            {
                ConfigureTraceListener(RoleEnvironment.GetConfigurationSettingValue("TraceEventTypeFilter"));
            }
        }
    }
}
