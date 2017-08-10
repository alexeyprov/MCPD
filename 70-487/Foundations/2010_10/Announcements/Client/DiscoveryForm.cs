// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using ServiceModelEx;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ServiceModelEx.ServiceBus;
using Microsoft.ServiceBus;
using System.Windows.Forms;

[ServiceContract]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}


partial class DiscoveryForm : Form
{
   AnnouncementSink<IMyContract> m_AnnouncementSink;

   const string Secret = "E7IstfL+9E2cWE...+B4iUYzdFFkZTW4=";
   const string ServiceNamespace = "MyNamespace"; 

   public DiscoveryForm()
   {
      m_AnnouncementSink = new ServiceBusAnnouncementSink<IMyContract>(ServiceNamespace,Secret);
      m_AnnouncementSink.Open();

      //If you prefer the event-based model: 
      //m_AnnouncementSink.OnlineAnnouncementReceived += (address,scopes)=>
      //                                                 {
      //                                                    EndpointAddress endpointAddress = new EndpointAddress(address);
      //                                                    ChannelFactory<IMyContract> factory = new ChannelFactory<IMyContract>(new NetTcpRelayBinding(),endpointAddress);
      //                                                    factory.SetServiceBusCredentials(Secret);

      //                                                    IMyContract proxy = factory.CreateChannel();
      //                                                    proxy.MyMethod();

      //                                                    (proxy as ICommunicationObject).Close();
      //                                                 };

      InitializeComponent();
   }

   void OnCallService(object sender,EventArgs e)
   {  
      EndpointAddress address = m_AnnouncementSink[0];
      ChannelFactory<IMyContract> factory = new ChannelFactory<IMyContract>(new NetTcpRelayBinding(),address);
      factory.SetServiceBusCredentials(Secret);

      IMyContract proxy = factory.CreateChannel();
      proxy.MyMethod();

      (proxy as ICommunicationObject).Close();
   }

   void OnFormClosed(object sender,FormClosedEventArgs args)
   {
      m_AnnouncementSink.Close();
   }
}