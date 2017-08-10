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

static class Program
{
   //Easiest way to create partial trust client is to permit only what is required

   //Permissions to display form and execute
   [SecurityPermission(SecurityAction.PermitOnly,Flags = SecurityPermissionFlag.Execution)]
   [UIPermission(SecurityAction.PermitOnly,Window = UIPermissionWindow.SafeTopLevelWindows)]

   //Permissions to call over WS with default creds
   [EnvironmentPermission(SecurityAction.PermitOnly,Read = "USERNAME")]
   [WebPermission(SecurityAction.PermitOnly,Connect = "http://localhost:8000/MyService")]

   //Permissions for message security with negotiation and validation
   [StorePermission(SecurityAction.PermitOnly,OpenStore = true,EnumerateStores = true,EnumerateCertificates = true)]

   static void Main()
   {
      Application.Run(new MyClientForm());
   }
}