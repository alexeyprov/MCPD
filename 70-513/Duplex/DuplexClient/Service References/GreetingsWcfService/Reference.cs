﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DuplexClient.GreetingsWcfService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="DuplexService", ConfigurationName="GreetingsWcfService.IGreetingsService", CallbackContract=typeof(DuplexClient.GreetingsWcfService.IGreetingsServiceCallback))]
    public interface IGreetingsService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="DuplexService/IGreetingsService/RequestGreeting")]
        void RequestGreeting(string name);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGreetingsServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="DuplexService/IGreetingsService/GreetingGenerated")]
        void GreetingGenerated(string greeting);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGreetingsServiceChannel : DuplexClient.GreetingsWcfService.IGreetingsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GreetingsServiceClient : System.ServiceModel.DuplexClientBase<DuplexClient.GreetingsWcfService.IGreetingsService>, DuplexClient.GreetingsWcfService.IGreetingsService {
        
        public GreetingsServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public GreetingsServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public GreetingsServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GreetingsServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GreetingsServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void RequestGreeting(string name) {
            base.Channel.RequestGreeting(name);
        }
    }
}
