//IDesign Inc. 2007 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Permissions;
using System.Threading;
using System.Security.Principal;
using ServiceModelEx;

namespace MyNamespace
{
   [ServiceContract]
   interface IMyContract
   {
      [OperationContract]
      void MyMethod();
   }

   //Add a user called 'CN=MyClientCert; 12A06153D25E94902F50971F68D86DCDE2A00756' to an application called 'MyApplication' and make it a member of the 'Manager' role

   [SecurityBehavior(ServiceSecurity.BusinessToBusiness,"MyServiceCert",UseAspNetProviders = true,ApplicationName = "MyApplication")]
   class MyService : IMyContract
   {
      [PrincipalPermission(SecurityAction.Demand,Role = "Manager")]
      public void MyMethod()
      {
         DisplayIdentities("MyService.MyMethod()");
      }

      static void DisplayIdentities(string caption)
      {
         string message;

         IPrincipal principal = Thread.CurrentPrincipal;
         ServiceSecurityContext context = ServiceSecurityContext.Current;
         if(context == null)
         {
            message =  "Token identity: " + WindowsIdentity.GetCurrent().Name + Environment.NewLine  +
                       "Principal identity: " + principal.Identity.GetType() + " with " +(principal.Identity.Name==""?"Blank identity":principal.Identity.Name) + Environment.NewLine  +
                       "Security context is null";
         }
         else
         {
            message =  "Token identity: " + WindowsIdentity.GetCurrent().Name + Environment.NewLine  +
                       "Principal identity: " + principal.Identity.GetType() + " with " +(principal.Identity.Name==""?"Blank identity":principal.Identity.Name) + Environment.NewLine  +
                       "Security context primary identity: " + context.PrimaryIdentity.GetType() + " with " + (context.PrimaryIdentity.Name==""?"Blank identity":context.PrimaryIdentity.Name) + Environment.NewLine +
                       "Security context Windows identity: " + context.WindowsIdentity.GetType() + " with " + (context.WindowsIdentity.Name==""?"Blank identity":context.WindowsIdentity.Name);
         }
         MessageBox.Show(message,caption);
      }
   }
}