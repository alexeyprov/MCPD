using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

internal static class Program
{
	private static void Main()
	{
		ConcurrentStack<double> stack = new ConcurrentStack<double>();
		double[] data = BuildData(1000, 1.02);
		
		ParallelLoopResult plr = Parallel.For(
			0, 
			data.Length, 
			new ParallelOptions
			{
				MaxDegreeOfParallelism = 4
			},
			(i, pls) =>
		{
			Console.WriteLine("[{0}] iteration {1}", Thread.CurrentThread.ManagedThreadId, i);
			stack.Push(Math.Sqrt(data[i]));
			if (data[i] > 2)
			{
				Console.WriteLine("[{0}] BREAK", Thread.CurrentThread.ManagedThreadId);
				pls.Break();
			}	
		});

		Console.WriteLine("Stack size: {0}", stack.Count);
		if (!plr.IsCompleted && plr.LowestBreakIteration.HasValue)
		{
			Console.WriteLine("Stopped at iteration {0}", plr.LowestBreakIteration);
		}
	}

	private static double[] BuildData(int size, double increment)
	{
		double[] result = new double[size];
		double val = 1.0;

		for (int i = 0; i < size; ++i)
		{
			val *= increment;
			result[i] = val;
		}

		return result;			
	}
}