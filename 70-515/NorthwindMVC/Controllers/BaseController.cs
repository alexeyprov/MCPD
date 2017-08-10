using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

using Northwind.Data.Entities;
using NorthwindMVC.Models;

namespace NorthwindMVC.Controllers
{
	public abstract class BaseController<TEntity, TModel> : Controller
		where TEntity : class
		where TModel : BaseModel<TEntity>, new()
	{
		private readonly TModel _model;

		protected BaseController(Func<NorthwindObjectContext, IEnumerable<TEntity>> entitySetFactory)
		{
			_model = new TModel();
			_model.Initialize(entitySetFactory);
		}

		//
		// GET: /T/
		protected TModel Model
		{
			get
			{
				Contract.Ensures(Contract.Result<TModel>() != null);

				return _model;
			}
		}
	}
}