//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

static class Program
{
   //Easiest way to create partial trust client is to permit only what is required

   //Permissions to display form and execute
   [SecurityPermission(SecurityAction.PermitOnly,Flags = SecurityPermissionFlag.Execution)]
   [UIPermission(SecurityAction.PermitOnly,Window = UIPermissionWindow.SafeTopLevelWindows)]

   //Permissions to call over IPC with default creds
   [SecurityPermission(SecurityAction.PermitOnly,Flags = SecurityPermissionFlag.ControlPolicy|SecurityPermissionFlag.ControlEvidence)]
   [EnvironmentPermission(SecurityAction.PermitOnly,Read = "USERNAME")]
   static void Main()
   {
      Application.Run(new MyClientForm());
   }
}