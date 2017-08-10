using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace Northwind
{
	public class OrderLine
	{
		public int OrderID
		{
			get;
			set;
		}

		public int ProductID
		{
			get;
			set;
		}

		public decimal UnitPrice
		{
			get;
			set;
		}

		public short Quantity
		{
			get;
			set;
		}

		public float Discount
		{
			get;
			set;
		}
	}
}
