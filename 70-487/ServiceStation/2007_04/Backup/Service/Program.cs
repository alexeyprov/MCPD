using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Library;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(CustomerService),
                new Uri("http://localhost:8080/customerservice")))
            {
                MtomMessageEncodingBindingElement mtom = new MtomMessageEncodingBindingElement(
                    MessageVersion.Soap12, Encoding.UTF8);

                CustomBinding binding = new CustomBinding();
                binding.Elements.Add(mtom);
                binding.Elements.Add(new HttpTransportBindingElement());

                host.AddServiceEndpoint(typeof(IUniversalOneWay), binding, "");
                host.Open();

                Console.WriteLine("CustomerService is up and running...");
                Console.ReadKey();
            }
        }
    }
}
