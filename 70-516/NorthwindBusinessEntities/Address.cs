using System.ComponentModel.DataAnnotations;

namespace Northwind
{
	public class Address
	{
        [StringLength(50, MinimumLength=2)]
		public string City
		{
			get;
			set;
		}

		public string Region
		{
			get;
			set;
		}

        [Required]
        [RegularExpression(@"[\d\-]+")]
		public string PostalCode
		{
			get;
			set;
		}

        [Required]
		public string Country
		{
			get;
			set;
		}

        [StringLength(200, MinimumLength=5)]
		public string StreetAddress
		{
			get;
			set;
		}
	}
}
