//2006 IDesign Inc.  
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;

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

         string sessionID = OperationContext.Current.SessionId;
         Trace.WriteLine("MyService.MyMethod() Session ID: " + (sessionID??"None"));

      }
      public void Dispose()
      {
         string sessionID = OperationContext.Current.SessionId;
         MessageBox.Show("Dispose()","MyService");
      }
   }
}
