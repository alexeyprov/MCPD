//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        [ScaffoldColumn(false)]
        public Guid OrderId { get; set; }

        [ScaffoldColumn(false)]
        [Required]
        public DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [ScaffoldColumn(false)]
        [Required]
        public decimal Total { get; set; }

        [Required]
        [StringLength(256)]
        public string Address { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [StringLength(256)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(256)]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(256)]
        public string Phone { get; set; }

        [ScaffoldColumn(false)]
        public IEnumerable<OrderDetail> OrderDetails { get; set; }

        [ScaffoldColumn(false)]
        public OrderStatus Status { get; set; }

        [ScaffoldColumn(false)]
        public string TransportPartner { get; set; }

        [ScaffoldColumn(false)]
        public Guid? TrackingId { get; set; }
    }
}