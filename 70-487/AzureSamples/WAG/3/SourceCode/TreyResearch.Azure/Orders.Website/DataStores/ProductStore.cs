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
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.Practices.TransientFaultHandling;
    using Orders.Shared.Helpers;
    using Orders.Website.Models;

    public class ProductStore : IProductStore
    {
        private readonly RetryPolicy sqlCommandRetryPolicy;

        public ProductStore()
        {
            // this policy is defined in the configuration file
            this.sqlCommandRetryPolicy = RetryPolicyFactory.GetDefaultSqlCommandRetryPolicy();
            this.sqlCommandRetryPolicy.Retrying += (sender, args) => TraceHelper.TraceInformation("Retry in ProductStore - Count:{0}, Delay:{1}, Exception:{2}", args.CurrentRetryCount, args.Delay, args.LastException);
        }

        public IEnumerable<Product> FindAll()
        {
            using (var database = TreyResearchModelFactory.CreateContext())
            {   
                return this.sqlCommandRetryPolicy.ExecuteAction<IEnumerable<Product>>(() =>
                        database.Products.Where(p => p.Active).ToList().Select(
                            p => new Product
                                {
                                    ProductId = p.ProductId, 
                                    Description = p.Description, 
                                    Price = p.Price
                                }).ToList());
            }
        }

        public Product FindOne(int productId)
        {
            using (var database = TreyResearchModelFactory.CreateContext())
            {
                return this.sqlCommandRetryPolicy.ExecuteAction(() =>
                    database.Products.Where(p => p.ProductId == productId).Select(
                        p => new Product
                            {
                                ProductId = p.ProductId, 
                                Price = p.Price, 
                                Description = p.Description
                            }).FirstOrDefault());
            }
        }
    }
}