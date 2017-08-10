//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.DataStores
{
    using System.Collections.Generic;
    using Orders.Website.Models;

    public interface ICartStore
    {
        Cart FindItem(int recordId);

        Cart FindByShoppingCartId(int recordId, string shoppingCartId);

        IEnumerable<Cart> FindCartItems(string cartId);

        void DeleteItems(string cartId);

        void AddItem(string cartId, Product product);

        int Delete(int itemId);

        decimal GetTotal(string cartId);

        int GetCount(string cartId);

        void UpdateCart(string userName, string cartId);
    }
}
