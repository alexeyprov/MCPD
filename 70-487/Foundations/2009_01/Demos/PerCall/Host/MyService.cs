//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using ServiceModelEx;
using ServiceModelEx.Transactional;


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


[ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
class MyService : IMyCounter
{
   static TransactionalDictionary<string,int> m_StateStore = new TransactionalDictionary<string,int>();

   public MyService()
   {
      MessageBox.Show("MyService()","MyService");
   }
   [OperationBehavior(TransactionScopeRequired=true)]
   public void Increment(string instanceId)
   {
      if(m_StateStore.ContainsKey(instanceId) == false)
      {
         m_StateStore[instanceId] = 0;
      }
      m_StateStore[instanceId]++;
      MessageBox.Show("Counter = " + m_StateStore[instanceId],"MyService.Increment()");
   }
   [OperationBehavior(TransactionScopeRequired=true)]
   public void RemoveCounter(string instanceId)
   {
      if(m_StateStore.ContainsKey(instanceId))
      {
         MessageBox.Show("RemoveCounter()","MyService");
         m_StateStore.Remove(instanceId);
      }
   }
}