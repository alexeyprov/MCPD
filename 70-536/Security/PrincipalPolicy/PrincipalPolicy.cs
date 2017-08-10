using System;
using System.Security.Principal;
using System.Threading;

internal static class Program
{
	private static void Main()
	{
		// try printing thread principal without any setup
	    PrintPrincipal();

		// make new threads use windows principal
		AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
		PrintPrincipal();

		// set principal explicitly, the policy will be ignored from now on
		IIdentity identity = new GenericIdentity("New User");
		AppDomain.CurrentDomain.SetThreadPrincipal(new GenericPrincipal(identity, null));
		PrintPrincipal();
	}

	/// <summary>
	/// Tests principal on a child thread in this domain
	/// </summary>
	private static void PrintPrincipal()
	{
		Thread t = new Thread(() =>
			{
				IPrincipal principal = Thread.CurrentPrincipal;
				if (principal != null)
				{
					Console.WriteLine("=====");
					Console.WriteLine("Principal type: {0}", principal.GetType());
					Console.WriteLine("Principal name: {0}", principal.Identity.Name);
					Console.WriteLine("Principal authenticated: {0}", principal.Identity.IsAuthenticated);
				}
		    });

		t.Start();
		t.Join();
	}
}