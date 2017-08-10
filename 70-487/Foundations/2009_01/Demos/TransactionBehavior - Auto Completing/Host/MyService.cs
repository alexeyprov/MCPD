//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using ServiceModelEx;
using System.ServiceModel.Description;


[ServiceContract]
interface IMyCounter
{
   [OperationContract]
   [TransactionFlow(TransactionFlowOption.Allowed)]
   void Increment();
}


[Serializable]
[TransactionalBehavior]
class MyService : IMyCounter
{
   int m_Counter = 0;

   public MyService()
   {
      MessageBox.Show("MyService()","MyService");
   }

   public void Increment()
   {
      m_Counter++;
      MessageBox.Show("Counter = " + m_Counter,"MyService.Increment()");
   }
}
