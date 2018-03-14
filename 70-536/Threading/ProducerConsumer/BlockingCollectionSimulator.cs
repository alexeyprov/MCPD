using System.Collections.Concurrent;
using System.Threading;

namespace ProducerConsumer
{
    internal sealed class BlockingCollectionSimulator<T> : SimulatorBase
        where T : IProducerConsumerCollection<int>, new()
    {
        private readonly CancellationTokenSource _cts;
        private BlockingCollection<int> _data;

        public BlockingCollectionSimulator()
        {
            _cts = new CancellationTokenSource();
        }

        public override PerformanceStats Run(
            int producerCount, 
            int consumerCount, 
            int boundedCapacity, 
            int itemCount)
        {
            _data = new BlockingCollection<int>(new T(), boundedCapacity);
            return base.Run(producerCount, consumerCount, boundedCapacity, itemCount);
        }

        protected override void ProduceItems(int producerIndex, int count)
        {
            CancellationToken ct = _cts.Token;
            for (int i = 0; i < count; ++i)
            {
                _data.Add(ProduceItem(producerIndex, i, count), ct);
            }
        }

        protected override void OnProductionCompleted()
        {
            _data.CompleteAdding();
        }

        protected override int ConsumeItems(int consumerIndex)
        {
            int count = 0;
            foreach (int item in _data.GetConsumingEnumerable(_cts.Token))
            {
                ConsumeItem(consumerIndex, item);
                count++;
            }

            return count;
        }
    }
}