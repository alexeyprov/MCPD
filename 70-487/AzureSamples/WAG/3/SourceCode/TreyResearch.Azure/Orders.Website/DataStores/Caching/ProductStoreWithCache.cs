//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.DataStores.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Models;

    public class ProductStoreWithCache : IProductStore
    {
        private readonly IProductStore productStore;

        private readonly ICachingStrategy cachingStrategy;

        public ProductStoreWithCache(IProductStore productStore, ICachingStrategy cachingStrategy)
        {
            this.productStore = productStore;
            this.cachingStrategy = cachingStrategy;
        }

        public IEnumerable<Product> FindAll()
        {
            //// The products retrieved by FindAll are cached by storing in the cache a key that identifies this operation.
            //// The timeout value (the time before objects expire from the cache) depends heavily on the solution implemented, and a
            //// thorough analysis should be performed in order to find the correct value for this setting. In this sample we used a value 
            //// of 10 minutes since the products in this application are rarely updated.
            
            return (IEnumerable<Product>)this.cachingStrategy.Get(
                "ProductStore/FindAll",
                () => this.productStore.FindAll(),
                TimeSpan.FromMinutes(10));
        }

        public Product FindOne(int productId)
        {
            //// The products retrieved by the FindOne operation are cached by storing in the cache a key that identifies 
            //// the operation and the product 'ProductStore/Product/<ProductId>'

            return (Product)this.cachingStrategy.Get(
                string.Format(CultureInfo.InvariantCulture, "ProductStore/Product/{0}", productId),
                () => this.productStore.FindOne(productId),
                TimeSpan.FromMinutes(10));
        }
    }
}