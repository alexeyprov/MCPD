using System;
using Microsoft.ServiceBus;
using System.Threading;
using System.ServiceModel;

class Program
{
   static void Main()
   {
      Console.WriteLine("Creating buffer......");

      Uri bufferAddress = new Uri(@"https://myservicenamespace.servicebus.windows.net/MyQueue/");

      string issuer = "owner";
      string secret = "**********  Enter Your Secret Here  **********";

      TransportClientEndpointBehavior sharedSecret = new TransportClientEndpointBehavior();
      sharedSecret.CredentialType = TransportClientCredentialType.SharedSecret;
      sharedSecret.Credentials.SharedSecret.IssuerName = issuer;
      sharedSecret.Credentials.SharedSecret.IssuerSecret = secret;

      if(BufferExists(sharedSecret,bufferAddress))
      {
         DeleteBuffer(sharedSecret,bufferAddress);
      }

      MessageBufferPolicy bufferPolicy = new MessageBufferPolicy();

      bufferPolicy.MaxMessageCount = 123;
      bufferPolicy.TransportProtection = TransportProtectionPolicy.AllPaths;
      bufferPolicy.Discoverability = DiscoverabilityPolicy.Public;

      MessageBufferClient.CreateMessageBuffer(sharedSecret,bufferAddress,bufferPolicy);
      
      Console.WriteLine("Buffer is ready");
      Console.ReadLine();
   }
   static void DeleteBuffer(TransportClientEndpointBehavior serviceBusCredential,Uri bufferUri)
   {
      MessageBufferClient client = MessageBufferClient.GetMessageBuffer(serviceBusCredential,bufferUri);
      client.DeleteMessageBuffer();
   }
    
   static bool BufferExists(TransportClientEndpointBehavior serviceBusCredential,Uri bufferUri)
   {
      try
      {
         MessageBufferClient client = MessageBufferClient.GetMessageBuffer(serviceBusCredential,bufferUri);
         client.GetPolicy();
         return true;
      }
      catch(FaultException)
      {}
      return false;
   }
}


 



   