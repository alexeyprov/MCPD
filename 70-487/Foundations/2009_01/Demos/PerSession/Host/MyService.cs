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
   void Increment();
}

[ServiceBehavior(ReleaseServiceInstanceOnTransactionComplete = false)]
class MyService : IMyCounter
{
   Transactional<int> m_Counter = new Transactional<int>();

   public MyService()
   {
      MessageBox.Show("MyService()","MyService");
   }
   [OperationBehavior(TransactionScopeRequired = true)]
   public void Increment()
   {
      m_Counter.Value++;
      MessageBox.Show("Counter = " + m_Counter.Value,"MyService.Increment()");
   }
}