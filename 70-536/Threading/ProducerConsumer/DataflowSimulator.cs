using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ProducerConsumer
{
    internal sealed class DataflowSimulator : SimulatorBase
    {
        private BufferBlock<int> _block;
        private volatile bool _isCompleted;

        public override PerformanceStats Run(
            int producerCount, 
            int consumerCount, 
            int boundedCapacity, 
            int itemCount)
        {
            _block = new BufferBlock<int>(
                new DataflowBlockOptions
                {
                    BoundedCapacity = boundedCapacity
                });
            _block.Completion.ContinueWith(_ => _isCompleted = true);

            return base.Run(producerCount, consumerCount, boundedCapacity, itemCount);
        }

        protected override async Task ProduceItemsAsync(int producerIndex, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                int item = ProduceItem(producerIndex, i, count);
                await _block.SendAsync(item);
            }
        }

        protected override void OnProductionCompleted()
        {
            _block.Complete();
        }

        protected override async Task<int> ConsumeItemsAsync(int consumerIndex)
        {
            int total = 0;
            while (await _block.OutputAvailableAsync() || !_isCompleted)
            {
                if (_block.TryReceive(out int item))
                {
                    ConsumeItem(consumerIndex, item);
                    total++;
                }
            }

            return total;
        }
    }
}