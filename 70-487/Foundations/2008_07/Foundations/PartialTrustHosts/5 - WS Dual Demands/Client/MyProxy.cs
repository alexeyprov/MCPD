//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;


interface IMyContractCallback
{
   [OperationContract]
   void OnMyCallback();
}

[ServiceContract(CallbackContract = typeof(IMyContractCallback))]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}

class MyContractClient : DuplexClientBase<IMyContract>,IMyContract
{
   public MyContractClient(IMyContractCallback callback) : base(callback)
   {}

   public MyContractClient(IMyContractCallback callback,string endpointName) : base(callback,endpointName)
   {}

   public void MyMethod()
   {
      Channel.MyMethod();
   }
}
