using System;
using System.Threading;
using System.Threading.Tasks;

internal static class Program
{
    private static int _flag;
    private static EventWaitHandle _evt;

    static Program()
    {
        _evt = new AutoResetEvent(false);
    }

    private static void Main()
    {
        Parallel.For(0, 4, i => TestMethod());
        Console.ReadLine();
    }

    private static void TestMethod()
    {
        if (0 == Interlocked.CompareExchange(ref _flag, 1, 0))
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running a request");
            Thread.Sleep(5000);
            Interlocked.CompareExchange(ref _flag, 0, 1);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} completed a request");
            //_evt.Set();
        }
        else
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is waiting for completion");
            while (Volatile.Read(ref _flag) != 0)
            {
                Thread.Sleep(200);
            }
            //_evt.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} finished waiting");
        }
    }
}