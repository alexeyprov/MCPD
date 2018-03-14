using System;
using System.Collections.Concurrent;
using System.Linq;

namespace ProducerConsumer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.FirstOrDefault() == "-d")
            {
                SimulatorBase.ShouldLogDetails = true;
            }

            TestSimulator(new VanillaSimulator());
            TestSimulator(new BlockingCollectionSimulator<ConcurrentQueue<int>>());
            TestSimulator(new DataflowSimulator());
            TestSimulator(new WorkProcessorDisruptorSimulator());
            TestSimulator(new EventProcessorDisruptorSimulator());
        }

        private static void TestSimulator(ISimulator simulator)
        {
            GC.Collect();
            Console.WriteLine($"{Environment.NewLine}{simulator.GetType()}: starting...");

            PerformanceStats stats = simulator.Run(2, 2, 0x100, 100000);

            Console.WriteLine(
                $"{simulator.GetType()}: processed {stats.ConsumedCount} items in {stats.Elapsed}. Speed: {stats.Speed} items/ms");
        }
    }
}