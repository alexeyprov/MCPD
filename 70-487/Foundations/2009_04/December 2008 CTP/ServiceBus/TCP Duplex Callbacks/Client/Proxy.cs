//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;

[ServiceContract(CallbackContract = typeof(IMyContractCallback))]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}

interface IMyContractCallback
{
   [OperationContract]
   void OnCallback();
}

class MyContractClient : DuplexClientBase<IMyContract>,IMyContract
{
   public MyContractClient(object callback) : base(callback)
   {}

   public void MyMethod()
   {
      Channel.MyMethod();
   }
}
