using System;
using Microsoft.ServiceBus;
using System.ServiceModel;
using System.ServiceModel.Channels;

partial class MyClientForm : System.Windows.Forms.Form
{
   public MyClientForm()
   {
      InitializeComponent();
   }
   void OnCallService(object sender,EventArgs e)
   {
      string issuer = "owner";
      string secret = "**********  Enter Your Secret Here  **********";

      TransportClientEndpointBehavior sharedSecret = new TransportClientEndpointBehavior();
      sharedSecret.CredentialType = TransportClientCredentialType.SharedSecret;
      sharedSecret.Credentials.SharedSecret.IssuerName = issuer;
      sharedSecret.Credentials.SharedSecret.IssuerSecret = secret;

      Uri bufferUri = new Uri(@"https://myservicenamespace.servicebus.windows.net/MyQueue/");
      
      MessageBufferClient bufferClient = MessageBufferClient.GetMessageBuffer(sharedSecret,bufferUri);
      Message message = Message.CreateMessage(MessageVersion.Default,"Hello");
      bufferClient.Send(message);
   }
}


