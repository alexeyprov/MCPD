using System.Web.Mvc;

using Northwind.Data.Entities;
using NorthwindMVC.Areas.DataManagement.Models;
using NorthwindMVC.Controllers;

namespace NorthwindMVC.Areas.DataManagement.Controllers
{
	public class SupplierController : BaseFlatController<Supplier, SupplierModel>
	{
		private const string UPDATE_STATUS_KEY = "Supplier.UpdateStatus";
		private const string MODEL_DATA_KEY = "Supplier.ModelData";

		public SupplierController() : base(c => c.Suppliers)
		{
		}

		[ActionName("Edit")]
		[HttpPost]
		public ActionResult EditForPost([Bind(Prefix = "suppliers")] int supplierID)
		{
			return RedirectToAction("Edit", new { id = supplierID });
		}

		[ActionName("Edit")]
		[HttpGet]
		public ActionResult EditForGet(int id)
		{
			ModelStateDictionary updateState = TempData[MODEL_DATA_KEY] as ModelStateDictionary;

			if (updateState != null)
			{
				ViewData.ModelState.Merge(updateState);
			}

			return View(
				new EditViewModel<Supplier>()
				{
					SelectedEntity = base.Model.GetSupplier(id),
					Entities = base.Model.GetData(),
					UpdateStatus = TempData[UPDATE_STATUS_KEY] as string
				});
		}

		public ActionResult Update(Supplier supplier)
		{
			string status;

			if (ValidateSupplier(supplier))
			{
				Supplier originalSupplier = base.Model.GetSupplier(supplier.SupplierID);

				originalSupplier.CompanyName = supplier.CompanyName;
				originalSupplier.ContactName = supplier.ContactName;
				originalSupplier.Country = supplier.Country;
				originalSupplier.Phone = supplier.Phone;

				base.Model.Save();
				status = "Saved succesfully.";
			}
			else
			{
				status = "Validation error.";
			}

			TempData[UPDATE_STATUS_KEY] = status;

			return RedirectToAction(
				"Edit", 
				new 
				{
					id = supplier.SupplierID
				});
		}

		private bool ValidateSupplier(Supplier supplier)
		{
			bool isValid = true;

			if (string.IsNullOrEmpty(supplier.CompanyName))
			{
				isValid = false;
				base.ViewData.ModelState.AddModelError("CompanyName", "Company name is required.");
			}

			if (string.IsNullOrEmpty(supplier.ContactName))
			{
				isValid = false;
				base.ViewData.ModelState.AddModelError("ContactName", "Contact name is required.");
			}

			if (string.IsNullOrEmpty(supplier.Country))
			{
				isValid = false;
				base.ViewData.ModelState.AddModelError("Country", "Country is required.");
			}

			if (string.IsNullOrEmpty(supplier.Phone))
			{
				isValid = false;
				base.ViewData.ModelState.AddModelError("Phone", "Phone number is required.");
			}

			TempData[MODEL_DATA_KEY] = base.ViewData.ModelState;

			return isValid;
		}
	}
}
