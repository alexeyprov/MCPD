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

   void OnAllScopes(object sender,EventArgs e)
   {
      Binding binding = new NetTcpBinding();

      for(int i = 1; i <= 20; i++)
      {
         EndpointAddress address = DiscoveryHelper.DiscoverAddress<IMyContract>();
         IMyContract proxy = ChannelFactory<IMyContract>.CreateChannel(binding,address);
         proxy.MyMethod();
         (proxy as ICommunicationObject).Close();
      }
   }

   void OnMyApplicationScope(object sender,EventArgs e)
   {
      Binding binding = new NetTcpBinding();

      for(int i = 1; i <= 20; i++)
      {
         EndpointAddress address = DiscoveryHelper.DiscoverAddress<IMyContract>(new Uri("net.tcp://MyApplication"));
         IMyContract proxy = ChannelFactory<IMyContract>.CreateChannel(binding,address);
         proxy.MyMethod();
         (proxy as ICommunicationObject).Close();
      }
   }
}