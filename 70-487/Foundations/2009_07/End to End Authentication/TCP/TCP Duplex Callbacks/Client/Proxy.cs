// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using ServiceModelEx.ServiceBus;


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

class MyContractClient : ServiceBusDuplexClientBase<IMyContract,IMyContractCallback>,IMyContract
{
   public MyContractClient(IMyContractCallback callback) : base(callback)
   {}
   public void MyMethod()
   {
      Channel.MyMethod();
   }
}
