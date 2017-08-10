using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Security;
using System.Security.Permissions;

namespace ListPermissions
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			writePermissionState(new FileIOPermission(PermissionState.Unrestricted));
			writePermissionState(new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME"));
			writePermissionState(new FileDialogPermission(FileDialogPermissionAccess.Open));
			writePermissionState(new IsolatedStorageFilePermission(PermissionState.Unrestricted));
			writePermissionState(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
			writePermissionState(new UIPermission(UIPermissionWindow.SafeTopLevelWindows));
			writePermissionState(new PrintingPermission(PrintingPermissionLevel.SafePrinting));
			
			Console.WriteLine("\nPress Enter key to continue");
			Console.Read();
		}

		static private void writePermissionState(CodeAccessPermission thisPermission)
		{
            Console.WriteLine(thisPermission.GetType().ToString() + ": " + SecurityManager.IsGranted(thisPermission));
		}
	}
}
