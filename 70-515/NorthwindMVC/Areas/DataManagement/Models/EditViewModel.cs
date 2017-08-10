using System.Collections.Generic;

namespace NorthwindMVC.Areas.DataManagement.Models
{
	public class EditViewModel<T> : EditViewModelBase<T>
	{
		public IEnumerable<T> Entities
		{
			get;
			set;
		}
	}
}