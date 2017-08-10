using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Northwind.Data.ClassicAdo
{
	public class OrderData : BaseDataAccessComponent
	{
		#region Private Constants

		private const string SP_ORDER_BY_ID_GET = "SP_ORDER_BY_ID_GET";
		private const string SP_ORDERS_GET = "SP_ORDERS_GET";
		private const string SP_ORDER_COUNT_GET = "SP_ORDER_COUNT_GET";
		private const string SP_ORDER_UPD = "SP_ORDER_UPD";
		private const string SP_ORDER_DELETE = "SP_ORDER_DELETE";
		private const string SP_LINES_BY_ODER_GET = "SP_LINES_BY_ORDER_GET";

		#endregion

		#region Construction

		public OrderData(string connectionString)
			: base(connectionString)
		{
		}

		#endregion

		#region Public Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public List<Order> GetAllOrders(string sortExpression)
		{
			List<Order> retval = new List<Order>();

			using (DbCommand cmd = GetStoredProcCommand(SP_ORDERS_GET))
			{
				using (DbDataReader reader = ExecuteReader(cmd))
				{
					while (reader.Read())
					{
						retval.Add(CreateOrder(reader, false));
					}
				}
			}

			Sort(retval, sortExpression);
			return retval;
		}

		[DataObjectMethod(DataObjectMethodType.Select)]
		public int GetOrderCount()
		{
			using (DbCommand cmd = GetStoredProcCommand(SP_ORDER_COUNT_GET))
			{
				using (DbDataReader reader = ExecuteReader(cmd))
				{
					if (reader.Read())
					{
						return (int) reader[0];
					}
				}
			}

			return 0;
		}


		[DataObjectMethod(DataObjectMethodType.Select)]
		public Order GetOrder(string id)
		{
			using (DbCommand cmd = GetStoredProcCommand(SP_ORDER_BY_ID_GET, id))
			{
				using (DbDataReader reader = ExecuteReader(cmd))
				{
					if (reader.Read())
					{
						return CreateOrder(reader, true);
					}
				}
			}

			return null;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public void UpdateOrder(Order o)
		{
			using (DbCommand cmd = GetStoredProcCommand(SP_ORDER_UPD,
				o.ID,
				o.CustomerID,
				o.EmployeeID,
				o.ShipperID,
				o.ShippingAddress.StreetAddress,
				o.ShippingAddress.City,
				o.ShippingAddress.Region,
				o.ShippingAddress.PostalCode,
				o.ShippingAddress.Country,
				o.OrderedDate,
				o.RequiredDate,
				o.ShippedDate))
			{
				ExecuteNonQuery(cmd);
			}
		}

		[DataObjectMethod(DataObjectMethodType.Insert, true)]
		public void InsertOrder(Order o)
		{
			UpdateOrder(o);
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public void DeleteOrder(string id)
		{
			using (DbCommand cmd = GetStoredProcCommand(SP_ORDER_DELETE, id))
			{
				ExecuteNonQuery(cmd);
			}
		}

		#endregion

		#region Implementation

		private Order CreateOrder(DbDataReader reader, bool readLines)
		{
			Order o = new Order();

			o.ID = reader.GetInt32(0);
			o.CustomerID = reader[1].ToString();
			o.EmployeeID = reader.GetInt32(2);

			if (!reader.IsDBNull(3))
			{
				o.OrderedDate = reader.GetDateTime(3);
			}
			if (!reader.IsDBNull(4))
			{
				o.RequiredDate = reader.GetDateTime(4);
			}
			if (!reader.IsDBNull(5))
			{
				o.ShippedDate = reader.GetDateTime(5);
			}

			o.Freight = reader.GetDecimal(6);

			o.ShipperID = reader.GetInt32(7);

			o.ShippingAddress.StreetAddress = reader[8].ToString();
			o.ShippingAddress.City = reader[9].ToString();
			o.ShippingAddress.Region = reader[10].ToString();
			o.ShippingAddress.PostalCode = reader[11].ToString();
			o.ShippingAddress.Country = reader[12].ToString();

			if (readLines)
			{
				List<OrderLine> lines = new List<OrderLine>();

				using (DbCommand cmd = GetStoredProcCommand(SP_LINES_BY_ODER_GET, o.ID))
				{
					using (DbDataReader linesReader = cmd.ExecuteReader())
					{
						while (linesReader.Read())
						{
							OrderLine line = new OrderLine();

							line.OrderID = reader.GetInt32(0);
							line.ProductID = reader.GetInt32(1);

							line.Discount = reader.GetFloat(2);
							line.Quantity = reader.GetInt16(3);
							line.UnitPrice = reader.GetDecimal(4);

							lines.Add(line);
						}
					}
				}

				o.Lines = lines.ToArray();
			}

			return o;
		}

		#endregion

	}
}
