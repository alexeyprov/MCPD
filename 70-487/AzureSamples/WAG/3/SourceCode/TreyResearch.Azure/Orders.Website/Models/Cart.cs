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

    public class Cart
    {
        public int RecordId { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public string CartId { get; set; }

        public int Count { get; set; }

        public DateTime DateCreated { get; set; }
    }
}