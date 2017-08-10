//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net


using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel.Syndication;
using Microsoft.ServiceBus;
using ServiceModelEx;

namespace ServiceModelEx
{
   class ServiceBusGraph
   {
      string Token
      {get;set;}

      string Solution
      {get;set;}      
      
      string Password
      {get;set;}

      public ServiceBusNode[] DiscoveredEndpoints
      {get;private set;}

      string m_ServiceBusRootAddress;

      public string ServiceBusRootAddress
      {
         get
         {
            return m_ServiceBusRootAddress;
         }
         set
         {
            m_ServiceBusRootAddress = value;
            if(m_ServiceBusRootAddress.StartsWith(@"/"))
            {
               m_ServiceBusRootAddress = m_ServiceBusRootAddress.Remove(0,1);
            }
            if(m_ServiceBusRootAddress.EndsWith(@"/"))
            {
               m_ServiceBusRootAddress = m_ServiceBusRootAddress.Remove(m_ServiceBusRootAddress.Length-1,1);
            }
         }
      }
      public readonly TransportClientEndpointBehavior Credential;

      public ServiceBusGraph(string solution,string password)
      {
         Solution = solution;
         Password = password;

         ServiceBusRootAddress = ServiceBusEnvironment.CreateServiceUri("https",solution,"").AbsoluteUri;

         ServiceBusRootAddress = VerifyEndSlash(ServiceBusRootAddress);

         Credential = new TransportClientEndpointBehavior();
         Credential.CredentialType = TransportClientCredentialType.UserNamePassword;
         Credential.Credentials.UserName.UserName = Solution;
         Credential.Credentials.UserName.Password = password;
      }


      public ServiceBusNode[] Discover()
      {
         DiscoveredEndpoints = null;

         if(Token == null)
         {
            Token = GetToken(Solution,Password);
         }

         List<ServiceBusNode> nodes = Discover(ServiceBusRootAddress,null);

         Consolidate(nodes);

         DiscoveredEndpoints = SortList(nodes);

         AssertIntegrery(DiscoveredEndpoints);

         return DiscoveredEndpoints;
      }

