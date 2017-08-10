// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using ServiceModelEx.ServiceBus;


[ServiceContract]
interface IMyContract
{
   [OperationContract(IsOneWay = true)]
   void MyMethod(int counter);
}

class MyContractClient : BufferedServiceBusClient<IMyContract>,IMyContract
{
   public MyContractClient(string secret) : base(secret)
   {}
   public void MyMethod(int counter)
   {
      Enqueue(()=>Channel.MyMethod(counter));
   }
}
