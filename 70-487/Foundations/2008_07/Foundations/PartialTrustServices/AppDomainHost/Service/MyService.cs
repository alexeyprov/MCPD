//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;

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
      Form form = new TestForm();
      form.Text = AppDomain.CurrentDomain.FriendlyName;
      form.ShowDialog();
   }
}