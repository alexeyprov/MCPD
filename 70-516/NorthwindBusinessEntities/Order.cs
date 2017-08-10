using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind
{
	public class Order
	{
		private Address _shippingAddress = new Address();

		public Address ShippingAddress
		{
			get
			{
				return _shippingAddress;
			}
		}

		public int ID
		{
			get;
			set;
		}

		public int EmployeeID
		{
			get;
			set;
		}

		public string CustomerID
		{
			get;
			set;
		}

		public DateTime OrderedDate
		{
			get;
			set;
		}

		public DateTime RequiredDate
		{
			get;
			set;
		}

		public DateTime ShippedDate
		{
			get;
			set;
		}

		public decimal Freight
		{
			get;
			set;
		}

		public int ShipperID
		{
			get;
			set;
		}

		public OrderLine[] Lines
		{
			get;
			set;
		}
	}
}
