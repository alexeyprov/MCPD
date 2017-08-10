// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;


[ServiceContract(CallbackContract = typeof(IMyContractCallback))]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}


[ServiceContract]
interface IMyContractCallback
{
   [OperationContract]
   void OnCallback();
}

[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
class MyService : IMyContract
{
   public void MyMethod()
   {
      IMyContractCallback callback = OperationContext.Current.GetCallbackChannel<IMyContractCallback>();
      callback.OnCallback();
   }
}
