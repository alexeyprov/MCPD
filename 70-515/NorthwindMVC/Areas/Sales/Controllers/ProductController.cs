using System.Web.Mvc;

using Northwind.Data.Entities;
using NorthwindMVC.Areas.Sales.Models;
using NorthwindMVC.Attributes;
using NorthwindMVC.Controllers;
using NorthwindMVC.Helpers;

namespace NorthwindMVC.Areas.Sales.Controllers
{
	[HandleError(Order = 2)]
	public class ProductController : 
		BaseFlatController<Product, ProductModel>
	{
		#region Constructor

		public ProductController() :
			base(c => c.Products)
		{
		}
		
		#endregion

		#region Actions

		//
		// GET: /Product/

		[ReportDuration]
		public override ActionResult Index()
		{
			return base.Index();
		}

		//
		// GET: /Product/Details/5

		[HandleError(ExceptionType = typeof(Exceptions.NoDataFoundException), View = "NoDataFound")]
		public ActionResult Details(int id)
		{
			return View(Model.GetProductByID(id));
		}

		//
		// GET: /Product/Create

		public ActionResult Create()
		{
			SetupLookups();

			return View(new Product());
		}

		//
		// POST: /Product/Create

		[HttpPost]
		public ActionResult Create(Product product)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					SetupLookups();
					return View("Create", product);
				}

				Model.AddProduct(product);

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		//
		// GET: /Product/Edit/5

		[HandleError(ExceptionType = typeof(Exceptions.NoDataFoundException), View = "NoDataFound")]
		public ActionResult Edit(int id)
		{
			SetupLookups();
			return View(Model.GetProductByID(id));
		}

		//
		// POST: /Product/Edit/5

		[HttpPost]
		public ActionResult Edit(int id, Product p)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					SetupLookups();
					return View("Edit", p);
				}

				Product product = Model.GetProductByID(id);
				UpdateModel(product);

				Model.UpdateProduct(product);

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		//
		// GET: /Product/Delete/5

		[HandleError(ExceptionType = typeof(Exceptions.NoDataFoundException), View = "NoDataFound")]
		public ActionResult Delete(int id)
		{
			return View(Model.GetProductByID(id));
		}

		//
		// POST: /Product/Delete/5

		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				Product product = Model.GetProductByID(id);
				Model.DeleteProduct(product);

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
		
		#endregion

		#region Implementation

		private void SetupLookups()
		{
			this.ViewData[ConstantsHelper.CATEGORIES_VIEW_DATA_KEY] = new SelectList(
				Model.GetCategories(),
				"CategoryID",
				"CategoryName");

			this.ViewData[ConstantsHelper.SUPPLIERS_VIEW_DATA_KEY] = new SelectList(
				Model.GetSuppliers(),
				"SupplierID",
				"CompanyName");
		}

		#endregion
	}
}
