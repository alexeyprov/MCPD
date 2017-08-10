//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace HeadOffice.DataStores
{
    using System.Collections.Generic;
    using System.Linq;
    using HeadOffice.Models;

    public class ProductStore : IProductStore
    {
        public IEnumerable<Product> FindAll()
        {           
            using (var database = new Entities.TreyResearchDataModelContainer())
            {
                return database.Product.ToList().Select(p => new Product
                {
                    ProductId = p.ProductId,
                    Description = p.Description,
                    Price = p.Price,
                    Active = p.Active
                }).ToList();
            }
        }

        public Product FindOne(int productId)
        {
            using (var database = new Entities.TreyResearchDataModelContainer())
            {
                return database.Product.Where(p => p.ProductId == productId).Select(p => new Product
                { 
                    ProductId = p.ProductId,
                    Price = p.Price,
                    Description = p.Description,
                    Active = p.Active
                }).FirstOrDefault();
            }
        }

        public int Add(Product product)
        {
            Guard.CheckArgumentNull(product, "product");

            using (var database = new Entities.TreyResearchDataModelContainer())
            {
                var productToSave = new Entities.Product 
                {
                    Description = product.Description,
                    Price = product.Price,
                    Active = product.Active
                };

                database.Product.AddObject(productToSave);
                database.SaveChanges();

                return product.ProductId;
            }
        }

        public void Delete(int id)
        {
            using (var database = new Entities.TreyResearchDataModelContainer())
            {                
                var product = database.Product.FirstOrDefault(p => p.ProductId == id);
                if (product != null)
                {
                    product.Active = false;
                }
                database.SaveChanges();
            }
        }

        public void Update(Product product)
        {
            Guard.CheckArgumentNull(product, "product");

            using (var database = new Entities.TreyResearchDataModelContainer())
            {
                var productEntity = database.Product.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (productEntity != null)
                {
                    productEntity.Description = product.Description;
                    productEntity.Active = product.Active;
                    database.SaveChanges();
                }
            }
        }
    }
}