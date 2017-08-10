//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Security.Permissions;

[ServiceContract]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}

//Permit only what is nesessary
[UIPermission(SecurityAction.PermitOnly,Window = UIPermissionWindow.SafeTopLevelWindows)]
[SecurityPermission(SecurityAction.PermitOnly,Execution = true)]
class MyService : IMyContract
{
   public void MyMethod()
   {
      Form form = new TestForm();
      form.Text = AppDomain.CurrentDomain.FriendlyName;
      form.ShowDialog();
   }
}

