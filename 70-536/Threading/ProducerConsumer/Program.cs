using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

internal static class Program
{
    private static readonly BlockingCollection<int> _data;
    private static readonly CancellationTokenSource _cts;

    static Program()
    {
        _data = new BlockingCollection<int>(new ConcurrentQueue<int>());
        _cts = new CancellationTokenSource();
    }

    private static void Main()
    {
        Console.WriteLine("Press <ENTER> to exit");

        Task p = Task.Run(() => ProduceItems(), _cts.Token);
        Task c = Task.Run(() => ConsumeItems(), _cts.Token);

        Console.ReadLine();
        _cts.Cancel();

        try
        {
            Task.WaitAll(p, c);
        }
        catch (AggregateException ae)
        {
            ae.Handle(e => e is OperationCanceledException);
            Console.WriteLine("Cancelled");
        }
    }

    private static void ProduceItems()
    {
        CancellationToken ct = _cts.Token;
        for (int i = 0; i < 50; ++i)
        {
            _data.Add(i, ct);
            Console.WriteLine($"Produced item {i}");

            if (i % 3 == 0)
            {
                Thread.Sleep(1000);
            }

#if FORCE_ERROR
            if (i == 40)
            {
                throw new InvalidOperationException();
            }
#endif
        }
        _data.CompleteAdding();            
    }

    private static void ConsumeItems()
    {
        foreach (int item in _data.GetConsumingEnumerable(_cts.Token))
        {
            Console.WriteLine($"   Consumed item {item}");
        }
        Console.WriteLine("All data consumed");
    }
}