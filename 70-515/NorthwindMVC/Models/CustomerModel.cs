using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using Northwind.Data.Entities;

namespace NorthwindMVC.Models
{
	public class CustomerModel : BaseModel
	{
		internal IEnumerable<Customer> GetCustomers()
		{
			Contract.Ensures(Contract.Result<IEnumerable<Customer>>() != null);
			Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<Customer>>(),
				c => c != null));

			return base.Context.Customers;
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