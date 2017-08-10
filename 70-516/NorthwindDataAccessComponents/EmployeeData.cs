using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Northwind
{
	[DataObject]
	public class EmployeeData : BaseDataAccessComponent
	{
		#region Private Constants

		private const string SP_EMPLOYEE_BY_ID_GET = "SP_EMPLOYEE_BY_ID_GET";
		private const string SP_EMPLOYEES_GET = "SP_EMPLOYEES_GET";
		private const string SP_EMPLOYEE_COUNT_GET = "SP_EMPLOYEE_COUNT_GET";
		private const string SP_EMPLOYEE_UPD = "SP_EMPLOYEE_UPD";
		private const string SP_EMPLOYEE_DELETE = "SP_EMPLOYEE_DELETE";

		#endregion

		#region Construction

		public EmployeeData(string connectionString)
			: base(connectionString)
		{
		}

		#endregion

		#region Public Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public List<Employee> GetAllEmployees(string sortExpression)
		{
			List<Employee> retval = GetAllEmployeesHelper();

			Sort(retval, sortExpression);
			return retval;
		}

		public List<Employee> GetEmployeesHiredAfter(DateTime hireDate)
		{
			// Use LINQ to Objects to filter employees based on hire date
			return new List<Employee>(
				from e in GetAllEmployeesHelper()
				where e.HireDate > hireDate
				select e);
		}

		/// <summary>
		/// Illustrates LINQ notions, such as:
		/// (*) anonymous types
		/// (*) implicitly typed variables
		/// (*) projections
		/// </summary>
		/// <returns></returns>
		public Dictionary<int, Address> GetEmployeesAddresses()
		{
			Dictionary<int, Address> addresses = new Dictionary<int, Address>();
			foreach (var record in
				from e in GetAllEmployeesHelper()
				where !String.IsNullOrEmpty(e.Address.StreetAddress)
				select new
				{
					e.ID,
					e.Address
				})
			{
				addresses.Add(record.ID, record.Address);
			}
			return addresses;
		}

		[DataObjectMethod(DataObjectMethodType.Select)]
		public int GetEmployeeCount()
		{
			using (DbCommand cmd = GetStoredProcCommand(SP_EMPLOYEE_COUNT_GET))
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
		public Employee GetEmployee(int id)
		{
			using (DbCommand cmd = GetStoredProcCommand(SP_EMPLOYEE_BY_ID_GET, id))
			{
				using (DbDataReader reader = ExecuteReader(cmd))
				{
					if (reader.Read())
					{
						return CreateEmployee(reader);
					}
				}
			}

			return null;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public void UpdateEmployee(Employee e)
		{
			using (DbCommand cmd = GetStoredProcCommand(SP_EMPLOYEE_UPD,
				e.ID,
				e.FirstName,
				e.LastName,
				e.Title,
				e.BirthDate,
				e.HireDate,
				e.Address.StreetAddress,
				e.Address.City,
				e.Address.Region,
				e.Address.PostalCode,
				e.Address.Country,
				e.Notes))
			{
				ExecuteNonQuery(cmd);
			}
		}

		[DataObjectMethod(DataObjectMethodType.Insert, true)]
		public void InsertEmployee(Employee e)
		{
			UpdateEmployee(e);
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public void DeleteEmployee(int id)
		{
			using (DbCommand cmd = GetStoredProcCommand(SP_EMPLOYEE_DELETE, id))
			{
				ExecuteNonQuery(cmd);
			}
		}

		#endregion

		#region Implementation

		private static Employee CreateEmployee(DbDataReader reader)
		{
			Employee e = new Employee();

			e.ID = reader.GetInt32(0);
			e.FirstName = reader[1].ToString();
			e.LastName = reader[2].ToString();
			e.Title = reader[3].ToString();

			e.BirthDate = reader.GetDateTime(4);
			e.HireDate = reader.GetDateTime(5);

			e.Address.StreetAddress = reader[6].ToString();
			e.Address.City = reader[7].ToString();
			e.Address.Region = reader[8].ToString();
			e.Address.PostalCode = reader[9].ToString();
			e.Address.Country = reader[10].ToString();

			e.Notes = reader[12].ToString();

			return e;
		}

		private List<Employee> GetAllEmployeesHelper()
		{
			List<Employee> retval = new List<Employee>();

			using (DbCommand cmd = GetStoredProcCommand(SP_EMPLOYEES_GET))
			{
				using (DbDataReader reader = ExecuteReader(cmd))
				{
					while (reader.Read())
					{
						retval.Add(CreateEmployee(reader));
					}
				}
			}

			return retval;
		}

		#endregion
	}
}
