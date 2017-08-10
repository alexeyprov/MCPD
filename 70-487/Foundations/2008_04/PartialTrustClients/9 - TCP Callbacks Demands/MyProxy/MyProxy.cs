//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using ServiceModelEx;
using System.Security;
using System.Security.Permissions;

public interface IMyContractCallback
{
   [OperationContract]
   void OnMyCallback();
}

[ServiceContract(CallbackContract = typeof(IMyContractCallback))]
public interface IMyContract
{
   [OperationContract]
   void MyMethod();
}

public class MyContractClient : PartialTrustDuplexClientBase<IMyContract,IMyContractCallback>,IMyContract
{
   public MyContractClient(IMyContractCallback callback) : base(callback)
   {}

   public MyContractClient(IMyContractCallback callback,string endpointName) : base(callback,endpointName)
   {}

   public void MyMethod()
   {
      Invoke("MyMethod");
   }
}
