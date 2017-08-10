//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using ServiceModelEx;

namespace MyNamespace
{
   [ServiceContract]
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

   [ThreadAffinityBehavior(typeof(MyService))]
   class MyService : IMyContract,IMyOtherContract
   {
      int m_Counter = 0;
      public MyService()
      {
         MessageBox.Show("MyService.MyService()","MyService");
      }
      public void MyMethod()
      {
         m_Counter++;
         MessageBox.Show("Counter = " + m_Counter + "","MyService.MyMethod() Thread Name = " + Thread.CurrentThread.Name);
      }
      public void MyOtherMethod()
      {
         m_Counter++;
         MessageBox.Show("Counter = " + m_Counter + "","MyService.MyOtherMethod() Thread Name = " + Thread.CurrentThread.Name);
      }
   }
}
