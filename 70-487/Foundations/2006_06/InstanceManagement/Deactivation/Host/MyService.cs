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
   [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession)] 
   class MyService : IMyContract,IDisposable
   {
      int m_Counter = 0;

      public MyService()
      {
         MessageBox.Show("MyService","MyService.MyService()");
      }
      [OperationBehavior(ReleaseInstanceMode = ReleaseInstanceMode.AfterCall)]
      public void MyMethod()
      {
         m_Counter++;
         int contextID = OperationContext.Current.InstanceContext.GetHashCode();
         Trace.WriteLine("ContextID = " + contextID);

         string sessionID = OperationContext.Current.SessionId;
         Trace.WriteLine("MyMethod() Session ID " + sessionID);

         MessageBox.Show("Counter = " + m_Counter,"MyService.MyMethod()");
      }
      public void Dispose()
      {
         string sessionID = OperationContext.Current.SessionId;
         MessageBox.Show("Thread ID = " + Thread.CurrentThread.ManagedThreadId,"MyService.Dispose()");
      }
   }
}

