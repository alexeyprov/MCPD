using System.Collections.Generic;
using System.Linq;

using Disruptor;

namespace ProducerConsumer
{
    internal sealed class DisruptorSimulator : SimulatorBase
    {
        private RingBuffer<EventData> _ringBuffer;
        private IReadOnlyList<Consumer> _consumers;

        public override PerformanceStats Run(
            int producerCount, 
            int consumerCount, 
            int boundedCapacity, 
            int itemCount)
        {
            _ringBuffer = producerCount == 1 ? 
                RingBuffer<EventData>.CreateSingleProducer(
                    () => new EventData(),
                    boundedCapacity) :
                RingBuffer<EventData>.CreateMultiProducer(
                    () => new EventData(),
                    boundedCapacity);
            ISequenceBarrier barrier = _ringBuffer.NewBarrier();
            long cursor = _ringBuffer.Cursor;
            ISequence workSequence = new Sequence(cursor);

            _consumers = Enumerable.Range(0, consumerCount)
                .Select(i => new Consumer(i, _ringBuffer, barrier, workSequence))
                .ToArray();
            ISequence[] gatingSequences = _consumers
                .Select(c => c.EventProcessor.Sequence)
                .Concat(new[] { workSequence })
                .ToArray();
            _ringBuffer.AddGatingSequences(gatingSequences);

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
            foreach (Consumer consumer in _consumers)
            {
                consumer.Halt();
            }
        }

        protected override int ConsumeItems(int consumerIndex) =>
            _consumers[consumerIndex].Run();

        private sealed class EventData
        {
            public int Data;
        }

        private sealed class Consumer : IWorkHandler<EventData>
        {
            private readonly int _index;
            private int _consumedCount;

            public Consumer(
                int index,
                RingBuffer<EventData> ringBuffer,
                ISequenceBarrier barrier,
                ISequence workSequence)
            {
                _index = index;
                _consumedCount = 0;

                EventProcessor = new WorkProcessor<EventData>(
                    ringBuffer,
                    barrier,
                    this,
                    new FatalExceptionHandler(),
                    workSequence);
                EventProcessor.Sequence.SetValue(workSequence.Value);
            }

            public IEventProcessor EventProcessor { get; }

            public int Run()
            {
                EventProcessor.Run();
                return _consumedCount;
            }

            public void Halt() => EventProcessor.Halt();

            void IWorkHandler<EventData>.OnEvent(EventData data)
            {
                ConsumeItem(_index, data.Data);
                _consumedCount++;
            }
        }
    }
}