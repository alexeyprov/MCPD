using System;
using System.Threading.Tasks;

namespace SleepingBarber
{
    static class Program
    {
        private const int ChairCount = 5;
        private const int CustomerCount = 15;
        private const int BarberCount = 1;    

        private static void Main()
        {
            RunSimulator(new PlainSimulator()).Wait();
            RunSimulator(new BlockingCollectionSimulator()).Wait();
        }

        private static Task RunSimulator(ISimulator simulator)
        {
            Console.WriteLine($"Running {simulator.GetType()} simulator...");
            return simulator.Run(BarberCount, CustomerCount, ChairCount);
        }
    }
}