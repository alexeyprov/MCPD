﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestClient.HeaderWcfService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="HeaderWcfService.IGetHeadersService")]
    public interface IGetHeadersService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGetHeadersService/GetHeaderValue", ReplyAction="http://tempuri.org/IGetHeadersService/GetHeaderValueResponse")]
        string GetHeaderValue(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGetHeadersService/GetCustomHeaderValue", ReplyAction="http://tempuri.org/IGetHeadersService/GetCustomHeaderValueResponse")]
        string GetCustomHeaderValue();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGetHeadersServiceChannel : TestClient.HeaderWcfService.IGetHeadersService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetHeadersServiceClient : System.ServiceModel.ClientBase<TestClient.HeaderWcfService.IGetHeadersService>, TestClient.HeaderWcfService.IGetHeadersService {
        
        public GetHeadersServiceClient() {
        }
        
        public GetHeadersServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GetHeadersServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GetHeadersServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GetHeadersServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetHeaderValue(string name) {
            return base.Channel.GetHeaderValue(name);
        }
        
        public string GetCustomHeaderValue() {
            return base.Channel.GetCustomHeaderValue();
        }
    }
}