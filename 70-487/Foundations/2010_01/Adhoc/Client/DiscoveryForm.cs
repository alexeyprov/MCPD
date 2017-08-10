// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using ServiceModelEx;
using System.ServiceModel;
using System.ServiceModel.Channels;

[ServiceContract]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}


partial class DiscoveryForm : System.Windows.Forms.Form
{
   public DiscoveryForm()
   {
      InitializeComponent();
   }

   void OnDiscoverAddress(object sender,EventArgs e)
   {
      EndpointAddress address = DiscoveryHelper.DiscoverAddress<IMyContract>();
      Binding binding = new NetTcpBinding();

      IMyContract proxy = ChannelFactory<IMyContract>.CreateChannel(binding,address);
      proxy.MyMethod();

      (proxy as ICommunicationObject).Close();
   }

   void OnDiscoverMEX(object sender,EventArgs e)
   {
      IMyContract proxy = DiscoveryFactory.CreateChannel<IMyContract>();
      proxy.MyMethod();

      (proxy as ICommunicationObject).Close();
   }
}