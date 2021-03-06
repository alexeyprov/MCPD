﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DuplexClient.StockQuoteWcfService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://alexeypr.com/2015/06/Duplex", ConfigurationName="StockQuoteWcfService.StockQuoteService", CallbackContract=typeof(DuplexClient.StockQuoteWcfService.StockQuoteServiceCallback))]
    public interface StockQuoteService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://alexeypr.com/2015/06/Duplex/StockQuoteService/Subscribe")]
        void Subscribe(string ticker);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface StockQuoteServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://alexeypr.com/2015/06/Duplex/StockQuoteService/UpdateQuote")]
        void UpdateQuote(string ticker, decimal price);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface StockQuoteServiceChannel : DuplexClient.StockQuoteWcfService.StockQuoteService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StockQuoteServiceClient : System.ServiceModel.DuplexClientBase<DuplexClient.StockQuoteWcfService.StockQuoteService>, DuplexClient.StockQuoteWcfService.StockQuoteService {
        
        public StockQuoteServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public StockQuoteServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public StockQuoteServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public StockQuoteServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public StockQuoteServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Subscribe(string ticker) {
            base.Channel.Subscribe(ticker);
        }
    }
}
