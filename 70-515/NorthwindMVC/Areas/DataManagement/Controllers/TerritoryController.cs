using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NorthwindMVC.Controllers;
using Northwind.Data.Entities;
using NorthwindMVC.Models;

namespace NorthwindMVC.Areas.DataManagement.Controllers
{
	public class TerritoryController : BasePagedController<Territory, PagedModel<Territory>>
	{
		public TerritoryController() :
			base(c => c.Territories)
		{
		}
	}
}
