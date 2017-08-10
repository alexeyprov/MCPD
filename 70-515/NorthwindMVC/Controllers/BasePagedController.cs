using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Northwind.Data.Entities;
using NorthwindMVC.Models;

namespace NorthwindMVC.Controllers
{
	public abstract class BasePagedController<TEntity, TModel> : 
		BaseController<TEntity, TModel>
		where TEntity : class
		where TModel : PagedModel<TEntity>, new()
	{
		private const int PAGE_SIZE = 10;

		protected BasePagedController(Func<NorthwindObjectContext, IEnumerable<TEntity>> entitySetFactory) :
			base(entitySetFactory)
		{
		}

		//
		// GET: /T/

		public ActionResult Index(int? pageIndex)
		{
			int index = Math.Max(0, pageIndex ?? 0);

			int totalItems;
			IEnumerable<TEntity> currentPage = Model.GetData(index, PAGE_SIZE, out totalItems);

			return View(new PagedViewModel<TEntity>()
							{
								Items = currentPage,
								PageIndex = index,
								PageCount = totalItems / PAGE_SIZE
							});
		}
	}
}