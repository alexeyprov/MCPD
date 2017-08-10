// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;


[ServiceContract]
interface IMyContract
{
   [OperationContract(IsOneWay = true)]
   void MyMethod(int counter);
}

class MyService : IMyContract
{
   public void MyMethod(int counter)
   {
      MessageBox.Show("Counter = " + counter,"MyService");
   }
}
