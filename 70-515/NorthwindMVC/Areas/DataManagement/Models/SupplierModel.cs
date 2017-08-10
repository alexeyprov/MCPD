using System;
using System.Collections.Generic;
using System.Linq;

using Northwind.Data.Entities;
using NorthwindMVC.Exceptions;
using NorthwindMVC.Models;

namespace NorthwindMVC.Areas.DataManagement.Models
{
	public class SupplierModel : BaseModel<Supplier>
	{
		public Supplier GetSupplier(int id)
		{
			IEnumerable<Supplier> suppliers = GetData().Where(s => s.SupplierID == id);

			try
			{
				return suppliers.Single();
			}
			catch (InvalidOperationException ex)
			{
				throw new NoDataFoundException("No supplier found", ex);
			}
		}
	}
}