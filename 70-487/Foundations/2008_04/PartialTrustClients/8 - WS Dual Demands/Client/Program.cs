//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Reflection;
using System.Transactions;
using System.Net;
using System.Web;

static class Program
{
   //Easiest way to create partial trust client is to permit only what is required

   //Permissions to display form and execute
   [SecurityPermission(SecurityAction.PermitOnly,Flags = SecurityPermissionFlag.Execution)]
   [UIPermission(SecurityAction.PermitOnly,Window = UIPermissionWindow.SafeTopLevelWindows)]

   //Permissions to call over WS dual with default creds
   [EnvironmentPermission(SecurityAction.PermitOnly,Read = "USERNAME")]
   [WebPermission(SecurityAction.PermitOnly,Connect = "http://localhost:8000/MyService/")]

   //Permission to accept the callback
   [AspNetHostingPermission(SecurityAction.PermitOnly,Level = AspNetHostingPermissionLevel.Minimal)]
   [WebPermission(SecurityAction.PermitOnly,Accept = "http://localhost:9000/")]
   static void Main()
   {
      Application.Run(new MyClientForm());
   }
}