using System;
using System.Collections.Generic;
using System.Text;

namespace PwdEncryption
{
	public class WrongPasswordException : Exception
	{
		public WrongPasswordException(string msg, Exception inner) : 
			base(msg, inner)
		{
		}
	}
}
