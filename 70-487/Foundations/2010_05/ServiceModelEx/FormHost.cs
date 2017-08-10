// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.ServiceModel;


namespace ServiceModelEx
{
   [Serializable]
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
   public class FormHost : Form 
   {
      protected ServiceHost Host
      {
         get;set;
      }
      public FormHost(params string[] baseAddresses)
      {
         Host = new ServiceHost(this,baseAddresses.Select(address=>new Uri(address)).ToArray());

         Load += delegate
                 {
                    if(Host.State == CommunicationState.Created)
                    {
                       Host.Open();
                    }
                 };         
         FormClosed += delegate
                       {
                          if(Host.State == CommunicationState.Opened)
                          {
                             Host.Close();
                          }
                       };
      }
   }
}