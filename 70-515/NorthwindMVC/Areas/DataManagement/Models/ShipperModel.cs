using System;
using System.Collections.Generic;
using System.Linq;

using Northwind.Data.Entities;
using NorthwindMVC.Exceptions;
using NorthwindMVC.Models;

namespace NorthwindMVC.Areas.DataManagement.Models
{
	public class ShipperModel : BaseModel<Shipper>
	{
		public Shipper GetShipper(int id)
		{
			IEnumerable<Shipper> shippers = GetData().Where(s => s.ShipperID == id);

			try
			{
				return shippers.Single();
			}
			catch (InvalidOperationException ex)
			{
				throw new NoDataFoundException("No shipper found", ex);
			}
		}
	}
}