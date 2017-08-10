//2006 IDesign Inc.  
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyNamespace
{
   [ServiceContract(Session = true)]
   interface IMyContract
   {
      [OperationContract]
      void StartSession();

      [OperationContract(IsInitiating = false)]
      void CannotStart();

      [OperationContract(IsTerminating = true)]
      void EndSession();

      [OperationContract(IsInitiating = true,IsTerminating = true)]
      void StartAndEndSession();
   }
   [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession)] 
   class MyService : IMyContract,IDisposable
   {
      int m_Counter = 0;

      public MyService()
      {
         MessageBox.Show("MyService","MyService.MyService()");
      }
      public void StartSession()
      {
         m_Counter++;
         string sessionID = OperationContext.Current.SessionId;
         Trace.WriteLine(sessionID);

         MessageBox.Show("Counter = " + m_Counter,"MyService.StartSession()");
      }
      public void EndSession()
      {
         m_Counter++;
         string sessionID = OperationContext.Current.SessionId;
         Trace.WriteLine(sessionID);

         MessageBox.Show("Counter = " + m_Counter,"MyService.EndSession()");
      }
      public void StartAndEndSession()
      {
         m_Counter++;
         string sessionID = OperationContext.Current.SessionId;
         Trace.WriteLine(sessionID);

         MessageBox.Show("Counter = " + m_Counter,"MyService.StartAndEndSession()");
      }
      public void CannotStart()
      {
         m_Counter++;
         string sessionID = OperationContext.Current.SessionId;
         Trace.WriteLine(sessionID);

         MessageBox.Show("Counter = " + m_Counter,"MyService.CannotStart()");
      }
      public void Dispose()
      {
         MessageBox.Show("Counter = " + m_Counter,"MyService.Dispose()");
      }
   }
}
