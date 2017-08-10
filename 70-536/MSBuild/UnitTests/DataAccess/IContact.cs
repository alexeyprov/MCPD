using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
	public interface IContact
	{
		string Ssn
		{
			get;
			set;
		}

		string FirstName
		{
			get;
			set;
		}

		string MiddleName
		{
			get;
			set;
		}

		string LastName
		{
			get;
			set;
		}

		string Email
		{
			get;
			set;
		}

		string Website
		{
			get;
			set;
		}
	}
}
