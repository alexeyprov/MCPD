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

   //Permissions to pass alternative Windows Credentials
   [SecurityPermission(SecurityAction.PermitOnly,Flags = SecurityPermissionFlag.ControlPrincipal)]
   static void Main()
   {
      Application.Run(new MyClientForm());
   }
}