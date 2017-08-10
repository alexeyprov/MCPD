using System;
using System.IO;
using System.Net;
using System.Security.Permissions;

namespace CASDemands
{
    public class CASProtectedClass
    {
        [FileIOPermission(SecurityAction.Deny, ViewAndModify = @"C:\Windows\")]
        public static void createProgramFolder()
        {
            // Method logic
        }

        [WebPermission(SecurityAction.PermitOnly, ConnectPattern = @"http://www\.microsoft\.com/.*")]
        public static void requestWebPage()
        {
            // Method logic
        }
    }
}
