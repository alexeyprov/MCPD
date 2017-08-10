//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;


[ServiceContract]
interface IMyCounter
{
   [OperationContract]
   [TransactionFlow(TransactionFlowOption.Allowed)]
   void Increment();
}

class MyCounterClient : ClientBase<IMyCounter>,IMyCounter
{
   public void Increment()
   {
      Channel.Increment();
   }
}
