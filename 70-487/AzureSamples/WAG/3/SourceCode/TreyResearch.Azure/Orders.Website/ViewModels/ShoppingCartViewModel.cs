//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.ViewModels
{
    using System.Collections.Generic;
    using Orders.Website.Models;

    public class ShoppingCartViewModel
    {
        public IEnumerable<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}