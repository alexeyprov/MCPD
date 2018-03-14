using Disruptor;

namespace ProducerConsumer
{
    internal interface IDisruptorConsumer
    {
        IEventProcessor EventProcessor { get; }

        void Halt();

        int Run();
    }
}