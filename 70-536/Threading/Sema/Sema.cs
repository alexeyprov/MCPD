using System;
using System.Threading;

static class Sema
{
    private static Semaphore _sema;
    private const int MaxThreads = 5;
    private const int SemaThreads = 2;

    static void Main()
    {
        // Create semaphore with 0 out of SemaThreads threads
        // that can run initially
        _sema = new Semaphore(0, SemaThreads); 

        // Run all the threads
        Thread[] threads = new Thread[MaxThreads];
        for (int i = 0; i < MaxThreads; i++)
        {
            threads[i] = new Thread(ThreadMethod);
            //threads[i].IsBackground = true;
            threads[i].Start(i);
        }

        Console.WriteLine("Press ENTER to exit" + Environment.NewLine);
        _sema.Release(SemaThreads); // release all the semaphore slots
        Console.ReadLine();

        // Clean up
        Array.ForEach(threads, t =>
            {
                t.Abort();
                t.Join();
            });
        _sema.Close();
    }

    static void ThreadMethod(object state)
    {
        Random rnd = new Random();
        int k = 1;
        string padding = new String(' ', ((int) state) << 2);

        while (true)
        {
            if (_sema.WaitOne(100, false))
            {
                Console.WriteLine(padding + ">>> Thread {0} >>>",
                    Thread.CurrentThread.ManagedThreadId);
                try
                {
                    for (int i = 0; i < 5; i++)
                    {
                        ProtectedAction(padding, k + i);
                    }
                    k += 5;
                    Console.WriteLine(padding + "<<< Thread {0} <<<",
                        Thread.CurrentThread.ManagedThreadId);
                }
                finally
                {
                    _sema.Release();
                }
            }
            Thread.Sleep(rnd.Next(1000));
        }   
    }

    static void ProtectedAction(string padding, int it)
    {
        Console.WriteLine(padding + "Thread {0} at iteration {1}",
            Thread.CurrentThread.ManagedThreadId, it);
        Thread.Sleep(100);
    }
}