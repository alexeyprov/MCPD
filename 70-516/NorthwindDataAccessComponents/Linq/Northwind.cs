using System;
using System.Text.RegularExpressions;

namespace Northwind.Data.Linq
{
	public class NorthwindDataValidationException : ApplicationException
	{
	}

	partial class NorthwindDataContext
	{
	}

	partial class Customer
	{
		private static readonly Regex PHONE_FAX_REGEX = new Regex(@"^\(\d{3}\)\s+\d{3}[-\s]+\d{2}[-\s]?\d{2}$");

		partial void OnFaxChanging(string value)
		{
			ValidatePhoneOrFax(value);
		}

		partial void OnPhoneChanging(string value)
		{
			ValidatePhoneOrFax(value);
		}

		private void ValidatePhoneOrFax(string value)
		{
			if (!PHONE_FAX_REGEX.IsMatch(value))
			{
				throw new NorthwindDataValidationException();
			}
		}
	}
}
