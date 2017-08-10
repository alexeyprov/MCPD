using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using ChatLibrary;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();

            ChatMessage msg = new ChatMessage();
            msg.Username = "Aaron";
            msg.Timestamp = DateTime.Now;
            msg.Text = "Hey there!";

            ServiceEndpointCollection endpoints = 
                MetadataResolver.Resolve(typeof(IChat),
                new EndpointAddress("http://localhost:8080/chat/mex"));
            foreach (ServiceEndpoint se in endpoints)
            {
                ChannelFactory<IChat> cf = new ChannelFactory<IChat>(
                    se.Binding, se.Address);
                IChat client = cf.CreateChannel();
                client.SendMessage(msg);
            }
        }
    }
}
