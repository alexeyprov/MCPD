using System;
using System.Threading;
using System.Threading.Tasks;

internal class Program
{
    private readonly ResourceCache _cache;

    private Program()
    {
        _cache = new ResourceCache();
    }

    private static void Main()
    {
        Program p = new Program();
        Parallel.For(
            0,
            5,
            taskId => Parallel.For(
                0,
                16,
                subTaskId => p.AccessResource(taskId, subTaskId)));
    }

    private void AccessResource(int key, int subKey)
    {
        string resource = _cache[key].GetData(subKey);
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}>> ({key}, {subKey}): read '{resource}'");
    }
}