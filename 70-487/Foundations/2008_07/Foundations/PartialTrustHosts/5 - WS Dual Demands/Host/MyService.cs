//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;

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
      Trace.WriteLine("MyService.MyMethod()");
      IMyContractCallback callback = OperationContext.Current.GetCallbackChannel<IMyContractCallback>();
      callback.OnMyCallback();
   }
}
