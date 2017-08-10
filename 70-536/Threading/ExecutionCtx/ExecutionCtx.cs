using System;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Threading;

class ExecutionCtx
{
	static void Main()
	{
		// Test in default context
		TestAccess("Default Context");

		// Test with denied File I/O permission
		FileIOPermission perm = new FileIOPermission(PermissionState.Unrestricted);
		perm.Deny();
		ExecutionContext ec = ExecutionContext.Capture(); // capture restricted EC
		TestAccess("No file permissions");
		
		// Make sure context is flowing by default
		TestOnPool("Flowing context");

		// Suppress flow, so now everything is OK for secondary thread		
		AsyncFlowControl afc = ExecutionContext.SuppressFlow();
		Debug.Assert(ExecutionContext.IsFlowSuppressed());
		TestOnPool("Suppressed flowing context");

		// Restore normal context again and allow context flow
		// NOTE: Capturing context here will return null reference!
		//ExecutionContext ec = ExecutionContext.Capture();
		SecurityPermission.RevertDeny();
		afc.Undo(); // this line can be replaced with "using"
		TestOnPool("Default (and flowing) context again");
		
		// Run in the previously captured (restricted) context
		ExecutionContext.Run(ec, //ExecutionContext
			new ContextCallback(TestAccess), //delegate
			"Captured context"); //state object
	}

	private static void TestAccess(object ctxState)
	{
		bool succeeded = false;
		try
		{
			File.GetAttributes("ExecutionCtx.cs");
			succeeded = true;
		}
		catch (SecurityException)
		{
		}

		Console.WriteLine("[Thread {0}] {1} : {2}", 
			Thread.CurrentThread.ManagedThreadId,
			ctxState, 
			succeeded);
	}

	private static void TestOnPool(string ctxState)
	{
		WaitCallback d = new WaitCallback(TestAccess);
		d.EndInvoke(d.BeginInvoke(ctxState, null, null));
	}
}
