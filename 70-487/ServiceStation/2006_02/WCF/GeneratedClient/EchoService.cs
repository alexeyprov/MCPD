﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.26
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



[System.ServiceModel.ServiceContractAttribute(Namespace="http://example.org/echo/")]
public interface IEchoService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://example.org/echo/IEchoService/Echo", ReplyAction="http://example.org/echo/IEchoService/EchoResponse")]
    string Echo(string msg);
}

public interface IEchoServiceChannel : IEchoService, System.ServiceModel.IClientChannel
{
}

public partial class EchoServiceProxy : System.ServiceModel.ClientBase<IEchoService>, IEchoService
{
    
    public EchoServiceProxy()
    {
    }
    
    public EchoServiceProxy(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public EchoServiceProxy(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public EchoServiceProxy(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public EchoServiceProxy(System.ServiceModel.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public string Echo(string msg)
    {
        return base.InnerProxy.Echo(msg);
    }
}
