using System;
using System.Threading;

class ThreadStateTest
{
	class ThreadEvents : IDisposable
	{
		EventWaitHandle[] _evts = new EventWaitHandle[2]
		{
			new EventWaitHandle(false, EventResetMode.AutoReset),
			new AutoResetEvent(false) //These 2 lines are synoni\ms
		};

		public void Dispose()
		{
			foreach (WaitHandle wh in _evts)
			{
				wh.Close();
			}	
		}

		public EventWaitHandle this[int idx]
		{
			get
			{
				if (idx < 0 || idx > 1)
				{
					throw new IndexOutOfRangeException();
				}
				return _evts[idx];
			}
		}
	}

	static void Main()
	{
		Thread t = new Thread(new ParameterizedThreadStart(ThreadProc));
		using (ThreadEvents te = new ThreadEvents())
		{
			PrintThreadState(t); //Unstarted
			t.Start(te);
			Thread.Sleep(500);
			PrintThreadState(t); //Running
			te[0].WaitOne();
			PrintThreadState(t); //WaitSleepJoin
			te[1].Set();
			t.Abort();
			PrintThreadState(t); //AbortRequested
			t.Join();
			PrintThreadState(t); //Stopped
		}
	}
	
	static void ThreadProc(object o)
	{
		try
		{
			int sum = 0;
			for (int i = 0; i < 1000000000; i++)
			{
				sum += i;
			}
			ThreadEvents te = (ThreadEvents) o;
			WaitHandle.SignalAndWait(te[0], te[1]);
			for (int i = 0; i < 1000000000; i++)
			{
				sum += i;
			}
		}
		catch (ThreadAbortException)
		{
			Console.WriteLine("Secondary thread aborted.");
		}
	}	

	static void PrintThreadState(Thread t)
	{
		Console.WriteLine("Secondary thread state: {0}", t.ThreadState);
	}
}
