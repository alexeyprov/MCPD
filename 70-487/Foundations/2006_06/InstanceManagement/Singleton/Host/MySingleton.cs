//2006 IDesign Inc.  
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace MyNamespace
{
   [ServiceContract(Session = true)]
   interface IMyContract
   {
      [OperationContract]
      void MyMethod();
   }
   [ServiceContract]
   interface IMyOtherContract
   {
      [OperationContract]
      void MyOtherMethod();
   }
   [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
   class MySingleton : IMyContract,IMyOtherContract,IDisposable
   {
      int m_Counter = 0;
      public MySingleton()
      {
         MessageBox.Show("MyService.MyService()","MyService");
      }
      public void MyMethod()
      {
         m_Counter++;
         string sessionID = OperationContext.Current.SessionId;
         Trace.WriteLine(sessionID);

         MessageBox.Show("Counter = " + m_Counter + " MyMethod()","MyService");
      }
      public void MyOtherMethod()
      {
         m_Counter++;
         string sessionID = OperationContext.Current.SessionId;
         Trace.WriteLine(sessionID);

         MessageBox.Show("Counter = " + m_Counter + " MyOtherMethod()","MyService");
      }
      public void Dispose()
      {
         MessageBox.Show("MyService.Dispose()","MyService");
      }
   }
}
