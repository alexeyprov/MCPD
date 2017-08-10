using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using Northwind.Data.Entities;
using NorthwindMVC.Models;

namespace NorthwindMVC.Areas.Sales.Models
{
	public class CustomerModel : BaseModel<Customer>
	{
		public CustomerModel()
		{
			Initialize(c => c.Customers);
		}

		internal IEnumerable<Customer> GetCustomers()
		{
			return base.GetData();
		}

		internal IEnumerable<string> GetCountries()
		{
			Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
			Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(),
				s => !String.IsNullOrEmpty(s)));

			return (from c in base.Context.Customers
					where !String.IsNullOrEmpty(c.Country)
					select c.Country).Distinct();
		}
	}
}