//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Workers.Stores
{
    using Orders.Shared;
    using Orders.Workers.Stores.Entities;

    public static class TreyResearchModelFactory
    {
        public static TreyResearchModel CreateContext()
        {
            return new TreyResearchModel(CloudConfiguration.GetConfigurationSetting("TreyResearchModel", null));
        }
    }
}
