//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace TransportPartner
{
    using System;

    public class ActiveOrder
    {
        public Guid OrderId { get; set; }
        public string ShippingAddress { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public string ReplyTo { get; set; }
        public string ReplyToNamespace { get; set; }
        public string SwtAcsNamespace { get; set; }
    }
}
