//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace ExternalDataAnalyzer
{
    using System.ServiceModel;

    public interface IOrdersStatisticsChannel : IOrdersStatistics, IClientChannel
    {
    }
}