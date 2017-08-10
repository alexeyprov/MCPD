//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Runtime.CompilerServices;

namespace ServiceModelEx
{
   public static class InProcFactory
   {
      struct HostRecord
      {
         public HostRecord(ServiceHost host,string address)
         {
            Host = host;
            Address = new EndpointAddress(address);
         }
         public readonly ServiceHost Host;
         public readonly EndpointAddress Address;
      }
      static readonly Uri BaseAddress = new Uri("net.pipe://localhost/" + Guid.NewGuid().ToString());
      static readonly Binding Binding;
      static Dictionary<Type,HostRecord> m_Hosts = new Dictionary<Type,HostRecord>();

      static InProcFactory()
      {
         NetNamedPipeBinding binding = new NetNamedPipeContextBinding();
         binding.TransactionFlow = true;
         Binding = binding;
         AppDomain.CurrentDomain.ProcessExit += delegate
                                                {
                                                   foreach(HostRecord hostRecord in m_Hosts.Values)
                                                   {
                                                      hostRecord.Host.Close();
                                                   }
                                                 };
      }
      public static I CreateInstance<S,I>() where I : class
                                            where S : I
      {
         HostRecord hostRecord = GetHostRecord<S,I>();
         return ChannelFactory<I>.CreateChannel(Binding,hostRecord.Address);
      }
      static HostRecord GetHostRecord<S,I>() where I : class
                                             where S : I
      {
         HostRecord hostRecord;
         if(m_Hosts.ContainsKey(typeof(S)))
         {
            hostRecord = m_Hosts[typeof(S)];
         }
         else
         {
            ServiceHost host = new ServiceHost(typeof(S),BaseAddress);
            string address = BaseAddress.ToString() + Guid.NewGuid().ToString();
            hostRecord = new HostRecord(host,address);
            m_Hosts.Add(typeof(S),hostRecord);
            host.AddServiceEndpoint(typeof(I),Binding,address);
            host.Open();
         }
         return hostRecord;
      }
      public static void CloseProxy<I>(I instance) where I : class
      {
         ICommunicationObject proxy = instance as ICommunicationObject;
         Debug.Assert(proxy != null);
         proxy.Close();
      }
   }
}