using System.Collections.Generic;

namespace NorthwindMVC.Models
{
	public class PagedViewModel<T>
		where T : class
	{
		public int PageIndex
		{
			get;
			set;
		}

		public int PageCount
		{
			get;
			set;
		}

		public IEnumerable<T> Items
		{
			get;
			set;
		}
	}
}