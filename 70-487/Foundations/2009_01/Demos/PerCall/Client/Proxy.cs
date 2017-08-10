//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;


[ServiceContract]
interface IMyCounter
{
   [OperationContract]
   [TransactionFlow(TransactionFlowOption.Allowed)]
   void Increment(string instanceId);

   [OperationContract]
   [TransactionFlow(TransactionFlowOption.Allowed)]
   void RemoveCounter(string instanceId);
}


class MyCounterClient : ClientBase<IMyCounter>,IMyCounter
{
   public void Increment(string instanceId)
   {
      Channel.Increment(instanceId);
   }

   public void RemoveCounter(string instanceId)
   {
      Channel.RemoveCounter(instanceId);
   }
}
