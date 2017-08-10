//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.ServiceBus;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;

namespace ServiceModelEx
{
    public static class ServiceBusHelper
   {
      static void SetServiceBusCertificate(Collection<ServiceEndpoint> endpoints,string subjectName)
      {
         if(string.IsNullOrEmpty(subjectName))
         {
            if(endpoints[0].Binding is NetTcpRelayBinding   ||
               endpoints[0].Binding is WSHttpRelayBinding   ||
               endpoints[0].Binding is NetOnewayRelayBinding)
            {
               subjectName = ExtractSolution(endpoints[0].Address.Uri);
            }
         }
         //Sanity check: 
         foreach(ServiceEndpoint endpoint in endpoints)
         {
            if(endpoint.Binding is NetTcpRelayBinding   ||
               endpoint.Binding is WSHttpRelayBinding   ||
               endpoint.Binding is NetOnewayRelayBinding)
            {
               Debug.Assert(subjectName == ExtractSolution(endpoint.Address.Uri));
            }
         }

         SetServiceBusCertificate(endpoints,subjectName,StoreLocation.LocalMachine,StoreName.My,X509FindType.FindBySubjectName);
      }
      static void SetServiceBusCertificate(Collection<ServiceEndpoint> endpoints,object findValue,StoreLocation location,StoreName storeName,X509FindType findType)
      {
            TransportClientEndpointBehavior behavior = new TransportClientEndpointBehavior();
         behavior.CredentialType = TransportClientCredentialType.X509Certificate;
         behavior.Credentials.ClientCertificate.SetCertificate(location,storeName,findType,findValue);

         SetBehavior(endpoints,behavior);
      }

      static void SetServiceBusPassword(Collection<ServiceEndpoint> endpoints,string password)
      {
         string solution = null;

         if(endpoints[0].Binding is NetTcpRelayBinding   ||
            endpoints[0].Binding is WSHttpRelayBinding   ||
            endpoints[0].Binding is NetOnewayRelayBinding)
         {
            solution = ExtractSolution(endpoints[0].Address.Uri);
         }

         //Sanity check: 
         foreach(ServiceEndpoint endpoint in endpoints)
         {
            if(endpoint.Binding is NetTcpRelayBinding   ||
               endpoint.Binding is WSHttpRelayBinding   ||
               endpoint.Binding is NetOnewayRelayBinding)
            {
               Debug.Assert(solution == ExtractSolution(endpoint.Address.Uri));
            }
         }
         SetServiceBusPassword(endpoints,solution,password);
      }
      static void SetServiceBusPassword(Collection<ServiceEndpoint> endpoints,string solution,string password)
      {
         TransportClientEndpointBehavior behavior = new TransportClientEndpointBehavior();
         behavior.CredentialType = TransportClientCredentialType.UserNamePassword;
         behavior.Credentials.UserName.UserName = solution;
         behavior.Credentials.UserName.Password = password;

         SetBehavior(endpoints,behavior);
      }

      public static void SetServiceBusCertificate<T>(this ClientBase<T> proxy,object findValue,StoreLocation location,StoreName storeName,X509FindType findType) where T : class
      {
         if(proxy.State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Proxy is already opened");
         }
         proxy.ChannelFactory.SetServiceBusCertificate(findValue,location,storeName,findType);
      }
      public static void SetServiceBusCertificate<T>(this ClientBase<T> proxy,string subjectName) where T : class
      {
         if(proxy.State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Proxy is already opened");
         }
         proxy.ChannelFactory.SetServiceBusCertificate(subjectName);
      }
      public static void SetServiceBusCertificate<T>(this ClientBase<T> proxy) where T : class
      {
         proxy.ChannelFactory.SetServiceBusCertificate();
      }
      public static void SetServiceBusPassword<T>(this ClientBase<T> proxy,string solution,string password) where T : class
      {
         if(proxy.State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Proxy is already opened");
         }
         proxy.ChannelFactory.SetServiceBusPassword(solution,password);
      }
      public static void SetServiceBusPassword<T>(this ClientBase<T> proxy,string password) where T : class
      {
         if(proxy.State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Proxy is already opened");
         }
         proxy.ChannelFactory.SetServiceBusPassword(password);
      }

