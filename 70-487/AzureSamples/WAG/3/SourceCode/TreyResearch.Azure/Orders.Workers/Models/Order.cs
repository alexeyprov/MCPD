//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Workers.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserName { get; set; }
        public decimal Total { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public OrderStatus Status { get; set; }
        public string TransportPartner { get; set; }
        public Guid? TrackingId { get; set; }
    }
}
