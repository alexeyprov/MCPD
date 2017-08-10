// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;


[ServiceContract]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}

class MyService : IMyContract
{
   int m_Counter = 0;

   public void MyMethod()
   {
      CallsCounterForm form = Application.OpenForms[0] as CallsCounterForm;
      form.Counter = ++m_Counter;
   }
}
