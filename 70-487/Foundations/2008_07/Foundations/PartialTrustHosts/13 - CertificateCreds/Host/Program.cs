//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.ServiceModel;
using System.Security.Permissions;
using System.Security;
using ServiceModelEx;
using System.Net;
using System.Web.Security;
using System.Web;
using System.Data.SqlClient;

static class Program
{
   //Easiest way to create partial trust host is to permit only what is required

   //Permissions to execute host
   [SecurityPermission(SecurityAction.PermitOnly,Flags = SecurityPermissionFlag.Execution)]

   //Permissions to accept clients over WS with cert creds
   [WebPermission(SecurityAction.PermitOnly,Accept = "http://localhost:8000/MyService")]
   [SecurityPermission(SecurityAction.PermitOnly,Flags = SecurityPermissionFlag.ControlPrincipal)]

   //Permissions for message security with negotiation and validation
   [StorePermission(SecurityAction.PermitOnly,OpenStore = true,EnumerateStores = true,EnumerateCertificates = true)]

   //For demonstration, this Main() uses a form to prevent from shutting down, so grant this as well:
   [UIPermission(SecurityAction.PermitOnly,Window = UIPermissionWindow.SafeTopLevelWindows)]
   static void Main()
   {
      /* 
       * AppDomainHost can take the permissions to grant the service. Unspecified, such as:
       * 
       *    AppDomainHost host = new AppDomainHost(typeof(MyService));
       * 
       * it will grant full trust. AppDomainHost also demands the permissions it is asks to grant the service, to ensure that
       * no one is trying to have partial trust environment try to host a service with more permissions. 
       * Since this demo uses partial-trust host, we need to give explicit permissions to the service, and those permissions
       * must be a subset of the host's permissions for the demand to succeed, yet sufficient for the service to do work.
       * The service only traces, so execution is good enough.
      */   

      PermissionSet servicePermisions = CodeAccessSecurityHelper.PermissionSetFromStandardSet(StandardPermissionSet.Execution);

      AppDomainHost host = new AppDomainHost(typeof(MyService),servicePermisions);

      host.Open();
     
      Application.Run(new HostForm());

      host.Close();
   }
}