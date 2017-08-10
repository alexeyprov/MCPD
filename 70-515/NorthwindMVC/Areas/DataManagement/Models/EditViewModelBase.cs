namespace NorthwindMVC.Areas.DataManagement.Models
{
	public class EditViewModelBase<T>
	{
		public T SelectedEntity
		{
			get;
			set;
		}

		public string UpdateStatus
		{
			get;
			set;
		}
	}
}