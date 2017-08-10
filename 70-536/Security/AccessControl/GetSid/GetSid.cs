using System;
using System.Security;
using System.Security.Principal;

class GetSid
{
	static void Main()
	{
		WindowsIdentity wi = WindowsIdentity.GetCurrent();
		SecurityIdentifier sid = wi.User;
		Console.WriteLine("SID = " + sid.Value);
	}
}