//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Shared.Communication.Messages
{
    using System;

    public class OrderStatusUpdateMessage
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public Guid TrackingId { get; set; }
        public string TransportPartnerName { get; set; }
    }
}
