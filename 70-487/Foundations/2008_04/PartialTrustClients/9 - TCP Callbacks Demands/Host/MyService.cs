//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;

interface IMyContractCallback
{
   [OperationContract]
   void OnMyCallback();
}
[ServiceContract(CallbackContract=typeof(IMyContractCallback))]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}

[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
class MyService : IMyContract
{
   public void MyMethod()
   {
      MessageBox.Show("MyMethod()","MyService");
      IMyContractCallback callback = OperationContext.Current.GetCallbackChannel<IMyContractCallback>();
      callback.OnMyCallback();
   }
}
