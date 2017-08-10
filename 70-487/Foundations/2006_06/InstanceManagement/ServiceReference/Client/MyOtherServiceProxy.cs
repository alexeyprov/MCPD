//2006 IDesign Inc.  
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;



[ServiceContract]
public interface ISomeContract
{
   [OperationContract]
   void PassReference(EndpointAddress10 serviceAddress);
}

public interface ISomeContractChannel : ISomeContract,IClientChannel
{
}

public partial class SomeContractProxy : ClientBase<ISomeContract>,ISomeContract
{
    
    public SomeContractProxy()
    {
    }
    
    public SomeContractProxy(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public SomeContractProxy(string endpointConfigurationName,string remoteAddress) : 
            base(endpointConfigurationName,remoteAddress)
    {
    }
    
    public SomeContractProxy(string endpointConfigurationName,EndpointAddress remoteAddress) : 
            base(endpointConfigurationName,remoteAddress)
    {
    }
    
    public SomeContractProxy(Binding binding,EndpointAddress remoteAddress) : 
            base(binding,remoteAddress)
    {
    }

   public void PassReference(EndpointAddress10 serviceAddress)
    {
       InnerProxy.PassReference(serviceAddress);
    }

}
