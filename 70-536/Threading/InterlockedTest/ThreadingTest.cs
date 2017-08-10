using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace InterlockedTest
{
	class ThreadingTest
	{
		static ProtectedResource _res = new ProtectedResource();

		public static void Run()
		{
			ThreadPool.QueueUserWorkItem(TestMethod);
		}

		static void TestMethod(object state)
		{
			Random r = new Random();
			while (true)
			{
				for (int i = 0; i < 100; i++)
				{
					_res.CheckData();
					Thread.Sleep(0);
				}
				Thread.Sleep(r.Next(1000));
				double a = r.NextDouble() * 1000.0;
				double b = r.NextDouble() * 1000.0;
				_res.SetData(a, b);
				Console.WriteLine("Thread {0} sets data to ({1}, {2})",
					Thread.CurrentThread.ManagedThreadId, a, b);
			}
		}
	}
}
