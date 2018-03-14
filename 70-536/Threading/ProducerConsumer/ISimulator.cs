namespace ProducerConsumer
{
    internal interface ISimulator
    {
        PerformanceStats Run(
            int producerCount, 
            int consumerCount, 
            int boundedCapacity, 
            int itemCount);
    }
}