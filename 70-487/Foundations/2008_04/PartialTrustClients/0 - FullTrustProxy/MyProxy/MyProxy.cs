//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Security;
using System.Security.Permissions;

[ServiceContract]
public interface IMyContract
{
   [OperationContract]
   void MyMethod();
}
[PermissionSet(SecurityAction.Assert,Name = "FullTrust")]
public class MyContractClient : ClientBase<IMyContract>,IMyContract,IDisposable
{
   public MyContractClient()
   {}

   public MyContractClient(string endpointName) : base(endpointName)
   {}

   public void MyMethod()
   {
      Channel.MyMethod();
   }
   public new void Close()
   {
      base.Close();
   }
   void IDisposable.Dispose()
   {
      Close();
   }
}