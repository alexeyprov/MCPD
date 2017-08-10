//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Net;

static class Program
{
   //Easiest way to create partial trust client is to permit only what is required

   //Permissions to display form and execute
   [SecurityPermission(SecurityAction.PermitOnly,Flags = SecurityPermissionFlag.Execution)]
   [UIPermission(SecurityAction.PermitOnly,Window = UIPermissionWindow.SafeTopLevelWindows)]

   //Permissions to call over TCP with default creds
   [EnvironmentPermission(SecurityAction.PermitOnly,Read = "USERNAME")]
   [DnsPermission(SecurityAction.PermitOnly,Unrestricted = true)]
   [SocketPermission(SecurityAction.PermitOnly,Access = "Connect",Transport = "Tcp",Host = "localhost",Port = "8000")]

   //Permissions to trace
   [EnvironmentPermission(SecurityAction.PermitOnly,Read = "COMPUTERNAME")]
   //Must be absolute path
   [FileIOPermission(SecurityAction.PermitOnly,
                     Append = @"C:\Temp\WCF\app_messages.svclog",
                     PathDiscovery = @"C:\Temp\WCF\app_messages.svclog",
                     Write = @"C:\Temp\WCF\app_messages.svclog")]
   [FileIOPermission(SecurityAction.PermitOnly,
                     Append = @"C:\Temp\WCF\app_tracelog.svclog",
                     PathDiscovery = @"C:\Temp\WCF\app_tracelog.svclog",
                     Write = @"C:\Temp\WCF\app_tracelog.svclog")]
   static void Main()
   {
      Application.Run(new MyClientForm());
   }
}