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
      void MyMethod(string text);
   }
   [PriorityCallsBehavior(1,typeof(MyService))]//Pool size can be any number, but easier to demo priority with 1
   class MyService : IMyContract
   {
      public void MyMethod(string text)
      {
         MessageBox.Show(text,"MyService.MyMethod()");
      }
   }
}
