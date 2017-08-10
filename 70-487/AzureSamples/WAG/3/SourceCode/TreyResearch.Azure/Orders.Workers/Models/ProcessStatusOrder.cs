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

    public class OrderProcessStatus
    {
        public Guid OrderId { get; set; }
        public string ProcessStatus { get; set; }
        public string LockedBy { get; set; }
        public DateTime? LockedUntil { get; set; }
        public Order Order { get; set; }
    }
}
