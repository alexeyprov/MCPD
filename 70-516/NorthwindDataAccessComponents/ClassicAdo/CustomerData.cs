using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using ReflectionUtilities;

namespace Northwind.Data.ClassicAdo
{
	[DataObject]
	public class CustomerData : BaseDataAccessComponent
	{
		#region Private Constants

		private const string SP_CUSTOMER_BY_ID_GET = "SP_CUSTOMER_BY_ID_GET";
		private const string SP_CUSTOMERS_GET = "SP_CUSTOMERS_GET";
		private const string SP_CUSTOMER_COUNT_GET = "SP_CUSTOMER_COUNT_GET";
		private const string SP_CUSTOMER_UPD = "SP_CUSTOMER_UPD";
		private const string SP_CUSTOMER_DELETE = "SP_CUSTOMER_DELETE";

		#endregion

		#region Construction

		public CustomerData(string connectionString)
			: base(connectionString)
		{
		}

		#endregion

		#region Public Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public List<Customer> GetAllCustomers(string sortExpression)
		{
			List<Customer> retval = new List<Customer>();

			using (DbCommand cmd = GetStoredProcCommand(SP_CUSTOMERS_GET))
			{
				using (DbDataReader reader = ExecuteReader(cmd))
				{
					while (reader.Read())
					{
						retval.Add(CreateCustomer(reader));
					}
				}
			}

			Sort(retval, sortExpression);
			return retval;
		}

		[DataObjectMethod(DataObjectMethodType.Select)]
		public int GetCustomerCount()
		{
			using (DbCommand cmd = GetStoredProcCommand(SP_CUSTOMER_COUNT_GET))
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
		public Customer GetCustomer(string id)
		{
			using (DbCommand cmd = GetStoredProcCommand(SP_CUSTOMER_BY_ID_GET, id))
			{
				using (DbDataReader reader = ExecuteReader(cmd))
				{
					if (reader.Read())
					{
						return CreateCustomer(reader);
					}
				}
			}

			return null;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public void UpdateCustomer(Customer c)
		{
			using (DbCommand cmd = GetStoredProcCommand(SP_CUSTOMER_UPD,
				c.ID,
				c.CompanyName,
				c.Contact.Name,
				c.Contact.Title,
				c.Address.StreetAddress,
				c.Address.City,
				c.Address.Region,
				c.Address.PostalCode,
				c.Address.Country,
				c.Contact.Phone,
				c.Contact.Fax))
			{
				ExecuteNonQuery(cmd);
			}
		}

		[DataObjectMethod(DataObjectMethodType.Insert, true)]
		public void InsertCustomer(Customer c)
		{
			UpdateCustomer(c);
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public void DeleteCustomer(Customer c)
		{
			using (DbCommand cmd = GetStoredProcCommand(SP_CUSTOMER_DELETE, c.ID))
			{
				ExecuteNonQuery(cmd);
			}
		}

		#endregion

		#region Implementation

		private static Customer CreateCustomer(DbDataReader reader)
		{
			Customer c = new Customer();

			c.ID = reader[0].ToString();
			c.CompanyName = reader[1].ToString();

			c.Contact.Name = reader[2].ToString();
			c.Contact.Title = reader[3].ToString();

			c.Address.StreetAddress = reader[4].ToString();
			c.Address.City = reader[5].ToString();
			c.Address.Region = reader[6].ToString();
			c.Address.PostalCode = reader[7].ToString();
			c.Address.Country = reader[8].ToString();

			c.Contact.Phone = reader[9].ToString();
			c.Contact.Fax = reader[10].ToString();

			return c;
		}

		#endregion
	}
}
