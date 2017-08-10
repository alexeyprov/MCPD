using System;
using System.Text;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Library;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();

            MtomMessageEncodingBindingElement mtom = 
                new MtomMessageEncodingBindingElement(
                    MessageVersion.Soap12, Encoding.UTF8);

            CustomBinding binding = new CustomBinding();
            binding.Elements.Add(mtom);
            binding.Elements.Add(new HttpTransportBindingElement());

            ChannelFactory<IUniversalOneWay> cf = 
                new ChannelFactory<IUniversalOneWay>(binding);

            IUniversalOneWay channel = cf.CreateChannel(
                new EndpointAddress("http://localhost:8080/customerservice"));

            Customer cust = new Customer("Bob", "bob@abc.com");
            Message msg = Message.CreateMessage(
                MessageVersion.Soap12, "urn:add-customer", cust);

            channel.ProcessMessage(msg);
        }
    }
}
