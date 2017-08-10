//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.DataStores
{
    using Orders.Shared;
    using Orders.Website.DataStores.Entities;

    public static class TreyResearchModelFactory
    {
        public static TreyResearchDataModelContainer CreateContext()
        {
            return new TreyResearchDataModelContainer(CloudConfiguration.GetConfigurationSetting("TreyResearchDataModelContainer", null));
        }
    }
}