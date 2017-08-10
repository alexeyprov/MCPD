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

    [ServiceContract]
    public interface IOrdersStatistics
    {
        [OperationContract]
        double SalesByQuarter(int quarter);

        [OperationContract]
        double SalesByRegion(string region);
    }
}
