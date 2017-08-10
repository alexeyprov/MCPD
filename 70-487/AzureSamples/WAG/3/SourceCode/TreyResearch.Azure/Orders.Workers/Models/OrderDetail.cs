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

    public class OrderDetail
    {
        public int ProductId { get; set; }

        public Guid OrderId { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }
    }
}
