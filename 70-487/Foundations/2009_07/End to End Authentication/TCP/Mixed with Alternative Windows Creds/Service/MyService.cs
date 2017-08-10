// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.ServiceModel;
using System.Threading;
using System.Security.Principal;


[ServiceContract]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}

class MyService : IMyContract
{
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
