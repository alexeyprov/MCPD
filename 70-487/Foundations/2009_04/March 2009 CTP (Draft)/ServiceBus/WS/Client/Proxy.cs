//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;

[ServiceContract]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}

class MyContractClient : ClientBase<IMyContract>,IMyContract
{
   public void MyMethod()
   {
      Channel.MyMethod();
   }
}
