// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using ServiceModelEx.ServiceBus;

[ServiceContract]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}

class MyContractClient : ServiceBusClientBase<IMyContract>,IMyContract
{
   public MyContractClient(string username,string password) : base(username,password)
   {}

   public void MyMethod()
   {
      Channel.MyMethod();
   }
}
