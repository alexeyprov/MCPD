using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace DynamicRouter
{
   [ServiceContract()]
   public interface ISay
   {
      [OperationContract(IsOneWay = true)]
      void Notify(string message);
   }

   [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
   public class PeerService2 : ISay
   {
      public PeerService2()
      {

      }
      public void Notify(string message)
      {


      }
   }

   public class PeerHost
   {
      public static void Start()
      {
         PeerService2 service = new PeerService2();
         ServiceHost host = new ServiceHost(service);
         //ServiceHost host = new ServiceHost(typeof(DynamicRouter.PeerService));

         
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