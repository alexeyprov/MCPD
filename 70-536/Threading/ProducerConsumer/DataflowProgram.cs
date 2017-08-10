using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

internal static class Program
{
    private static void Main()
    {
        var block = new BufferBlock<byte[]>();

        IEnumerable<Task<int>> consumers = new Task<int>[]
        {
            ConsumeItemsAsync(block),
            ConsumeItemsAsync(block)
        };

        ProduceItems(block);
        block.Complete();

        Task.WaitAll(consumers.ToArray());

        Console.WriteLine($"{consumers.Sum(t => t.Result)} total bytes consumed"); 
    }

    private static void ProduceItems(ITargetBlock<byte[]> block)
    {
        Random rnd = new Random();
        int totalSize = 0;
        for (int i = 0; i < 100; ++i)
        {
            int size = rnd.Next(1024);
            byte[] data = new byte[size];
            rnd.NextBytes(data);

            block.Post(data);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} >> Produced {size} bytes of data");
            totalSize += size;
        }

        Console.WriteLine($"{totalSize} total bytes produced");
    }

    private static async Task<int> ConsumeItemsAsync(IReceivableSourceBlock<byte[]> block)
    {
        int total = 0;
        while (await block.OutputAvailableAsync())
        {
            byte[] data;
            if (block.TryReceive(out data))
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} >> Consumed {data.Length} bytes of data");
                total += data.Length;
            }
        }
        return total;
    }
}