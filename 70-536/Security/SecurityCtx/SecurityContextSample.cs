using System;
using System.Threading;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

class SecurityContextSample
{
    static void Main()
    {
        try
        {
            Console.WriteLine("Executing the Main method in the primary " +
                "thread.");
            FileDialogPermission fdp = new FileDialogPermission(
                FileDialogPermissionAccess.OpenSave);
            fdp.Deny();

            // Do not allow the security context to pass across threads;
            // suppress its flow.
            using (AsyncFlowControl aFC = SecurityContext.SuppressFlow())
			{
            	Thread t1 = new Thread(new ThreadStart(DemandPermission));
            	t1.Start();
            	t1.Join();
            	Console.WriteLine("Is the flow suppressed? " +
                	SecurityContext.IsFlowSuppressed());
				Console.WriteLine("Restore the flow.");
			}

            Console.WriteLine("Is the flow suppressed? " +
                SecurityContext.IsFlowSuppressed());

            Thread t2 = new Thread(new ThreadStart(DemandPermission));
            t2.Start();
            t2.Join();

            CodeAccessPermission.RevertDeny();

            // Show the Deny is no longer present.
            Thread t3 = new Thread(new ThreadStart(DemandPermission));
            t3.Start();
            t3.Join();

            using (ImpersonateUser iU = new ImpersonateUser())
			{
            	Thread t5 = new Thread(new ThreadStart(CheckIdentity));
            	t5.Start();
            	t5.Join();

            	Console.WriteLine("Suppress the flow of the Windows identity.");
            	AsyncFlowControl aFC2 =
                	SecurityContext.SuppressFlowWindowsIdentity();

            	Console.WriteLine("Has the Windows identity flow been suppressed?"
	                + SecurityContext.IsWindowsIdentityFlowSuppressed());

	            Thread t6 = new Thread(new ThreadStart(CheckIdentity));
    	        t6.Start();
        	    t6.Join();

            	// Restore the flow of the Windows identity for the impersonated
	            // user.
    	        aFC2.Undo();
            }

			Console.WriteLine("User name after restoring the Windows identity"
                + " flow with Undo: \n" + WindowsIdentity.GetCurrent().Name);

            Console.WriteLine("This sample completed successfully;" +
                " press Enter to exit.");
            Console.Read();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    // Test method to be called on a second thread.
    static void DemandPermission()
    {
        try
        {
            Console.WriteLine("This is the thread executing the " +
                "DemandPermission method.");
            new FileDialogPermission(
                FileDialogPermissionAccess.OpenSave).Demand();
            Console.WriteLine("FileDialogPermission was successsfully" +
                " demanded.");
        }
        catch (SecurityException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    static void CheckIdentity()
    {
        Console.WriteLine("Current user: " +
            WindowsIdentity.GetCurrent().Name);
    }

}
