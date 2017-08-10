//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;


[ServiceContract]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}

class MyContractClient : ClientBase<IMyContract>,IMyContract
{
   public MyContractClient()
   {}

   public MyContractClient(string endpointConfigurationName) : base(endpointConfigurationName)
   {}

   public MyContractClient(Binding binding,EndpointAddress remoteAddress) : base(binding,remoteAddress)
   {}

   public void MyMethod()
   {
      Channel.MyMethod();
   }
}
