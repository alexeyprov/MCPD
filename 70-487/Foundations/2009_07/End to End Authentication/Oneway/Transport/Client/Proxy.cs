// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using ServiceModelEx.ServiceBus;

[ServiceContract]
interface IMyContract
{
   [OperationContract(IsOneWay = true)]
   void MyMethod();
}

class MyContractClient : ClientBase<IMyContract>,IMyContract
{
   public MyContractClient() 
   {}

   public void MyMethod()
   {
      Channel.MyMethod();
   }
}
