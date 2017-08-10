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
   const string Secret = "E7IstfL...FkZTW4=";
   const string ServiceNamespace = "MyNamespace";

   public DiscoveryForm()
   {
      InitializeComponent();
   }

   void OnAllScopes(object sender,EventArgs e)
   {
      Binding binding = new NetTcpRelayBinding();

      for(int i = 1; i <= 20; i++)
      {
         EndpointAddress address = ServiceBusDiscoveryHelper.DiscoverAddress<IMyContract>(ServiceNamespace,Secret);

         ChannelFactory<IMyContract> factory = new ChannelFactory<IMyContract>(binding,address);
         factory.SetServiceBusCredentials(Secret);
         IMyContract proxy = factory.CreateChannel();
         proxy.MyMethod();
         (proxy as ICommunicationObject).Close();
      }
   }

   void OnMyApplicationScope(object sender,EventArgs e)
   {
      Binding binding = new NetTcpRelayBinding();

      for(int i = 1; i <= 20; i++)
      {
         EndpointAddress address = ServiceBusDiscoveryHelper.DiscoverAddress<IMyContract>(ServiceNamespace,Secret,new Uri("sb://MyApplication"));
 
         ChannelFactory<IMyContract> factory = new ChannelFactory<IMyContract>(binding,address);
         factory.SetServiceBusCredentials(Secret);
         IMyContract proxy = factory.CreateChannel();
         proxy.MyMethod();
         (proxy as ICommunicationObject).Close();
      }
   }
}