using System;
using System.IO;
using System.Threading;
using System.Text;

namespace AsyncRead
{
	class Program
	{
		const string TEST_FILE = "AsyncRead.exe";

		static void Main(string[] args)
		{
			DumpThread("Main");
			TestAsyncDelegate();
			ThreadPoolTest();
			ThreadTest();
	
			Console.WriteLine("Hit ENTER to exit");
			Console.ReadLine();
		}

		static void TestAsyncDelegate()
		{
			FileStream fs = new FileStream(TEST_FILE, FileMode.Open,
				FileAccess.Read, FileShare.Read, 1024, true);

			byte[] data = new byte[100];

			fs.BeginRead(data, 0, data.Length,
				delegate(IAsyncResult ar)
				{
					DumpThread("AsyncCallback");
					int bytesRead = fs.EndRead(ar);
					fs.Close();
					PrintData(data, bytesRead);
				},
				null);
		}

		static void TestCallback(object threadName)
		{
			DumpThread((string) threadName);
			using (FileStream fs = new FileStream(TEST_FILE, FileMode.Open, FileAccess.Read))
			{
				byte[] data = new byte[100];
				int bytesRead = fs.Read(data, 0, data.Length);
				PrintData(data, bytesRead);
			}
		}

		static void ThreadPoolTest()
		{
			ThreadPool.QueueUserWorkItem(TestCallback, "ThreadPoolTest");
		}

		static void ThreadTest()
		{
			Thread t = new Thread(TestCallback);
			t.SetApartmentState(ApartmentState.STA);
			t.Start("ThreadTest");
		}

		static void PrintData(byte[] data, int bytesRead)
		{
			Array.Resize(ref data, bytesRead);
			Console.WriteLine("Data read: " + BitConverter.ToString(data));
		}

		static void DumpThread(string desc)
		{
			Thread t = Thread.CurrentThread;

			Console.WriteLine("{0} thread >>> ID = {1}, IsThreadPool = {2}, " +
				"IsBackground = {3}, Apartament = {4}",
				desc, t.ManagedThreadId, t.IsThreadPoolThread,
				t.IsBackground, t.GetApartmentState());
		}
	}
}
