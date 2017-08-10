using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind
{
	public class Supplier
	{
		private Address _address = new Address();
		private Contact _contact = new Contact();

		public Address Address
		{
			get
			{
				return _address;
			}
		}

		public Contact Contact
		{
			get
			{
				return _contact;
			}
		}

		public int ID
		{
			get;
			set;
		}

		public string CompanyName
		{
			get;
			set;
		}
	}
}