      public static void SetServiceBusCertificate<T>(this ChannelFactory<T> factory,object findValue,StoreLocation location,StoreName storeName,X509FindType findType) where T : class
      {
         if(factory.State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Factory is already opened");
         }
         Collection<ServiceEndpoint> endpoints = new Collection<ServiceEndpoint>();
         endpoints.Add(factory.Endpoint);
         SetServiceBusCertificate(factory,findValue,location,storeName,findType);
      }
      public static void SetServiceBusCertificate<T>(this ChannelFactory<T> factory,string subjectName) where T : class
      {
         if(factory.State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Factory is already opened");
         }
         Collection<ServiceEndpoint> endpoints = new Collection<ServiceEndpoint>();
         endpoints.Add(factory.Endpoint);
         SetServiceBusCertificate(endpoints,subjectName);
      }
      public static void SetServiceBusCertificate<T>(this ChannelFactory<T> factory) where T : class
      {
         SetServiceBusCertificate(factory,null);
      }
      public static void SetServiceBusPassword<T>(this ChannelFactory<T> factory,string password) where T : class
      {
         if(factory.State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Factory is already opened");
         }
         Collection<ServiceEndpoint> endpoints = new Collection<ServiceEndpoint>();
         endpoints.Add(factory.Endpoint);
         SetServiceBusPassword(endpoints,password);
      }
      public static void SetServiceBusPassword<T>(this ChannelFactory<T> factory,string solution,string password) where T : class
      {
         if(factory.State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Factory is already opened");
         }
         Collection<ServiceEndpoint> endpoints = new Collection<ServiceEndpoint>();
         endpoints.Add(factory.Endpoint);
         SetServiceBusPassword(endpoints,solution,password);
      }

      public static void SetServiceBusCertificate(this ServiceHost host,object findValue,StoreLocation location,StoreName storeName,X509FindType findType)
      {
         if(host.State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Host is already opened");
         }
         SetServiceBusCertificate(host.Description.Endpoints,findValue,location,storeName,findType);
      }
      public static void SetServiceBusCertificate(this ServiceHost host,string subjectName)
      {
         if(host.State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Host is already opened");
         }
         SetServiceBusCertificate(host.Description.Endpoints,subjectName);
      }
      public static void SetServiceBusCertificate(this ServiceHost host)
      {
         SetServiceBusCertificate(host.Description.Endpoints,null);
      }
      public static void SetServiceBusPassword(this ServiceHost host,string solution,string password)
      {
         if(host.State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Host is already opened");
         }
         SetServiceBusPassword(host.Description.Endpoints,solution,password);
      }
      public static void SetServiceBusPassword(this ServiceHost host,string password)
      {
         if(host.State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Host is already opened");
         }
         SetServiceBusPassword(host.Description.Endpoints,password);
      }

      //Helper methods 
      static void SetBehavior(Collection<ServiceEndpoint> endpoints,TransportClientEndpointBehavior behavior)
      {
         foreach(ServiceEndpoint endpoint in endpoints)
         {
            if(endpoint.Binding is NetTcpRelayBinding ||
               endpoint.Binding is WSHttpRelayBinding   ||
               endpoint.Binding is NetOnewayRelayBinding)
            {
               endpoint.Behaviors.Add(behavior);
            }
         }
      }
      internal static void ConfigureBinding(Binding binding)
      {
         ConfigureBinding(binding,true);
      }
      internal static void ConfigureBinding(Binding binding,bool anonymous)
      {
         if(binding is NetTcpRelayBinding)
         {
            NetTcpRelayBinding tcpBinding = (NetTcpRelayBinding)binding;
            tcpBinding.Security.Mode  = EndToEndSecurityMode.Message;
            if(anonymous)
            {
               tcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.None;
            }
            else
            {
               tcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            }

            tcpBinding.ConnectionMode = TcpRelayConnectionMode.Hybrid;
            tcpBinding.ReliableSession.Enabled = true; 

            return;
         }
         if(binding is WSHttpRelayBinding)
         {
            WSHttpRelayBinding wsBinding = (WSHttpRelayBinding)binding;
            wsBinding.Security.Mode = EndToEndSecurityMode.Message;
            if(anonymous)
            {
               wsBinding.Security.Message.ClientCredentialType = MessageCredentialType.None;
            }
            else
            {
               wsBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            }
            wsBinding.ReliableSession.Enabled = true; 

            return;
         }
         if(binding is NetOnewayRelayBinding)
         {
            NetOnewayRelayBinding onewayBinding = (NetOnewayRelayBinding)binding;
            onewayBinding.Security.Mode = EndToEndSecurityMode.Message;
            if(anonymous)
            {
               onewayBinding.Security.Message.ClientCredentialType = MessageCredentialType.None;
            }
            else
            {
               onewayBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            }
            return;
         }
         throw new InvalidOperationException(binding.GetType() + " is unsupported");
      }

      public static string ExtractSolution(Uri address)
      {
         return address.LocalPath.Split('/')[2];
      }

      public static string ExtractSolutionFromConfig(string endpointName)
      {
         Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
         ServiceModelSectionGroup sectionGroup = ServiceModelSectionGroup.GetSectionGroup(config);

         foreach(ChannelEndpointElement endpointElement in sectionGroup.Client.Endpoints)
         {
            if(endpointElement.Name == endpointName)
            {
               return ExtractSolution(endpointElement.Address);
            }
         }
         return null;
      }
      public static string ExtractSolutionFromConfig(Type serviceType)
      {
         Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
         ServiceModelSectionGroup sectionGroup = ServiceModelSectionGroup.GetSectionGroup(config);

         foreach(ServiceElement serviceElement in sectionGroup.Services.Services)
         {
            if(serviceElement.Name == serviceType.ToString())
            {
               return ExtractSolution(serviceElement.Endpoints[0].Address);
            }
         }
         return null;
      }
   }
}





