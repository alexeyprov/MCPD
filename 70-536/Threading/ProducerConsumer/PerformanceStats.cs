using System;

namespace ProducerConsumer
{
    internal sealed class PerformanceStats
    {
        public int ConsumedCount { get; set; }

        public TimeSpan Elapsed { get; set; }

        public double Speed => ConsumedCount / Elapsed.TotalMilliseconds;
    }
}