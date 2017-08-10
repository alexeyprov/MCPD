using System;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace CASDemands
{
	public class CASImperativeClass
	{
        [FileIOPermission(SecurityAction.Assert, Write = @"C:\Program Files\")]

        public static void createProgramFolder()
        {
/*            try
            {
                FileIOPermission filePermissions = new FileIOPermission(FileIOPermissionAccess.Write, @"C:\Program Files\");
                filePermissions.Demand();
                // Method logic
            }
            catch
            {
                // Error-handling logic
            } */

            // Deny access to the Windows directory
            FileIOPermission filePermissions = new FileIOPermission(FileIOPermissionAccess.Write, @"C:\Inetpub\");
            filePermissions.Assert();
            try
            {
                StreamWriter newFile = new StreamWriter(@"C:\Inetpub\NewFile.txt");
                newFile.WriteLine("Hello, World!");
                newFile.Close();
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
//            SecurityManager.IsGranted
            // Method logic

        }

        public static void requestWebPage()
        {
            /*
            try
            {
                Regex connectPattern = new Regex(@"http://www\.microsoft\.com/.*");
                WebPermission webPermissions = new WebPermission(NetworkAccess.Connect, connectPattern);
                webPermissions.Demand();
                // Method logic
            }
            catch
            {
                // Error-handling logic
            }

            */
            // Permit only Web access, and limit it to www.microsoft.com
            Regex connectPattern = new Regex(@"http://www\.microsoft\.com/.*");
            WebPermission webPermissions = new WebPermission(NetworkAccess.Connect, connectPattern);
            webPermissions.PermitOnly();
            // Method logic
        }	
    }
}
