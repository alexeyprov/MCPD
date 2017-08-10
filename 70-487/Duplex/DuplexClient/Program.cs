using System;
using System.Threading;

namespace DuplexClient
{
    class Program
    {
        private static void Main()
        {
            TestGreeting();
            TestStockQuote();
        }

        private static void TestGreeting()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            EventWaitHandle @event = new AutoResetEvent(false);

            using (GreetingsAgent serviceAgent = new GreetingsAgent(@event))
            {
                serviceAgent.RequestGreeting(name);

                @event.WaitOne();
            }
        }

        private static void TestStockQuote()
        {
            const string ticker = "MSFT";

            Console.WriteLine("Subscribing to {0} quotes. Press ENTER to stop...", ticker);
            using (StockQuoteAgent serviceAgent = new StockQuoteAgent())
            {
                serviceAgent.Subscribe(ticker);
                Console.ReadLine();
            }
        }
    }
}
