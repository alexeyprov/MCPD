using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

internal static class Program
{
    private static void Main()
	{
		Count();
		MaxPrime();
	}

	private static void Count()
	{
		Console.WriteLine("Hit <ENTER> to stop");

		CancellationTokenSource tokens = new CancellationTokenSource();
		CancellationToken token = tokens.Token;
		token.Register(() => Console.WriteLine("stopped"));

		new Task(() => DoCount(1000, token)).Start();

		Console.ReadLine();
		tokens.CancelAfter(500); //let 2 more numbers to be written

		Console.ReadLine();
	}

	private static void MaxPrime()
	{
		const int PRIME_THRESHOLD = 10000;

		Console.WriteLine("Hit <ENTER> to stop");

		CancellationTokenSource tokens = new CancellationTokenSource();
		CancellationToken token = tokens.Token;

		Task<int> t = new Task<int>(() => DoFindMaxPrime(PRIME_THRESHOLD, token), token);

		t.Start();

		Console.ReadLine();
		tokens.Cancel();

		try
		{
			Console.WriteLine("Maximum prime less than {0} is: {1}", PRIME_THRESHOLD, t.Result);	
		}
		catch (AggregateException ex)
		{
			ex.Handle(e => e is OperationCanceledException);
			Console.WriteLine("Operation was cancelled");
		}

	}

	private static void DoCount(int threshold, CancellationToken token)
	{
		for (int i = 0; i < threshold && !token.IsCancellationRequested; ++i)
		{
			Console.Write(i + " ");
			Thread.Sleep(200);
		}
	}

	private static int DoFindMaxPrime(int threshold, CancellationToken token)
	{
		if (threshold <= 3)
		{
			return threshold;
		}

		for (int i = threshold % 2 == 1 ? threshold : threshold - 1; i > 3; i -= 2)
		{
			int r = (int)Math.Sqrt(i);
			int n = r % 2 == 1 ? r / 2 : (r / 2 - 1);

			BitArray divisors = new BitArray(n, true);
			bool isPrime = true;

			for (int j = 0; j < n; ++j)
			{
				token.ThrowIfCancellationRequested();

				if (!divisors[j])
				{
					continue;
				}

				int d = 3 + j * 2;

				if (0 == (i % d))
				{
					isPrime = false;
					break;
				}

				for (int k = d + j; k < n; k += d)
				{
					divisors[k] = false;
				}

				Thread.Sleep(100);
			}

			if (isPrime)
			{
				return i;
			}
		}

		return 3;
	}
}