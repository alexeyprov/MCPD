using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disruptor;

namespace ProducerConsumer
{
    internal sealed class WorkProcessorDisruptorSimulator : DisruptorSimulatorBase
    {
        protected override IReadOnlyList<IDisruptorConsumer> CreateConsumers(RingBuffer<EventData> ringBuffer, int consumerCount)
        {
            ISequenceBarrier barrier = ringBuffer.NewBarrier();
            long cursor = ringBuffer.Cursor;
            ISequence workSequence = new Sequence(cursor);

            IReadOnlyList<IDisruptorConsumer> consumers = Enumerable.Range(0, consumerCount)
                .Select(i => new Consumer(i, ringBuffer, barrier, workSequence))
                .ToArray();
            ISequence[] gatingSequences = consumers
                .Select(c => c.EventProcessor.Sequence)
                .Concat(new[] { workSequence })
                .ToArray();
            ringBuffer.AddGatingSequences(gatingSequences);

            return consumers;
        }

        private sealed class Consumer : IWorkHandler<EventData>, IDisruptorConsumer
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
