using System;
using System.Threading;

public class WaitOnPool
{
	static void Main()
	{
		using (AutoResetEvent are = new AutoResetEvent(false))
		{
			RegisteredWaitHandle rwh = ThreadPool.RegisterWaitForSingleObject(
				are, //WaitHandle to wait for
				new WaitOrTimerCallback(ThreadPool_WaitFinished), //callback method
				null, //state to pass to callback
				5000, //timeout
				false); //wait once flag
			char c = 'X';
			do
			{
				Console.WriteLine("'S' = signal, 'Q' = quit: ");
				c = Console.ReadKey().KeyChar;
				Console.WriteLine();
				if ('S' == c)
				{
					are.Set();
					Thread.Sleep(100);
				}
			}
			while (c != 'Q');
			rwh.Unregister(null);
		}
	}

	static void ThreadPool_WaitFinished(object state, bool isTimeout)
	{
		if (isTimeout)
		{
			Console.WriteLine("Timeout while waiting for auto-reset event");
		}
		else
		{
			Console.WriteLine("Auto-reset event became signaled");
		}
	}
}