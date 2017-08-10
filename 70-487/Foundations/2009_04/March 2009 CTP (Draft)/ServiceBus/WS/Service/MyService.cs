//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Security.Principal;
using System.Threading;
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
      m_Counter++;
      MessageBox.Show("Counter = " + m_Counter.ToString(),"MyService");
   }
}
