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
   [ThreadPoolBehavior(3,typeof(MyService))]
   class MyService : IMyContract,IMyOtherContract
   {
      public void MyMethod()
      {
         MessageBox.Show("Thread Name = " + Thread.CurrentThread.Name,"MyService.MyMethod()");
      }
      public void MyOtherMethod()
      {
         MessageBox.Show("Thread Name = " + Thread.CurrentThread.Name,"MyService.MyOtherMethod()");
      }
   }
}
