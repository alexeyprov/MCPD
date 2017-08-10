//2008 IDesign Inc.   
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ServiceModelEx;



[ServiceContract]
public interface IMyContract
{
   [OperationContract]
   void MyMethod(string text);
}

public partial class MyContractClient : PriorityClientBase<IMyContract>,IMyContract
{
   public MyContractClient(CallPriority priority) : base(priority)
   {}
   public void MyMethod(string text)
   {
      Invoke("MyMethod",text);
   }
}