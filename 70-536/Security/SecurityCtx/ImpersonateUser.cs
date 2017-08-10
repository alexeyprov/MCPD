using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

// Perform user impersonation.
public class ImpersonateUser : IDisposable
{
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool LogonUser(
        string lpszUsername, 
        string lpszDomain, 
        string lpszPassword, 
        int dwLogonType, 
        int dwLogonProvider, 
        ref IntPtr phToken);

    [DllImport("kernel32.dll")]
    public extern static bool CloseHandle(IntPtr handle);

    private IntPtr _tokenHandle = IntPtr.Zero;
    private WindowsImpersonationContext _impersonatedUser;

    // If you incorporate this code into a DLL, be sure to demand that it
    // runs with FullTrust.
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public ImpersonateUser()
    {
        try
        {
            string userName, domainName;
            // Use the unmanaged LogonUser function to get the user token for
            // the specified user, domain, and password.
            // To impersonate a user on this machine, use the local machine
            // name for the domain name.
            Console.Write("Enter the name of the domain to log on to: ");
            domainName = Console.ReadLine();

            Console.Write("Enter the logon name of the user that you wish to"
                + " impersonate on {0}: ", domainName);
            userName = Console.ReadLine();

            Console.Write("Enter the password for {0}: ", userName);

            const int LOGON32_PROVIDER_DEFAULT = 0;
            // Passing this parameter causes LogonUser to create a primary
            // token.
            const int LOGON32_LOGON_INTERACTIVE = 2;
            // Call  LogonUser to obtain a handle to an access token.
            bool returnValue = LogonUser(
                userName, 
                domainName, 
                Console.ReadLine(), 
                LOGON32_LOGON_INTERACTIVE, 
                LOGON32_PROVIDER_DEFAULT, 
                ref _tokenHandle);

            if (!returnValue)
            {
                int ret = Marshal.GetLastWin32Error();
                Console.WriteLine("LogonUser call failed with error code : " +
                    ret);
                _tokenHandle = IntPtr.Zero;
                throw new System.ComponentModel.Win32Exception(ret);
            }

            Console.WriteLine("LogonUser succeeded"); 
            Console.WriteLine("Value of the Windows NT token: " + 
                _tokenHandle);

            // Check the identity.
            Console.WriteLine("User name before the impersonation: " + 
                WindowsIdentity.GetCurrent().Name);

            WindowsIdentity newId = new WindowsIdentity(_tokenHandle);
            _impersonatedUser = newId.Impersonate();
            // Check the identity.
            Console.WriteLine("User name after the impersonation: " + 
                WindowsIdentity.GetCurrent().Name);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred. " + ex.Message);
        }
    }

	~ImpersonateUser()
	{
		Dispose(false);
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

    protected virtual void Dispose(bool disposing)
    {
		if (disposing)
		{
			if (_impersonatedUser != null)
			{
		        _impersonatedUser.Undo();
				_impersonatedUser = null;
			}

	        // Check the identity.
    	    Console.WriteLine("After Undo: " + WindowsIdentity.GetCurrent().Name);
		}

        // Free the tokens.
        if (_tokenHandle != IntPtr.Zero)
		{
            CloseHandle(_tokenHandle);
			_tokenHandle = IntPtr.Zero;
		}
    }
}
