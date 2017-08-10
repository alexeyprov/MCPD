using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

internal static class ConcurrentQueuePerformance
{
    private const int ITERATIONS_COUNT = 10000000;
    private static readonly Queue<int> _lockedCollection;
    private static readonly ConcurrentQueue<int> _concurrentCollection;
    private static readonly object _lock;

    static ConcurrentQueuePerformance()
    {
        _lockedCollection = new Queue<int>();
        _concurrentCollection = new ConcurrentQueue<int>();
        _lock = new object();
    }
    
    private static void Main()
    {
        TimeSpan ts = RunTest(AddToLockedQueue);
        Console.WriteLine("Locked queue test took: " + ts);

        ts = RunTest(AddToConcurrentQueue);
        Console.WriteLine("Concurrent queue test took: " + ts);

        Console.WriteLine(
            "Resulting lists {0} equal",
            _lockedCollection.OrderBy(i => i)
                .SequenceEqual(
                    _concurrentCollection.OrderBy(i => i)) ? "are" : "are not"); 
    }

    private static TimeSpan RunTest(Action<int> iteration)
    {
        Stopwatch sw = Stopwatch.StartNew();

        Parallel.For(
            0,
            ITERATIONS_COUNT,
            delegate(int n)
            {
                int sum = 0;
                for (int i = 0; i < 100; ++i)
                {
                    sum += n;
                }
                iteration(sum);
            });

        sw.Stop();
        return sw.Elapsed;
    }

    private static void AddToLockedQueue(int n)
    {
        lock (_lock)
        {
            _lockedCollection.Enqueue(n);
        }
    }

    private static void AddToConcurrentQueue(int n)
    {
        _concurrentCollection.Enqueue(n);
    }
}