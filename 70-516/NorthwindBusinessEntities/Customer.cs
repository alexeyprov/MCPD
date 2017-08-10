using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Northwind
{
	public class Customer
	{
		private Address _address = new Address();
		private Contact _contact = new Contact();

		public string ID
		{
			get;
			set;
		}

        [StringLength(200, MinimumLength=2)]
		public string CompanyName
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

		public Contact Contact
		{
			get
			{
				return _contact;
			}
		}
	}
}
