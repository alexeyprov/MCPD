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
    using System.Collections.Generic;

    public class TransportServiceWrapper : ITransportServiceWrapper
    {
        private readonly IList<ActiveOrder> activeOrders;

        public TransportServiceWrapper()
        {
            this.activeOrders = new List<ActiveOrder>();
        }

        public Guid RequestShipment(ActiveOrder order)
        {
            // The steps for interacting with a transport partner that exposes services instead of using Trey Research's provided functionality.
            // 1. The order is posted in the required format by the Transport Partner (a web service for example).
            // 2. The Transport partner will process the order, and return a tracking id.
            order.Status = "received";
            this.activeOrders.Add(order);
            return Guid.NewGuid();
        }
    }
}
