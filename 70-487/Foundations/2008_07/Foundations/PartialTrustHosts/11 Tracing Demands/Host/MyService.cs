//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;

[ServiceContract]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}

class MyService : IMyContract
{
   public void MyMethod()
   {
   }
}
