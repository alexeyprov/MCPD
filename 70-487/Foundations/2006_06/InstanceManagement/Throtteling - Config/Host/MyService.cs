//2006 IDesign Inc.  
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceModel.Dispatcher;

namespace MyNamespace
{
   [ServiceContract]
   interface IMyContract
   {
      [OperationContract]
      void MyMethod();
   }
   class MyService : IMyContract,IDisposable
   {
      int m_Counter = 0;
      public void MyMethod()
      {
         m_Counter++;
         MessageBox.Show("MyMethod() " + m_Counter,"MyService");
         ChannelDispatcher dispatcher = OperationContext.Current.Host.ChannelDispatchers[0] as ChannelDispatcher;

         ServiceThrottle serviceThrottle = dispatcher.ServiceThrottle;

         Trace.WriteLine("MaxConcurrentCalls = "+ serviceThrottle.MaxConcurrentCalls);
         Trace.WriteLine("MaxConnections = " + serviceThrottle.MaxConnections);
         Trace.WriteLine("MaxInstances = " + serviceThrottle.MaxInstances);
      }
      public void Dispose()
      {
         string sessionID = OperationContext.Current.SessionId;
         MessageBox.Show("Dispose()","MyService");
      }
   }
}
