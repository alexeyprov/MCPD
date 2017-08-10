using Northwind.Data.Entities;
using NorthwindMVC.Controllers;
using NorthwindMVC.Models;

namespace NorthwindMVC.Areas.Sales.Controllers
{
	public class OrderController : BasePagedController<Order, PagedModel<Order>>
	{
		public OrderController()
			: base(c => c.Orders)
		{
		}
	}
}
