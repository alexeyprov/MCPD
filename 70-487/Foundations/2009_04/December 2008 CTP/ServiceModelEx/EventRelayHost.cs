//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.ServiceModel;
using Microsoft.ServiceBus;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ServiceModelEx
{
   public class EventRelayHost 
   {
      //For serivce cert lookup
      StoreLocation m_ServiceCertLocation;
      StoreName m_ServiceCertStoreName;
      X509FindType m_ServiceCertFindType;
      object m_ServiceCertFindValue;

      //For message security
      string m_ApplicationName;
      bool m_UseProviders;
      bool m_Anonymous;

      //Managing the host-per-event
      Dictionary<Type,Dictionary<string,ServiceBusHost>> m_Hosts = new Dictionary<Type,Dictionary<string,ServiceBusHost>>();
      Type m_SericeType;
      string[] m_BaseAddresses;
      object m_SingletonInstance;
      NetEventRelayBinding m_Binding;

      //Service bus credentials
      string m_ServiceBusPassword;
      StoreLocation m_ServiceBusCertLocation;
      StoreName m_ServiceBusCertStoreName;
      X509FindType m_ServiceBusCertFindType;
      object m_ServiceBusCertFindValue;

      public EventRelayHost(object singletonInstance,string baseAddress) : this(singletonInstance,new string[]{baseAddress})
      {}
      public EventRelayHost(object singletonInstance,string[] baseAddresses) 
      {
         Debug.Assert(baseAddresses != null);
         Debug.Assert(baseAddresses.Length > 0);

         m_SingletonInstance = singletonInstance;

         for(int index = 0;index < baseAddresses.Length;index++)
         {
            if(baseAddresses[index].EndsWith("/") == false)
            {
               baseAddresses[index] += "/";
            }
         }

         m_BaseAddresses = baseAddresses;

         //Try to guess a certificate 
         ConfigureAnonymousMessageSecurity(ServiceBusHelper.ExtractSolution(new Uri(baseAddresses[0])));
      }
      public EventRelayHost(Type serviceType,string baseAddress) : this(serviceType,new string[]{baseAddress})
      {}
      public EventRelayHost(Type serviceType,string[] baseAddresses)
      {
         Debug.Assert(baseAddresses != null);
         Debug.Assert(baseAddresses.Length > 0);

         m_SericeType = serviceType;

         for(int index = 0;index < baseAddresses.Length;index++)
         {
            if(baseAddresses[index].EndsWith("/") == false)
            {
               baseAddresses[index] += "/";
            }
         }
         m_BaseAddresses = baseAddresses;
          
         //Try to guess a certificate 
         ConfigureAnonymousMessageSecurity(ServiceBusHelper.ExtractSolution(new Uri(baseAddresses[0])));
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

      public void ConfigureAnonymousMessageSecurity(string serviceCert)
      {
         ConfigureAnonymousMessageSecurity(serviceCert,StoreLocation.LocalMachine,StoreName.My);
      }
      public void ConfigureAnonymousMessageSecurity(string serviceCert,StoreLocation location,StoreName storeName)
      {
         ConfigureAnonymousMessageSecurity(location,storeName,X509FindType.FindBySubjectName,serviceCert);
      }
            
      [MethodImpl(MethodImplOptions.Synchronized)]
      public void ConfigureAnonymousMessageSecurity(StoreLocation location,StoreName storeName,X509FindType findType,object findValue)
      {
         m_ServiceCertLocation = location;
         m_ServiceCertStoreName = storeName;
         m_ServiceCertFindType = findType;
         m_ServiceCertFindValue = findValue;
         m_Anonymous = true;
      }
      
      public void ConfigureMessageSecurity(string serviceCert)
      {
         ConfigureMessageSecurity(serviceCert,StoreLocation.LocalMachine,StoreName.My,true,null);
      }
      public void ConfigureMessageSecurity(string serviceCert,string applicationName)
      {
         ConfigureMessageSecurity(serviceCert,StoreLocation.LocalMachine,StoreName.My,true,applicationName);
      }
      public void ConfigureMessageSecurity(string serviceCert,bool useProviders,string applicationName)
      {
         ConfigureMessageSecurity(serviceCert,StoreLocation.LocalMachine,StoreName.My,useProviders,applicationName);
      }
      public void ConfigureMessageSecurity(string serviceCert,StoreLocation location,StoreName storeName,bool useProviders,string applicationName)
      {
         ConfigureMessageSecurity(location,storeName,X509FindType.FindBySubjectName,serviceCert,useProviders,applicationName);
      }    
      [MethodImpl(MethodImplOptions.Synchronized)]
      void ConfigureMessageSecurity(StoreLocation location,StoreName storeName,X509FindType findType,object findValue,bool useProviders,string applicationName)
      {
         m_ServiceCertLocation = location;
         m_ServiceCertStoreName = storeName;
         m_ServiceCertFindType = findType;
         m_ServiceCertFindValue = findValue;
         m_UseProviders = useProviders;
         m_ApplicationName = applicationName;
         m_Anonymous = false;
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public void SetBinding(NetEventRelayBinding binding)
      {
         m_Binding = binding;
      }

      public void SetBinding(string bindingConfigName)
      {
         SetBinding(new NetEventRelayBinding(bindingConfigName));
      }
      
      [MethodImpl(MethodImplOptions.Synchronized)]
      public void Subscribe()
      {
         Type serviceType;
         if(m_SericeType != null)
         {
            serviceType = m_SericeType;
         }
         else
         {
            serviceType = m_SingletonInstance.GetType();
         }
         Type[] interfaces = serviceType.GetInterfaces();
         foreach(Type interfaceType in interfaces)
         {
            if(interfaceType.GetCustomAttributes(typeof(ServiceContractAttribute),false).Length == 1)
            {
               Subscribe(interfaceType);
            }
         }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public void Subscribe(Type contractType)
      {
         string[] operations = GetOperations(contractType);

         foreach(string operationName in operations)
         {
            Subscribe(contractType,operationName);
         }
      }
       
      [MethodImpl(MethodImplOptions.Synchronized)]
      public void Subscribe(Type contractType,string operation)
      {
         Debug.Assert(String.IsNullOrEmpty(operation) == false);

         if(m_Hosts.ContainsKey(contractType) == false)
         {
            m_Hosts[contractType] = new Dictionary<string,ServiceBusHost>();

            string[] operations = GetOperations(contractType);
            foreach(string operationName in operations)
            {
               m_Hosts[contractType][operationName] = null;
            }
         }
         if(m_Hosts[contractType][operation] == null)
         {
            List<Uri> baseAddressesList = new List<Uri>();

            foreach(string address in m_BaseAddresses)
            {
               Debug.Assert(address.Contains(ServiceBusEnvironment.DefaultRelayHostName));

               baseAddressesList.Add(new Uri(address + contractType));
            }

            if(m_SericeType != null)
            {
               m_Hosts[contractType][operation] = new ServiceBusHost(m_SericeType,baseAddressesList.ToArray());
            }
            else
            {
               m_Hosts[contractType][operation] = new ServiceBusHost(m_SingletonInstance,baseAddressesList.ToArray());
            }

            NetEventRelayBinding binding = m_Binding ?? new NetEventRelayBinding();

            m_Hosts[contractType][operation].AddServiceEndpoint(contractType,binding,operation);
            
            //Configure service bus credentials for the host
            if(m_ServiceBusPassword != null)
            {
               m_Hosts[contractType][operation].SetServiceBusPassword(m_ServiceBusPassword);
            }
            if(m_ServiceBusCertFindValue != null)
            {
               m_Hosts[contractType][operation].SetServiceBusCertificate(m_ServiceBusCertFindValue,m_ServiceBusCertLocation,m_ServiceBusCertStoreName,m_ServiceBusCertFindType);
            }

            //Configure message security
            if(m_Anonymous)
            {
               m_Hosts[contractType][operation].ConfigureAnonymousMessageSecurity(m_ServiceCertLocation,m_ServiceCertStoreName,m_ServiceCertFindType,m_ServiceCertFindValue);
            }
            else
            {
               m_Hosts[contractType][operation].ConfigureMessageSecurity(m_ServiceCertLocation,m_ServiceCertStoreName,m_ServiceCertFindType,m_ServiceCertFindValue,m_UseProviders,m_ApplicationName);
            }

            m_Hosts[contractType][operation].Open();
         }
      }
      
      [MethodImpl(MethodImplOptions.Synchronized)]
      public void Unsubscribe()
      {
         foreach(Type contractType in m_Hosts.Keys)
         {
            Unsubscribe(contractType);
         }
      }
            
      [MethodImpl(MethodImplOptions.Synchronized)]
      public void Unsubscribe(Type contractType)
      {
         string[] operations = GetOperations(contractType);

         foreach(string operationName in operations)
         {
            Unsubscribe(contractType,operationName);
         }
      }      
      
      [MethodImpl(MethodImplOptions.Synchronized)]
      public void Unsubscribe(Type contractType,string operation)
      {
         Debug.Assert(String.IsNullOrEmpty(operation) == false);

         if(m_Hosts.ContainsKey(contractType) == false)
         {
            return;
         }
         if(m_Hosts[contractType][operation] != null)
         {
            m_Hosts[contractType][operation].Close();
            m_Hosts[contractType][operation] = null;
         }
      }

      static string[] GetOperations(Type contract)
      {
         MethodInfo[] methods = contract.GetMethods(BindingFlags.Public|BindingFlags.FlattenHierarchy|BindingFlags.Instance);
         List<string> operations = new List<string>(methods.Length);

         Action<MethodInfo> add = (method)=>
                                  {
                                     Debug.Assert(! operations.Contains(method.Name));
                                     operations.Add(method.Name);
                                  };
         methods.ForEach(add);
         return operations.ToArray();
      }
      
      [MethodImpl(MethodImplOptions.Synchronized)]
      public void Abort()
      {
         foreach(Type contractType in m_Hosts.Keys)
         {
            foreach(ServiceHost host in m_Hosts[contractType].Values)
            {
               if(host != null)
               {
                  host.Abort();
               }
            }
         }
      }
   }
}
