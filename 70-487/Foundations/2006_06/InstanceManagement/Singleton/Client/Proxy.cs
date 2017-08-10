//2006 IDesign Inc.  
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

public interface IMyContractChannel : IMyContract,IClientChannel
{
}

public partial class MyContractProxy : ClientBase<IMyContract>,IMyContract
{
    
    public MyContractProxy()
    {
    }
    
    public MyContractProxy(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public MyContractProxy(string endpointConfigurationName,string remoteAddress) : 
            base(endpointConfigurationName,remoteAddress)
    {
    }
    
    public MyContractProxy(string endpointConfigurationName,EndpointAddress remoteAddress) : 
            base(endpointConfigurationName,remoteAddress)
    {
    }
    
    public MyContractProxy(Binding binding,EndpointAddress remoteAddress) : 
            base(binding,remoteAddress)
    {
    }
    
    public void MyMethod()
    {
        InnerProxy.MyMethod();
    }
}

[ServiceContract]
public interface IMyOtherContract
{
    
    [OperationContract]
    void MyOtherMethod();
}

public interface IMyOtherContractChannel : IMyOtherContract,IClientChannel
{
}

public partial class MyOtherContractProxy : ClientBase<IMyOtherContract>,IMyOtherContract
{
    
    public MyOtherContractProxy()
    {
    }
    
    public MyOtherContractProxy(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public MyOtherContractProxy(string endpointConfigurationName,string remoteAddress) : 
            base(endpointConfigurationName,remoteAddress)
    {
    }
    
    public MyOtherContractProxy(string endpointConfigurationName,EndpointAddress remoteAddress) : 
            base(endpointConfigurationName,remoteAddress)
    {
    }
    
    public MyOtherContractProxy(Binding binding,EndpointAddress remoteAddress) : 
            base(binding,remoteAddress)
    {
    }
    
    public void MyOtherMethod()
    {
        InnerProxy.MyOtherMethod();
    }
}
