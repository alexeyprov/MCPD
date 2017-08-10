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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.Practices.TransientFaultHandling;
    using Models;
    using Orders.Shared.Helpers;
    using Guard = Orders.Shared.Guard;

    public class CartStore : ICartStore
    {
        private readonly RetryPolicy sqlCommandRetryPolicy;

        public CartStore()
        {
            // this policy is defined in the configurationfile
            this.sqlCommandRetryPolicy = RetryPolicyFactory.GetDefaultSqlCommandRetryPolicy();
            this.sqlCommandRetryPolicy.Retrying += (sender, args) => TraceHelper.TraceInformation("Retry in CartStore - Count:{0}, Delay:{1}, Exception:{2}", args.CurrentRetryCount, args.Delay, args.LastException);
        }

        public Cart FindItem(int recordId)
        {
            using (var database = TreyResearchModelFactory.CreateContext())
            {
                return this.sqlCommandRetryPolicy.ExecuteAction(
                        () =>
                        database.Carts.Include("Product").Where(c => c.RecordId == recordId).Select(
                            c =>
                            new Cart
                                {
                                    RecordId = c.RecordId,
                                    CartId = c.CartId,
                                    ProductId = c.ProductId,
                                    Count = c.Count,
                                    DateCreated = c.DateCreated,
                                    Product =
                                        new Product
                                            {
                                                ProductId = c.Product.ProductId,
                                                Description = c.Product.Description,
                                                Price = c.Product.Price
                                            }
                                }).FirstOrDefault());
            }
        }

        public int Delete(int itemId)
        {
            using (var database = TreyResearchModelFactory.CreateContext())
            {
                // Get the cart
                var cartItem =
                    this.sqlCommandRetryPolicy.ExecuteAction(
                        () => database.Carts.SingleOrDefault(i => i.RecordId == itemId));

                int itemCount = 0;

                if (cartItem != null)
                {
                    if (cartItem.Count > 1)
                    {
                        cartItem.Count--;
                        itemCount = cartItem.Count;
                    }
                    else
                    {
                        database.Carts.DeleteObject(cartItem);
                    }

                    this.sqlCommandRetryPolicy.ExecuteAction(() => database.SaveChanges());
                }

                return itemCount;
            }
        }

        public Cart FindByShoppingCartId(int recordId, string shoppingCartId)
        {
            Guard.CheckArgumentNullOrEmpty(shoppingCartId, "shoppingCartId");

            using (var database = TreyResearchModelFactory.CreateContext())
            {
                // this policy is defined in the configurationfile
                return this.sqlCommandRetryPolicy.ExecuteAction(
                        () =>
                        database.Carts.Where(c => c.RecordId == recordId && c.CartId == shoppingCartId).Select(
                            c =>
                            new Cart
                                {
                                    RecordId = c.RecordId,
                                    CartId = c.CartId,
                                    Count = c.Count,
                                    DateCreated = c.DateCreated,
                                    ProductId = c.ProductId,
                                    Product =
                                        new Product
                                            {
                                                ProductId = c.Product.ProductId,
                                                Description = c.Product.Description,
                                                Price = c.Product.Price
                                            }
                                }).FirstOrDefault());
            }
        }

        public IEnumerable<Cart> FindCartItems(string cartId)
        {
            using (var database = TreyResearchModelFactory.CreateContext())
            {
                return this.sqlCommandRetryPolicy.ExecuteAction(
                        () =>
                        database.Carts.Include("Product").Where(cart => cart.CartId == cartId).Select(
                            c =>
                            new Cart
                                {
                                    RecordId = c.RecordId,
                                    CartId = c.CartId,
                                    DateCreated = c.DateCreated,
                                    Count = c.Count,
                                    ProductId = c.ProductId,
                                    Product =
                                        new Product
                                            {
                                                ProductId = c.Product.ProductId,
                                                Description = c.Product.Description,
                                                Price = c.Product.Price
                                            }
                                }).ToList());
            }
        }

        public void DeleteItems(string cartId)
        {
            using (var database = TreyResearchModelFactory.CreateContext())
            {
                var cartItems =
                    this.sqlCommandRetryPolicy.ExecuteAction(() => database.Carts.Where(cart => cart.CartId == cartId));

                foreach (var cartItem in cartItems)
                {
                    database.Carts.DeleteObject(cartItem);
                }

                // Save changes
                this.sqlCommandRetryPolicy.ExecuteAction(() => database.SaveChanges());
            }        
        }

        public void AddItem(string cartId, Product product)
        {
            Guard.CheckArgumentNull(product, "product");

            using (var database = TreyResearchModelFactory.CreateContext())
            {
                // Get the matching cart and album instances            
                var cartItem =
                    this.sqlCommandRetryPolicy.ExecuteAction(
                        () =>
                        database.Carts.SingleOrDefault(c => c.CartId == cartId && c.ProductId == product.ProductId));

                if (cartItem == null)
                {
                    // Create a new cart item if no cart item exists
                    cartItem = new Entities.Cart
                    {
                        ProductId = product.ProductId,
                        CartId = cartId,
                        Count = 1,
                        DateCreated = DateTime.Now
                    };

                   database.Carts.AddObject(cartItem);
                }
                else
                {
                    // If the item does exist in the cart, then add one to the quantity
                    cartItem.Count++;
                }

                // Save changes
                this.sqlCommandRetryPolicy.ExecuteAction(() => database.SaveChanges());
            }
        }

        public decimal GetTotal(string cartId)
        {
            Guard.CheckArgumentNullOrEmpty(cartId, "cartId");

            using (var database = TreyResearchModelFactory.CreateContext())
            {
                decimal? total =
                    this.sqlCommandRetryPolicy.ExecuteAction(
                        () =>
                        (from cartItems in database.Carts
                         where cartItems.CartId == cartId
                         select (int?)cartItems.Count * cartItems.Product.Price).Sum());
                return total ?? decimal.Zero;
            }
        }

        public int GetCount(string cartId)
        {
            Guard.CheckArgumentNullOrEmpty(cartId, "cartId");

            using (var database = TreyResearchModelFactory.CreateContext())
            {
                // Get the count of each item in the cart and sum them up
                int? count = this.sqlCommandRetryPolicy.ExecuteAction(
                        () =>
                        (from cartItems in database.Carts 
                         where cartItems.CartId == cartId 
                         select (int?)cartItems.Count).Sum());

                // Return 0 if all entries are null
                return count ?? 0;
            }
        }

        public void UpdateCart(string userName, string cartId)
        {
            Guard.CheckArgumentNullOrEmpty(userName, "userName");
            Guard.CheckArgumentNullOrEmpty(cartId, "cartId");

            using (var database = TreyResearchModelFactory.CreateContext())
            {
                var shoppingCart = this.sqlCommandRetryPolicy.ExecuteAction(() => database.Carts.Where(c => c.CartId == cartId));

                foreach (Entities.Cart item in shoppingCart)
                {
                    item.CartId = userName;
                }

                this.sqlCommandRetryPolicy.ExecuteAction(() => database.SaveChanges());
            }
        }
    }
}