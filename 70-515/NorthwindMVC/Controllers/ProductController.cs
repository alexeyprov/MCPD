using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Northwind.Data.Entities;
using NorthwindMVC.Helpers;
using NorthwindMVC.Models;

namespace NorthwindMVC.Controllers
{
	[HandleError(Order = 2)]
	public class ProductController : Controller
	{
		#region Private Fields

		private ProductModel _model;

		#endregion

		#region Constructor

		public ProductController()
		{
			_model = new ProductModel();
		}
		
		#endregion

		#region Actions

		//
		// GET: /Product/

		public ActionResult Index()
		{
			return View(_model.GetProducts());
		}

		//
		// GET: /Product/Details/5

		[HandleError(ExceptionType = typeof(Exceptions.NoDataFoundException), View = "NoDataFound")]
		public ActionResult Details(int id)
		{
			return View(_model.GetProductByID(id));
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

				_model.AddProduct(product);

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
			return View(_model.GetProductByID(id));
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

				Product product = _model.GetProductByID(id);
				UpdateModel(product);

				_model.UpdateProduct(product);

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
			return View(_model.GetProductByID(id));
		}

		//
		// POST: /Product/Delete/5

		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				Product product = _model.GetProductByID(id);
				_model.DeleteProduct(product);

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
				_model.GetCategories(),
				"CategoryID",
				"CategoryName");

			this.ViewData[ConstantsHelper.SUPPLIERS_VIEW_DATA_KEY] = new SelectList(
				_model.GetSuppliers(),
				"SupplierID",
				"CompanyName");
		}

		#endregion
	}
}
