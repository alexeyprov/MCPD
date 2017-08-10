// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using Microsoft.ServiceBus;
using System.ServiceModel.Channels;


class Program
{ 
   static void Main()
   {
      string issuer = "owner";
      string secret = "**********  Enter Your Secret Here  **********";

      TransportClientEndpointBehavior sharedSecret = new TransportClientEndpointBehavior();
      sharedSecret.CredentialType = TransportClientCredentialType.SharedSecret;
      sharedSecret.Credentials.SharedSecret.IssuerName = issuer;
      sharedSecret.Credentials.SharedSecret.IssuerSecret = secret;

      Uri bufferUri = new Uri(@"https://myservicenamespace.servicebus.windows.net/MyQueue/");

      MessageBufferClient bufferClient = MessageBufferClient.GetMessageBuffer(sharedSecret,bufferUri);
      Message message = bufferClient.Retrieve();

      System.Windows.Forms.MessageBox.Show( message.Headers.Action,"Retriever");
   }
}