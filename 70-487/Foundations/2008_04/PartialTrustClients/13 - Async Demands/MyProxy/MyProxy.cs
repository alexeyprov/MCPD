//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using ServiceModelEx;
using System.Security;
using System.Security.Permissions;

[ServiceContract]
public interface IMyContract
{
   [OperationContract]
   void MyMethod();

   [OperationContract(AsyncPattern = true)]
   IAsyncResult BeginMyMethod(AsyncCallback callback,object state);

   void EndMyMethod(IAsyncResult async);
}

public class MyContractClient : PartialTrustClientBase<IMyContract>,IMyContract
{
   public MyContractClient()
   {}

   public MyContractClient(string endpointName) : base(endpointName)
   {}

   public void MyMethod()
   {
      Invoke("MyMethod");
   }

   public IAsyncResult BeginMyMethod(AsyncCallback callback,object state)
   {
      return Invoke("BeginMyMethod",callback,state) as IAsyncResult;
   }

   public void EndMyMethod(IAsyncResult async)
   {
      Channel.EndMyMethod(async);
   }
}
