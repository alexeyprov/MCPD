using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NorthwindMVC.Helpers;
using NorthwindMVC.Models;

namespace NorthwindMVC.Controllers
{
	public class CustomerController : Controller
	{
		private CustomerModel _model;

		public CustomerController()
		{
			_model = new CustomerModel();
		}

		//
		// GET: /Customer/

		public ActionResult Index()
		{
			ViewData[ConstantsHelper.CAPTION_VIEW_DATA_KEY] = Properties.Resources.CustomersCaption;
			ViewData[ConstantsHelper.COUNTRIES_VIEW_DATA_KEY] = _model.GetCountries();

			return View(_model.GetCustomers());
		}

		//
		// GET: /Customer/Details/5

		public ActionResult Details(int id)
		{
			return View();
		}

		//
		// GET: /Customer/Create

		public ActionResult Create()
		{
			return View();
		}

		//
		// POST: /Customer/Create

		[HttpPost]
		public ActionResult Create(FormCollection collection)
		{
			try
			{
				// TODO: Add insert logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		//
		// GET: /Customer/Edit/5

		public ActionResult Edit(int id)
		{
			return View();
		}

		//
		// POST: /Customer/Edit/5

		[HttpPost]
		public ActionResult Edit(string id, FormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		//
		// GET: /Customer/Delete/5

		public ActionResult Delete(string id)
		{
			return View();
		}

		//
		// POST: /Customer/Delete/5

		[HttpPost]
		public ActionResult Delete(string id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
