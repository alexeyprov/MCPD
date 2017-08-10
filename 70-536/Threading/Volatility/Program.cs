using System;
using System.Threading;

internal static class Program
{
	private static bool _stopFlag = false;	

	private static void Main()
	{
		Thread t = new Thread(Worker);
		t.Start();
		Thread.Sleep(2000);

		_stopFlag = true;
		t.Join();
	}

	private static void Worker()
	{
		int x = 0;
		while (!_stopFlag)
		{
			x++;
		}
		Console.WriteLine("Stopped at {0}.", x);
	}
}