using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using ChatLibrary;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using NetHttpBindingLibrary;

namespace ChatServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(ChatService)))
            {
                /*
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                basicHttpBinding.MessageEncoding = WSMessageEncoding.Mtom;
                basicHttpBinding.Security.Mode = BasicHttpSecurityMode.Transport;
                
                host.AddServiceEndpoint(
                    typeof(IChatService),
                    basicHttpBinding,
                    "");

                // instantiate message encoding element and configure
                TextMessageEncodingBindingElement text =
                    new TextMessageEncodingBindingElement();
                text.MessageVersion = MessageVersion.Soap11WSAddressingAugust2004;

                // instantiate transport element and configure
                HttpTransportBindingElement http = new HttpTransportBindingElement();
                http.TransferMode = TransferMode.Streamed;
                http.UseDefaultWebProxy = true;
                http.AllowCookies = true;

                CustomBinding myHttpBinding = new CustomBinding();
                myHttpBinding.Name = "myHttpBinding";
                myHttpBinding.Elements.Add(text);
                myHttpBinding.Elements.Add(http);

                host.AddServiceEndpoint(typeof(IChat), myHttpBinding,
                    "custom");

                */
                try
                {

                    host.Open();
                    PrintServiceDescription(host);
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }
                Console.WriteLine("Service is up and running...");
                Console.ReadKey();
            }
        }
        static void PrintServiceDescription(ServiceHost host)
        {
            Console.WriteLine("{0} is running with the following endpoints:",
                host.Description.ServiceType);
            foreach (ServiceEndpoint se in host.Description.Endpoints)
            {
                Console.WriteLine("{0}",
                    se.Address);
            }
        }
    }
}
