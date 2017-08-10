using System;

namespace EchoServiceClientGenerated
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (EchoServiceProxy proxy = new EchoServiceProxy("IEchoService"))
                {
                    // invoke service operation
                    Console.WriteLine("Invoking HTTP endpoint: {0}",
                        proxy.Echo("Hello, world"));
                }
                using (EchoServiceProxy proxy = new EchoServiceProxy("IEchoService1"))
                {
                    // invoke service operation
                    Console.WriteLine("Invoking TCP endpoint: {0}",
                        proxy.Echo("Hello, world"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
