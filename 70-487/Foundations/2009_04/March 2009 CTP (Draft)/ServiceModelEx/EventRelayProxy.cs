//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.ServiceBus;
using System.Reflection;
using System.ServiceModel.Channels;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;

namespace ServiceModelEx
{
   public abstract class EventRelayClientBase<T> : IDisposable where T : class
   {
      //State management 
      public event EventHandler Closed  = delegate{};
      public event EventHandler Closing = delegate{};
      public event EventHandler Opened  = delegate{};
      public event EventHandler Opening = delegate{};

      public CommunicationState State 
      {
         get;private set;
      }

      //Service bus items
      readonly string BaseAddress;
      NetOnewayRelayBinding m_Binding;
      Dictionary<string,T> m_Proxies;

      //For message security
      readonly string ServiceUsername;
      readonly string ServicePassword;
      readonly bool Anonymous;
   
      //For serivce cert lookup
      StoreLocation m_ServiceCertLocation;
      StoreName m_ServiceCertStoreName;
      X509FindType m_ServiceCertFindType;
      object m_ServiceCertFindValue;

      //Service bus credentials
      string m_ServiceBusPassword;
      StoreLocation m_ServiceBusCertLocation;
      StoreName m_ServiceBusCertStoreName;
      X509FindType m_ServiceBusCertFindType;
      object m_ServiceBusCertFindValue;

      public EventRelayClientBase(string solutionBaseAddress,string username,string password) : this(solutionBaseAddress,null,username,password)
      {}
      public EventRelayClientBase(string solutionBaseAddress,NetEventRelayBinding binding,string username,string password) : this(solutionBaseAddress,binding)
      {
         ServiceUsername = username;
         ServicePassword = password;
         Anonymous = false;
      }

      public EventRelayClientBase(string solutionBaseAddress) : this(solutionBaseAddress,null)
      {}      
      public EventRelayClientBase(string solutionBaseAddress,NetEventRelayBinding binding)
      {
         State = CommunicationState.Faulted;

         //Debug.Assert(solutionBaseAddress.Contains(ServiceBusEnvironment.DefaultRelayHostName));
         if(solutionBaseAddress.EndsWith("/") == false)
         {
            solutionBaseAddress += "/";
         }
         BaseAddress = solutionBaseAddress + typeof(T) + "/";

         m_Binding = binding;

         Anonymous = true;

         SetServiceCertificate("");

         State = CommunicationState.Created;
      }      
      
      [MethodImpl(MethodImplOptions.Synchronized)]
      public void Open()
      {
         if(State != CommunicationState.Created)
         {
            return;
         }
         try
         {
            Opening(this,EventArgs.Empty);

            Binding binding = m_Binding ?? new NetOnewayRelayBinding();
            ServiceBusHelper.ConfigureBinding(binding,Anonymous);

            m_Proxies = new Dictionary<string,T>();

            MethodInfo[] methods = typeof(T).GetMethods(BindingFlags.Public|BindingFlags.FlattenHierarchy|BindingFlags.Instance);

            foreach(MethodInfo method in methods)
            {
               EndpointIdentity identity = new DnsEndpointIdentity(m_ServiceCertFindValue.ToString());
               EndpointAddress address = new EndpointAddress(new Uri(BaseAddress + method.Name),identity);
               ChannelFactory<T> factory = new ChannelFactory<T>(binding,address);

               //Set credentials for message security (if needed)
               factory.Credentials.UserName.UserName = ServiceUsername;//could be null
               factory.Credentials.UserName.Password = ServicePassword;//could be null

               //Set service cert to secure message 
               ClientCredentials behavior = factory.Endpoint.Behaviors.Find<ClientCredentials>();
               behavior.ServiceCertificate.SetDefaultCertificate(m_ServiceCertLocation,m_ServiceCertStoreName,m_ServiceCertFindType,m_ServiceCertFindValue);

               //Set service bus creds
               if(m_ServiceBusPassword != null)
               {
                  factory.SetServiceBusPassword(m_ServiceBusPassword);
               }
               if(m_ServiceBusCertFindValue != null)
               {
                  factory.SetServiceBusCertificate(m_ServiceBusCertFindValue,m_ServiceBusCertLocation,m_ServiceBusCertStoreName,m_ServiceBusCertFindType);
               }
               m_Proxies[method.Name] = factory.CreateChannel();
               ICommunicationObject proxy = m_Proxies[method.Name] as ICommunicationObject;
               proxy.Open();
            }
            State = CommunicationState.Opened;

            Opened(this,EventArgs.Empty);
         }
         catch
         {
            State = CommunicationState.Faulted;
         }
      }
      public void Close()
      {
         if(State != CommunicationState.Opened)
         {
            return;
         }
         try
         {
            Closing(this,EventArgs.Empty);

            foreach(ICommunicationObject proxy in m_Proxies.Values)
            {
               proxy.Close();
            }
            State = CommunicationState.Closed;
            Closed(this,EventArgs.Empty);
         }
         catch
         {
            State = CommunicationState.Faulted;
         }
      }
      public void Abort()
      {
         try
         {
            foreach(ICommunicationObject proxy in m_Proxies.Values)
            {
               proxy.Abort();
            }
         }
         finally
         {
            State = CommunicationState.Faulted;
         }
      }

      public T Channel
      {
         get
         {
            if(State != CommunicationState.Opened)
            {
               Open();
            }
            StackFrame frame = new StackFrame(1);
            return m_Proxies[frame.GetMethod().Name];
         }
      }

      public void SetServiceCertificate(string serviceCert)
      {
         SetServiceCertificate(serviceCert,StoreLocation.LocalMachine,StoreName.My);
      }
      public void SetServiceCertificate(string serviceCert,StoreLocation location,StoreName storeName)
      {
         string subjectName;
         if(serviceCert == string.Empty)
         {
            subjectName = ServiceBusHelper.ExtractSolution(new Uri(BaseAddress));
         }
         SetServiceCertificate(serviceCert,location,storeName,X509FindType.FindBySubjectName);
      }
      public void SetServiceCertificate(object findValue,StoreLocation location,StoreName storeName,X509FindType findType)
      {
         m_ServiceCertLocation = location;
         m_ServiceCertStoreName = storeName;
         m_ServiceCertFindType = findType;
         m_ServiceCertFindValue = findValue;
       }
   
   
      public void SetServiceBusCertificate(object findValue,StoreLocation location,StoreName storeName,X509FindType findType) 
      {
         m_ServiceBusCertFindValue = findValue;
         m_ServiceBusCertLocation = location;
         m_ServiceBusCertStoreName = storeName;
         m_ServiceBusCertFindType = findType;
      }
      public void SetServiceBusCertificate(string subjectName) 
      {
         SetServiceBusCertificate(subjectName,StoreLocation.LocalMachine,StoreName.My,X509FindType.FindBySubjectName);
      }
      
      public void SetServiceBusPassword(string password) 
      {
         m_ServiceBusPassword = password;
      }  

      void IDisposable.Dispose()
      {
         Close();
      }
   }
}