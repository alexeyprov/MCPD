using System;
using System.Web.Mvc;

using PartyInvites.Helpers;
using PartyInvites.Models;

namespace PartyInvites.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Home/

		public ViewResult Index()
		{
			ViewData[ConstantsHelper.ViewItems.GREETING] = DateTime.Now.Hour < 12 ?
				"Good morning" :
				"Good afternoon";

			return View();
		}

		[HttpGet]
		public ActionResult RsvpForm()
		{
			return View();
		}

		[HttpPost]
		public ActionResult RsvpForm(GuestResponse response)
		{
			if (ModelState.IsValid)
			{
				response.Submit();
				return View("ThankYou", response);
			}

			return View();
		}
	}
}
