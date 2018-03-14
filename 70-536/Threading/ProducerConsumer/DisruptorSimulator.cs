using System.Collections.Generic;

using Disruptor;

namespace ProducerConsumer
{
    internal abstract class DisruptorSimulatorBase : SimulatorBase
    {
        private RingBuffer<EventData> _ringBuffer;
        private IReadOnlyList<IDisruptorConsumer> _consumers;

        public override PerformanceStats Run(
            int producerCount, 
            int consumerCount, 
            int boundedCapacity, 
            int itemCount)
        {
            _ringBuffer = producerCount == 1 ?
                RingBuffer<EventData>.CreateSingleProducer(
                    () => new EventData(),
                    boundedCapacity,
                    new SpinWaitWaitStrategy()) :
                RingBuffer<EventData>.CreateMultiProducer(
                    () => new EventData(),
                    boundedCapacity,
                    new SpinWaitWaitStrategy());

            _consumers = CreateConsumers(_ringBuffer, consumerCount);

            return base.Run(producerCount, consumerCount, boundedCapacity, itemCount);
        }

        protected override void ProduceItems(int producerIndex, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                long sequence = _ringBuffer.Next();
                try
                {
                    EventData item = _ringBuffer[sequence];
                    item.Data = ProduceItem(producerIndex, i, count);
                }
                finally
                {
                    _ringBuffer.Publish(sequence);
                }
            }
        }

        protected override void OnProductionCompleted()
        {
            foreach (IDisruptorConsumer consumer in _consumers)
            {
                consumer.Halt();
            }
        }

        protected override int ConsumeItems(int consumerIndex) =>
            _consumers[consumerIndex].Run();

        protected abstract IReadOnlyList<IDisruptorConsumer> CreateConsumers(
            RingBuffer<EventData> ringBuffer,
            int consumerCount);

        protected sealed class EventData
        {
            public int Data;
        }
    }
}