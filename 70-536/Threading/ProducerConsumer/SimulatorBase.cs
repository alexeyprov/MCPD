using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    internal abstract class SimulatorBase : ISimulator
    {
        private const int DelayMs = 10;

        private int _runningProducerCount;
        private Barrier _phaser;

        public static bool ShouldLogDetails { get; set; }

        public virtual PerformanceStats Run(
            int producerCount, 
            int consumerCount, 
            int boundedCapacity, 
            int itemCount)
        {
            Stopwatch sw = new Stopwatch();
            using (_phaser = new Barrier(
                producerCount + consumerCount,
                _ => sw.Start()))
            {
                int itemsPerProducer = itemCount / producerCount;
                _runningProducerCount = producerCount;
                IEnumerable<Task> producerTasks = Enumerable.Range(0, producerCount)
                    .Select(i => RunProducerAsync(i, itemsPerProducer));

                IEnumerable<Task<int>> consumerTasks = Enumerable.Range(0, consumerCount)
                    .Select(i => RunConsumerAsync(i))
                    .ToArray();

                Task[] allTasks = producerTasks.Concat(consumerTasks).ToArray();
                Task.WaitAll(allTasks);
                sw.Stop();

                return new PerformanceStats
                {
                    ConsumedCount = consumerTasks.Sum(t => t.Result),
                    Elapsed = sw.Elapsed
                };
            }
        }

        protected virtual Task ProduceItemsAsync(int producerIndex, int count) =>
            Task.Run(() => ProduceItems(producerIndex, count));

        protected virtual void ProduceItems(int producerIndex, int count) =>
            throw new NotImplementedException();

        protected virtual void OnProductionCompleted()
        {
        }

        protected virtual Task<int> ConsumeItemsAsync(int consumerIndex) =>
            Task.Run(() => ConsumeItems(consumerIndex));

        protected virtual int ConsumeItems(int consumerIndex) =>
            throw new NotImplementedException();

        protected static int ProduceItem(int producerIndex, int index, int count)
        {
            int item = producerIndex * count + index;

            if (ShouldLogDetails)
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine($"[P={producerIndex},T={threadId}]: Produced {item}");
            }

            //Thread.Sleep(DelayMs);

            return item;
        }

        protected static void ConsumeItem(int consumerIndex, int item)
        {
            if (ShouldLogDetails)
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine($"\t\t[C={consumerIndex},T={threadId}]: Consumed {item}");
            }

            //Thread.Sleep(DelayMs);
        }

        private async Task RunProducerAsync(int producerIndex, int count)
        {
            await Task.Run(() => _phaser.SignalAndWait());
            try
            {
                await ProduceItemsAsync(producerIndex, count);
            }
            finally
            {
                if (Interlocked.Decrement(ref _runningProducerCount) == 0)
                {
                    OnProductionCompleted();
                }
            }
        }

        private async Task<int> RunConsumerAsync(int consumerIndex)
        {
            await Task.Run(() => _phaser.SignalAndWait());
            return await ConsumeItemsAsync(consumerIndex);
        }
    }
}