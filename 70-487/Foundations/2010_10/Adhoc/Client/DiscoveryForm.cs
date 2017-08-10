// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.ServiceBus;
using ServiceModelEx.ServiceBus;

[ServiceContract]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}


partial class DiscoveryForm : System.Windows.Forms.Form
{
   const string Secret = "E7IstfL+9E2cW...4iUYzdFFkZTW4=";
   const string ServiceNamespace = "MyNamespace";

   public DiscoveryForm()
   {
      InitializeComponent();
   }

   void OnDiscoverAddress(object sender,EventArgs e)
   {
      EndpointAddress address = ServiceBusDiscoveryHelper.DiscoverAddress<IMyContract>(ServiceNamespace,Secret);
      Binding binding = new NetTcpRelayBinding();

      ChannelFactory<IMyContract> factory = new ChannelFactory<IMyContract>(binding,address);
      factory.SetServiceBusCredentials(Secret);

      IMyContract proxy = factory.CreateChannel();

      proxy.MyMethod();

      (proxy as ICommunicationObject).Close();
   }

   void OnDiscoverMEX(object sender,EventArgs e)
   {
      IMyContract proxy = ServiceBusDiscoveryFactory.CreateChannel<IMyContract>(ServiceNamespace,Secret);
      proxy.MyMethod();

      (proxy as ICommunicationObject).Close();
   }
}