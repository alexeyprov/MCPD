using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Configuration;

using Northwind.Data.Entities;

using NorthwindMVC.Helpers;

namespace NorthwindMVC.Models
{
	public class BaseModel<T>
		where T : class
	{
		private Func<NorthwindObjectContext, IEnumerable<T>> _entitySetFactory;

		protected BaseModel()
		{
			string connString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_ENTITIES_CONNECTION_STRING].ConnectionString;
			Context = new NorthwindObjectContext(connString);
		}

		internal void Initialize(Func<NorthwindObjectContext, IEnumerable<T>> entitySetFactory)
		{
			Contract.Requires(entitySetFactory != null);

			_entitySetFactory = entitySetFactory;
		}

		internal IEnumerable<T> GetData()
		{
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
			Contract.Ensures(
				Contract.ForAll(
					Contract.Result<IEnumerable<T>>(),
					d => d != null));

			return this.EntitySetFactory(this.Context);
		}

		internal void Save()
		{
			this.Context.SaveChanges();
		}

		protected NorthwindObjectContext Context
		{
			get;
			private set;
		}

		protected Func<NorthwindObjectContext, IEnumerable<T>> EntitySetFactory
		{
			get
			{
				Contract.Ensures(Contract.Result<Func<NorthwindObjectContext, IEnumerable<T>>>() != null);

				if (null == _entitySetFactory)
				{
					throw new InvalidOperationException("Initialize should be called first");
				}

				return _entitySetFactory;
			}
		}
	}
}