      void Consolidate(List<ServiceBusNode> nodes)
      {
         //Routers and queues subscriber, they will appear twice - once as routers or queues and once as policies
         //Keep just the policies
         List<ServiceBusNode> nodesToRemove = new List<ServiceBusNode>();
         uint policiesCount = 0;

         foreach(ServiceBusNode junction in nodes)
         {
            if(junction.Policy != null)
            {
               policiesCount++;
               foreach(ServiceBusNode service in nodes)
               {
                  if(service.Name == junction.Name && service.Policy == null)
                  {
                     nodesToRemove.Add(service);
                     junction.AddSubscribedTo(service.SubscribedTo);
                     foreach(ServiceBusNode node in nodes)
                     {
                        node.ReplaceSubscriber(service,junction);
                     }
                  }
               }
            }
         }
         if(policiesCount < nodesToRemove.Count)
         {
            MessageBox.Show("Solution feed has some inconsistencies","Service Bus Explorer",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
         }

         foreach(ServiceBusNode nodeToRemove in nodesToRemove)
         {
            nodes.Remove(nodeToRemove);
         }

         nodesToRemove.Clear();

         //Have only top-level items
         foreach(ServiceBusNode node in nodes)
         {
            if(node.SubscribedTo != null)
            {
               nodesToRemove.Add(node);
            }
         }

         foreach(ServiceBusNode node in nodesToRemove)
         {
            nodes.Remove(node);
         }

         //Remove all URIs that are prefex of others (the path leading to the address is reported as a seperate link
         nodesToRemove.Clear();

         foreach(ServiceBusNode part in nodes)
         {
            foreach(ServiceBusNode node in nodes)
            {
               if(node != part && node.Name.StartsWith(part.Name,StringComparison.OrdinalIgnoreCase))
               {
                  if(nodesToRemove.Contains(part) == false)
                  {
                     nodesToRemove.Add(part);
                  }
               }
            }
         }

         foreach(ServiceBusNode node in nodesToRemove)
         {
            nodes.Remove(node);
         }
      }
      void AssertIntegrery(ServiceBusNode[] array)
      {
         foreach(ServiceBusNode node in array)
         {
            if(node.SubscribersCount > 0)
            {
               Debug.Assert(node.Subscribers != null);
               foreach(ServiceBusNode subscriber in node.Subscribers)
               {
                  subscriber.SubscribedTo.Contains(node);
               }
            }
         }
      }
      ServiceBusNode[] SortList(List<ServiceBusNode> nodes)
      {
         ServiceBusNode[] array = new ServiceBusNode[nodes.Count];

         for(int i = 0;i<array.Length;i++)
         {
            ServiceBusNode maxNode = FindMax(nodes);
            array[i] = maxNode;
            nodes.Remove(maxNode);
         }
         //Transpose array
         ServiceBusNode[] returned = new ServiceBusNode[array.Length];

         int index = 0;
         for(int j = array.Length-1;j>=0;j--)
         {
            returned[index++] = array[j];
         }
         return returned;
      }
      ServiceBusNode FindMax(List<ServiceBusNode> nodes)
      {
         ServiceBusNode maxNode = new ServiceBusNode("");
         foreach(ServiceBusNode node in nodes)
         {
            if(StringComparer.Ordinal.Compare(node.Name,maxNode.Name) >= 0)
            {
               maxNode = node;
            }
         }
         return maxNode;
      }

      List<ServiceBusNode> Discover(string root,ServiceBusNode router)
      {
         root = VerifyNoEndSlash(root);

         Uri feedUri = new Uri(root);

         List<ServiceBusNode> nodes = new List<ServiceBusNode>();

         if(root.Contains("!") == false)
         {
            string relativeAddress = root.Replace(ServiceBusRootAddress,"");
            if(relativeAddress != "" && relativeAddress != "/")
            {
               ServiceBusNode node = new ServiceBusNode(root);
               node.AddSubscribedTo(router);
               nodes.Add(node);
            }
         }

         SyndicationFeed feed = GetFeed(feedUri,Token);

         if(feed != null)
         {
            foreach(SyndicationItem endpoint in feed.Items)
            {
               ServiceBusNode node = null;

               foreach(SyndicationLink link in endpoint.Links)
               {
                  if(IsJunction(endpoint))
                  {
                     //Look for policies
                     if(link.RelationshipType == "alternate")
                     {
                        node = new ServiceBusNode(link.Uri.AbsoluteUri);
                        node.AddSubscribedTo(router);

                        if(endpoint.ElementExtensions[0].OuterName == "RouterPolicy")
                        {
                           node.Policy = GetRouterPolicy(link.Uri.AbsoluteUri);
                        }
                        if(endpoint.ElementExtensions[0].OuterName == "QueuePolicy")
                        {
                           node.Policy = GetQueuePolicy(link.Uri.AbsoluteUri);
                        }
                        nodes.Add(node);
                     }
                  }
                  //Look for subscribers
                  if(node != null)
                  {
                     if(node.Policy is RouterPolicy)
                     {
                        if(link.RelationshipType == "subscriptions")
                        {
                           List<ServiceBusNode> subscribers = Discover(link.Uri.AbsoluteUri,node);

                           foreach(ServiceBusNode subscriber in subscribers)
                           {
                              subscriber.Name = subscriber.Name.Replace(node.Name + "/","");
                           }
                           nodes.AddRange(subscribers);

                           node.SubscribersCount = (uint)(subscribers.Count);
                           node.Subscribers = subscribers.ToArray();
                        }
                     }
                  }
                  if(link.RelationshipType == "alternate" && node == null)
                  {
                     if(node == null)
                     {
                        nodes.AddRange(Discover(link.Uri.AbsoluteUri,router));
                     }
                     else
                     {
                        nodes.Add(node);
                        node = null;
                     }
                  }
               }
            }
         }
         return nodes;
      }
      string GetToken(string solutionName,string solutionPassword)
      {
         string token = null;

         string tokenUri = string.Format("https://{0}/issuetoken.aspx?u={1}&p={2}",ServiceBusEnvironment.DefaultIdentityHostName,solutionName,Uri.EscapeDataString(solutionPassword));

         HttpWebRequest tokenRequest = WebRequest.Create(tokenUri) as HttpWebRequest;

         tokenRequest.Method = "GET";

         using(HttpWebResponse tokenResponse = tokenRequest.GetResponse() as HttpWebResponse)
         {
            StreamReader tokenStreamReader = new StreamReader(tokenResponse.GetResponseStream());

            token = tokenStreamReader.ReadToEnd();
         }
         return token;
      }
      static SyndicationFeed GetFeed(Uri feedUri,string token)
      {
         if(feedUri.Scheme != "http" && feedUri.Scheme != "https")
         {
            return null;
         }
         HttpWebRequest getFeedRequest = WebRequest.Create(feedUri) as HttpWebRequest;
         getFeedRequest.Method = "GET";
         getFeedRequest.Headers.Add("X-MS-Identity-Token",token);

         Atom10FeedFormatter atomFormatter = new Atom10FeedFormatter();

         try
         {
            using(HttpWebResponse getFeedResponse = getFeedRequest.GetResponse() as HttpWebResponse)
            {
               atomFormatter.ReadFrom(new XmlTextReader(getFeedResponse.GetResponseStream()));
            }
         }
         catch
         {
         }
         return atomFormatter.Feed;
      }
      static bool IsJunction(SyndicationItem item)
      {
         if(item.ElementExtensions.Count == 1)
         {
            return item.ElementExtensions[0].OuterName.Contains("Policy");
         }
         return false;
      }

      string VerifyEndSlash(string text)
      {
         Debug.Assert(text != null);

         if(text != String.Empty)
         {
            if(text.EndsWith("/") == false)
            {
               return text += "/";
            }
         }
         return text;
      }

      static string VerifyNoEndSlash(string text)
      {
         Debug.Assert(text != null);

         if(text != String.Empty)
         {
            if(text.EndsWith("/"))
            {
               return text.Remove(text.Length-1,1);
            }
         }
         return text;
      }

      RouterPolicy GetRouterPolicy(string address)
      {
         address = address.Replace(@"https://",@"sb://");
         address = address.Replace(@"http://",@"sb://");

         Uri routerAddress = new Uri(address);

         return RouterManagementClient.GetRouterPolicy(Credential,routerAddress);
      }

      QueuePolicy GetQueuePolicy(string address)
      {
         address = address.Replace(@"https://",@"sb://");
         address = address.Replace(@"http://",@"sb://");

         Uri routerAddress = new Uri(address);

         return QueueManagementClient.GetQueuePolicy(Credential,routerAddress);
      }
   }
}