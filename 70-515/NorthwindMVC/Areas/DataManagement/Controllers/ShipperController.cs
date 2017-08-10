using System.Web.Mvc;

using Northwind.Data.Entities;
using NorthwindMVC.Areas.DataManagement.Models;
using NorthwindMVC.Controllers;

namespace NorthwindMVC.Areas.DataManagement.Controllers
{
	public class ShipperController : BaseFlatController<Shipper, ShipperModel>
	{
		public ShipperController()
			: base(c => c.Shippers)
		{
		}

		public ActionResult Edit([Bind(Prefix = "Shippers")] int shipperID)
		{
			return PartialView(
				"_EditShipper",
				new EditViewModelBase<Shipper>()
				{
					SelectedEntity = base.Model.GetShipper(shipperID)
				});
		}

		public ActionResult Update(Shipper shipper)
		{
			string status;

			if (ValidateShipper(shipper))
			{
				Shipper originalShipper = base.Model.GetShipper(shipper.ShipperID);

				originalShipper.CompanyName = shipper.CompanyName;
				originalShipper.Phone = shipper.Phone;

				base.Model.Save();
				status = "Saved succesfully.";

				if (Request.IsAjaxRequest())
				{
					Response.AddHeader(
						"Content-Title",
						string.Format("{0} updated successfully.", shipper.CompanyName));
				}
			}
			else
			{
				status = "Validation error.";
			}

			return PartialView(
				"_EditShipper",
				new EditViewModelBase<Shipper>()
					{
						SelectedEntity = shipper,
						UpdateStatus = status
					});
		}

		private bool ValidateShipper(Shipper shipper)
		{
			bool isValid = true;

			if (string.IsNullOrEmpty(shipper.CompanyName))
			{
				isValid = false;
				base.ViewData.ModelState.AddModelError("CompanyName", "Company name is required.");
			}

			if (string.IsNullOrEmpty(shipper.Phone))
			{
				isValid = false;
				base.ViewData.ModelState.AddModelError("Phone", "Phone number is required.");
			}

			return isValid;
		}
	}
}
