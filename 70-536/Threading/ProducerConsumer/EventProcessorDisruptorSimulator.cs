using System;
using System.Collections.Generic;
using System.Linq;

using Disruptor;

namespace ProducerConsumer
{
    internal sealed class EventProcessorDisruptorSimulator : DisruptorSimulatorBase
    {
        protected override IReadOnlyList<IDisruptorConsumer> CreateConsumers(
            RingBuffer<EventData> ringBuffer,
            int consumerCount)
        {
            ISequenceBarrier barrier = ringBuffer.NewBarrier();

            IReadOnlyList<IDisruptorConsumer> consumers = Enumerable.Range(0, consumerCount)
                .Select(i => new Consumer(i, consumerCount, ringBuffer, barrier))
                .ToArray();
            ISequence[] gatingSequences = consumers
                .Select(c => c.EventProcessor.Sequence)
                .ToArray();
            ringBuffer.AddGatingSequences(gatingSequences);

            return consumers;
        }

        private sealed class Consumer : IEventHandler<EventData>, IDisruptorConsumer
        {
            private readonly int _index;
            private readonly long _modulo;
            private int _consumedCount;

            public Consumer(
                int index,
                int modulo,
                RingBuffer<EventData> ringBuffer,
                ISequenceBarrier barrier)
            {
                _index = index;
                _modulo = modulo;
                _consumedCount = 0;

                EventProcessor = new BatchEventProcessor<EventData>(
                    ringBuffer,
                    barrier,
                    this);
            }

            public IEventProcessor EventProcessor { get; }

            public int Run()
            {
                EventProcessor.Run();
                return _consumedCount;
            }

            public void Halt() => EventProcessor.Halt();

            void IEventHandler<EventData>.OnEvent(EventData data, long sequence, bool endOfBatch)
            {
                if (sequence % _modulo == _index)
                {
                    ConsumeItem(_index, data.Data);
                    _consumedCount++;
                }
            }
        }
    }
}
