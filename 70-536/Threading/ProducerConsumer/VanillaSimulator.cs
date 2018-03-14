using System;
using System.Collections.Generic;
using System.Threading;

namespace ProducerConsumer
{
    internal sealed class VanillaSimulator : SimulatorBase
    {
        private readonly object _lock;
        private readonly CancellationTokenSource _cts;

        private Queue<int> _data;
        private SemaphoreSlim _freeNodes;
        private SemaphoreSlim _filledNodes;

        public VanillaSimulator()
        {
            _lock = new object();
            _cts = new CancellationTokenSource();
        }

        public override PerformanceStats Run(
            int producerCount, 
            int consumerCount, 
            int boundedCapacity, 
            int itemCount)
        {
            _data = new Queue<int>(boundedCapacity);
            _freeNodes = new SemaphoreSlim(boundedCapacity, boundedCapacity);
            _filledNodes = new SemaphoreSlim(0, boundedCapacity);

            try
            {
                return base.Run(producerCount, consumerCount, boundedCapacity, itemCount);
            }
            finally
            {
                _freeNodes.Dispose();
                _filledNodes.Dispose();
            }
        }

        protected override void ProduceItems(int producerIndex, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                int item = ProduceItem(producerIndex, i, count);
                _freeNodes.Wait();

                lock (_lock)
                {
                    _data.Enqueue(item);
                }

                _filledNodes.Release();
            }
        }

        protected override void OnProductionCompleted()
        {
            _cts.Cancel();
        }

        protected override int ConsumeItems(int consumerIndex)
        {
            CancellationToken token = _cts.Token;
            int consumedCount = 0;
            try
            {
                while (true)
                {
                    _filledNodes.Wait(token);

                    int item;
                    lock (_lock)
                    {
                        item = _data.Dequeue();
                    }

                    _freeNodes.Release();

                    ConsumeItem(consumerIndex, item);
                    consumedCount++;
                }
            }
            catch (OperationCanceledException)
            {
            }

            return consumedCount;
        }
    }
}

        