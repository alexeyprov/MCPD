using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess
{
	public class Contact : IContact
	{
		#region Private Fields

		private string _ssn;
		private string _firstName;
		private string _middleName;
		private string _lastName;
		private string _email;
		private string _website;

		#endregion

		#region Construction

		private Contact(DataRow row)
		{
			_ssn = row["Ssn"].ToString();
			_firstName = row["FirstName"].ToString();
			_middleName = row["MiddleName"].ToString();
			_lastName = row["LastName"].ToString();
			_email = row["Email"].ToString();
			_website = row["Website"].ToString();
		}

		public static IList<IContact> Load(DataSet ds)
		{
			IList<IContact> retval = new List<IContact>();

			if (null == ds || 0 == ds.Tables.Count)
			{
				return retval;
			}

			foreach (DataRow row in ds.Tables[0].Rows)
			{
				retval.Add(new Contact(row));
			}
			return retval;
		}

		#endregion

		#region IContact Members

		public string Ssn
		{
			get
			{
				return _ssn;
			}
			set
			{
				_ssn = value;
			}
		}

		public string FirstName
		{
			get
			{
				return _firstName;
			}
			set
			{
				_firstName = value;
			}
		}

		public string MiddleName
		{
			get
			{
				return _middleName;
			}
			set
			{
				_middleName = value;
			}
		}

		public string LastName
		{
			get
			{
				return _lastName;
			}
			set
			{
				_lastName = value;
			}
		}

		public string Email
		{
			get
			{
				return _email;
			}
			set
			{
				_email = value;
			}
		}

		public string Website
		{
			get
			{
				return _website;
			}
			set
			{
				_website = value;
			}
		}

		#endregion
	}
}
