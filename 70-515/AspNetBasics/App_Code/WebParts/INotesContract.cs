using System;

namespace WebParts
{
	/// <summary>
	/// Summary description for INotesContract
	/// </summary>
	public interface INotesContract
	{
		string Notes
		{
			get;
			set;
		}

		DateTime CreationDate
		{
			get;
		}
	} 
}