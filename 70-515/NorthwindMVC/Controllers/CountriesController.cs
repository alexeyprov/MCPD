using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindMVC.Models;

namespace NorthwindMVC.Controllers
{
	public class CountriesController : Controller
	{
		private static readonly Country[] DEFAULT_COUNTRIES = { 
																  new Country()
																  {
																	  Name = "Russia",
																	  Detail = new CountryDetail()
																	  {
																		  Capital = "Moscow",
																		  Continent = "Europe"
																	  }
																  },
																  new Country()
																  {
																	  Name = "USA",
																	  Detail = new CountryDetail()
																	  {
																		  Capital = "Washington",
																		  Continent = "America"
																	  }
																  },
																  new Country()
																  {
																	  Name = "Italy",
																	  Detail = new CountryDetail()
																	  {
																		  Capital = "Rome",
																		  Continent = "Europe"
																	  }
																  },
															  };
		//
		// GET: /Countries/

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index()
		{
			return View(DEFAULT_COUNTRIES);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Index(IList<Country> countries)
		{
			return View(countries);
		}
	}
}
