//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Reflection;
using System.Security.Policy;
using System.Security;
using System.Security.Permissions;

static class Program
{
   //Easiest way to create partial trust client is to permit only what is required

   //Permissions to display form and execute
   [SecurityPermission(SecurityAction.PermitOnly,Flags = SecurityPermissionFlag.Execution)]
   [UIPermission(SecurityAction.PermitOnly,Window = UIPermissionWindow.SafeTopLevelWindows)]

   //Permissions to call over IPC with default creds
   [SecurityPermission(SecurityAction.PermitOnly,Flags = SecurityPermissionFlag.ControlPolicy|
                                                         SecurityPermissionFlag.ControlEvidence|
                                                         SecurityPermissionFlag.UnmanagedCode)]
   [EnvironmentPermission(SecurityAction.PermitOnly,Read = "USERNAME")]
   static void Main()
   {
      Application.Run(new MyClientForm());
   }
}