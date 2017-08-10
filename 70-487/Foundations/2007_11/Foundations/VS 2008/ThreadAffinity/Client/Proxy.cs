//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

[ServiceContract]
public interface IMyContract
{
   [OperationContract]
   void MyMethod();
}

public partial class MyContractClient : ClientBase<IMyContract>,IMyContract
{
   public MyContractClient()
   {}

   public MyContractClient(string endpointConfigurationName) : base(endpointConfigurationName)
   {}

   public void MyMethod()
   {
      Channel.MyMethod();
   }
}

[ServiceContract]
public interface IMyOtherContract
{
   [OperationContract]
   void MyOtherMethod();
}

public partial class MyOtherContractClient : ClientBase<IMyOtherContract>,IMyOtherContract
{
   public MyOtherContractClient()
   {}

   public MyOtherContractClient(string endpointConfigurationName) : base(endpointConfigurationName)
   {}
   
   public void MyOtherMethod()
   {
      Channel.MyOtherMethod();
   }
}
