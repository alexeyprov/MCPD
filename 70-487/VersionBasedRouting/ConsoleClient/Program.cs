using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using VersionBasedRouting.Contracts;

namespace VersionBasedRouting.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter left operand: ");
            double l = double.Parse(Console.ReadLine());

            Console.Write("Enter right operand: ");
            double r = double.Parse(Console.ReadLine());

            using (ChannelFactory<ICalculatorService> factory = new ChannelFactory<ICalculatorService>("CalculatorEndpoint"))
            {
                ICalculatorService client = factory.CreateChannel();

                using (OperationContextScope ocs = new OperationContextScope((IContextChannel)client))
                {
                    MessageHeaders headers = OperationContext.Current.OutgoingMessageHeaders;

                    headers.Add(MessageHeader.CreateHeader("CalcVersion", "http://alexeypr.com/2015/09/VersionBasedRouting", 1));

                    Console.WriteLine("{0} + {1} = {2}", l, r, client.Add(l, r));
                }
            }

            Console.ReadLine();
        }
    }
}
