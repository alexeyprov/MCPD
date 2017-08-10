using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace DynamicRouter
{
   [ServiceContract()]
   public interface INotify
   {
      [OperationContract(IsOneWay = true)]
      void Notify(string message);
   }

   [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
   public class PeerService : INotify
   {
      public void Notify(string message)
      {


      }
   }

   public class PeerHost
   {
      public static void Start()
      {
         ServiceHost host = new ServiceHost(typeof(PeerService));

         
         var behavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
         if (behavior != null)
         {
            behavior.HttpGetEnabled = false;
         }
         /*
         NetPeerTcpBinding binding = new NetPeerTcpBinding();
         binding.Security.Mode = SecurityMode.None;
         binding.Resolver.Mode = System.ServiceModel.PeerResolvers.PeerResolverMode.Pnrp;                
         host.AddServiceEndpoint(typeof(INotify), binding, "net.p2p://Notification");
         */

         host.Open();     
      }



   }
}