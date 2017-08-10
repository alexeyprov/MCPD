//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;


[ServiceContract]
interface IMyContract
{
   [OperationContract(IsOneWay = true)]
   void MyMethod();
}

class MyServiceB : IMyContract
{
   static int m_Counter = 1; 

   public void MyMethod()
   {
      MessageBox.Show("Counter = " + m_Counter++,GetType().ToString());
   }
}
