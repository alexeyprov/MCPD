using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

internal static class Program
{
    private static void Main()
    {
        Debugger.Launch();

        Setup();

        Go();

        Console.ReadLine();
    }

    private static async void Setup()
    {
        var awaiter = new EventAwaiter<FirstChanceExceptionEventArgs>();

        AppDomain.CurrentDomain.FirstChanceException += awaiter.HandleEvent;

        while (true)
        {
            Console.WriteLine(
                "First-chance exception: {0}",
                (await awaiter).Exception.GetType());
        }
    }

    private static void Go()
    {
        ThrowAndCatch<InvalidOperationException>();
        ThrowAndCatch<ArgumentNullException>();
        ThrowAndCatch<NullReferenceException>();    
    }

    private static void ThrowAndCatch<TException>() where TException : Exception, new()
    {
        try
        {
            throw new TException();
        }
        catch
        {
        }
    }
        
}