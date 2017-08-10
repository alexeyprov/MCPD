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
}
