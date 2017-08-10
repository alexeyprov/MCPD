//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net


using System;
using System.Windows.Forms;
using Microsoft.ServiceBus;

namespace ServiceModelEx
{
   abstract class ServiceBusTreeNode : TreeNode
   {
      protected readonly ExplorerForm Form;
      protected readonly TransportClientEndpointBehavior Credential;

      public readonly ServiceBusNode ServiceBusNode;
     
      public ServiceBusTreeNode(ExplorerForm form,ServiceBusNode serviceBusNode,string text,int imageIndex) : base(text,imageIndex,imageIndex)
      {
         Form = form;
         ServiceBusNode = serviceBusNode;

         if(serviceBusNode != null)
         {
            string solution = ExtractSolution(new Uri(serviceBusNode.Address));
            Credential = form.Graphs[solution.ToLower()].Credential;
         }
      }
      abstract public void DisplayControl();

      
      public static string ExtractSolution(Uri address)
      {
         return address.Host.Split('.')[0];
      }
   }
}