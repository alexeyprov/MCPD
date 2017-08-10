using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind
{
	public class Employee
	{
		private Address _address = new Address();

		public int ID
		{
			get;
			set;
		}

		public string FirstName
		{
			get;
			set;
		}

		public string LastName
		{
			get;
			set;
		}

		public DateTime BirthDate
		{
			get;
			set;
		}

		public DateTime HireDate
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public Address Address
		{
			get
			{
				return _address;
			}
		}

		public string Notes
		{
			get;
			set;
		}
	}
}
