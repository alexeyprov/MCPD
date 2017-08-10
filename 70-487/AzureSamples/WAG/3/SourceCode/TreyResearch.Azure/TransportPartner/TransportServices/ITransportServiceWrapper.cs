//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace TransportPartner.TransportServices
{
    using System;

    public interface ITransportServiceWrapper
    {
        // Posts an order to the Fabrikam Transport Partner and receives a tracking id.
        Guid RequestShipment(ActiveOrder order);
    }
}
