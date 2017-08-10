//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net


using System;
using Microsoft.ServiceBus;
using System.Collections.Generic;
using System.Diagnostics;

namespace ServiceModelEx
{
   public class ServiceBusNode
   {
      public readonly string Address;

      public string Name
      {
         get;
         set;
      }

      public ServiceBusNode[] Subscribers;

      public ServiceBusNode[] SubscribedTo
      {
         get;
         set;
      }



      public JunctionPolicy Policy
      {
         get;
         set;
      }

      public uint SubscribersCount
      {
         get;
         set;
      }

      public ServiceBusNode(string address)
      {
         Address = address;
         Name = AddressToName(address);
      }

      static string AddressToName(string address)
      {
         if(String.IsNullOrEmpty(address))
         {
            return address;
         }
         Uri uri = new Uri(address);

         string localPath = uri.LocalPath;
         if(localPath.StartsWith("/"))
         {
            localPath = localPath.Remove(0,1);
         }
         if(localPath.EndsWith("/"))
         {
            localPath = localPath.Remove(localPath.Length-1,1);
         }
         return localPath;
      }

      public void AddSubscribedTo(ServiceBusNode[] subscribedTo)
      {
         if(SubscribedTo == null)
         {
            SubscribedTo = subscribedTo;
            return;
         }
         if(subscribedTo != null)
         {
            List<ServiceBusNode> list = new List<ServiceBusNode>(SubscribedTo);
            list.AddRange(subscribedTo);
            SubscribedTo = list.ToArray();
         }
      }

      public void AddSubscribedTo(ServiceBusNode subscribedTo)
      {
         if(subscribedTo == null)
         {
            return;
         }
         AddSubscribedTo(new ServiceBusNode[] { subscribedTo });
      }

      public void ReplaceSubscriber(ServiceBusNode service,ServiceBusNode junction)
      {
         if(Subscribers == null)
         {
            return;
         }
         Debug.Assert(service != null);
         Debug.Assert(junction != null);

         List<ServiceBusNode> list = new List<ServiceBusNode>(Subscribers);
         if(list.Contains(service))
         {
            list.Remove(service);
            list.Add(junction);
         }
         Subscribers = list.ToArray();
      }

      public void AddSubscriber(ServiceBusNode junction)
      {
         Debug.Assert(junction != null);

         if(Subscribers == null)
         {
            Subscribers = new ServiceBusNode[] { junction };
            return;
         }

         List<ServiceBusNode> list = new List<ServiceBusNode>(Subscribers);

         Debug.Assert(list.Contains(junction) == false);

         list.Add(junction);

         Subscribers = list.ToArray();
      }
   }
}