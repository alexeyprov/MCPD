using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace PartyInvites.Models
{
	public class GuestResponse
	{
		[Required(ErrorMessage = "Please enter your name")]
		public string Name
		{
			get;
			set;
		}

		[Required(ErrorMessage = "Please enter your phone")]
		public string Phone
		{
			get;
			set;
		}

		[Required(ErrorMessage = "Please enter your email address")]
		[RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
		public string Email
		{
			get;
			set;
		}

		[Required(ErrorMessage = "Please specify whether you will attend")]
		public bool? WillAttend
		{
			get;
			set;
		}

		public void Submit()
		{
			Debug.WriteLine("Submitting response for " + Name);
			//TODO: add email sending logic
		}
	}
}