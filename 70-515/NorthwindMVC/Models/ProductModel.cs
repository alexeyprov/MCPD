using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using Northwind.Data.Entities;

using NorthwindMVC.Exceptions;

namespace NorthwindMVC.Models
{
	public class ProductModel : BaseModel
	{
		public Product GetProductByID(int id)
		{
			var products = from product in base.Context.Products.Include("Category").Include("Supplier")
						   where product.ProductID == id
						   select product;

			try
			{
				return products.Single();
			}
			catch (InvalidOperationException ex)
			{
				throw new NoDataFoundException("No product found", ex);
			}
		}

		public IEnumerable<Category> GetCategories()
		{
			Contract.Ensures(Contract.Result<IEnumerable<Category>>() != null);
			Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<Category>>(),
				c => c != null));

			return base.Context.Categories;
		}

		public IEnumerable<Supplier> GetSuppliers()
		{
			Contract.Ensures(Contract.Result<IEnumerable<Supplier>>() != null);
			Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<Supplier>>(),
				s => s != null));

			return base.Context.Suppliers;
		}

		public IEnumerable<Product> GetProducts()
		{
			Contract.Ensures(Contract.Result<IEnumerable<Product>>() != null);
			Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<Product>>(),
				p => p != null));

			return base.Context.Products;
		}

		public void AddProduct(Product product)
		{
			Contract.Requires(product != null);

			UpdateReferencesForProduct(product);

			base.Context.Products.AddObject(product);
			base.Context.SaveChanges();
		}

		public void UpdateProduct(Product product)
		{
			Contract.Requires(product != null);

			UpdateReferencesForProduct(product);
			base.Context.SaveChanges();
		}

		public void DeleteProduct(Product product)
		{
			Contract.Requires(product != null);
			Contract.Requires(product.ProductID > 0);

			foreach (OrderLine line in product.OrderLines)
			{
				base.Context.DeleteObject(line);
			}

			base.Context.DeleteObject(product);
			base.Context.SaveChanges();
		}

		private void UpdateReferencesForProduct(Product product)
		{
			Contract.Requires(product != null);

			if (null == product.Category && 0 != product.CategoryID)
			{
				product.Category = base.Context.Categories.Where(c => c.CategoryID == product.CategoryID).SingleOrDefault();
			}

			if (null == product.Supplier && 0 != product.SupplierID)
			{
				product.Supplier = base.Context.Suppliers.Where(s => s.SupplierID == product.SupplierID).SingleOrDefault();
			}
		}
	}
}