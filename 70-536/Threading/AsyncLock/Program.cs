using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

internal static class Program
{
    private static void Main()
    {
        AsyncOneManyLock asyncLock = new AsyncOneManyLock();

        Debugger.Launch();

        TestLock(asyncLock);

        Console.ReadLine();
    }

    private static void TestLock(AsyncOneManyLock asyncLock)
    {
        string data = "(uninitialized)";
        Random rnd = new Random();
        
        Parallel.For(
            0,
            20,
            async taskNo =>
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;
                OneManyMode mode = (taskNo % 7 == 0) ? OneManyMode.Exclusive : OneManyMode.Shared;
                Console.WriteLine($"{threadId}>> Task {taskNo} started in {mode} mode");

                if (taskNo > 0)
                {
                    // give priority to the first writer
                    Thread.Sleep(rnd.Next(100));
                }

                await asyncLock.WaitAsync(mode);

                Thread.Sleep(rnd.Next(100));
                
                if (mode == OneManyMode.Exclusive)
                {
                    Volatile.Write(ref data, $"Data_{taskNo}");
                    Console.WriteLine($"{threadId}>> Data set to '{data}' by task no {taskNo}.");
                }
                else
                {
                    Console.WriteLine($"{threadId}>> Data read as '{Volatile.Read(ref data)}' by task no {taskNo}.");
                }

                asyncLock.Release();
            });
    }
}