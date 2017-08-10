using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Northwind.Data.Entities;
using NorthwindMVC.Models;

namespace NorthwindMVC.Controllers
{
	public abstract class BaseFlatController<TEntity, TModel> : BaseController<TEntity, TModel>
		where TEntity : class
		where TModel : BaseModel<TEntity>, new()
	{
		protected BaseFlatController(Func<NorthwindObjectContext, IEnumerable<TEntity>> entitySetFactory) :
			base(entitySetFactory)
		{
		}

		//
		// GET: /T/

		public virtual ActionResult Index()
		{
			return View(Model.GetData());
		}
	}
}