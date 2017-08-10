//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.ServiceBus;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ServiceModelEx;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;

namespace ServiceModelEx.ServiceBus
{
   public static partial class ServiceBusHelper
   {
      static void SetServiceBusCertificate(Collection<ServiceEndpoint> endpoints,string subjectName)
      {
         if(String.IsNullOrEmpty(subjectName))
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
   }
}





