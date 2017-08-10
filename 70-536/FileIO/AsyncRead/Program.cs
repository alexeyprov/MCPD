using System;
using System.IO;
using System.Threading;

namespace AsyncRead
{
    internal static class Program
    {
        private const string TEST_FILE = "AsyncRead.exe";

        private static void Main(string[] args)
        {
            DumpThreadInfo("Main");
            TestAsyncDelegate();
            ThreadPoolTest();
            ThreadTest();
            AsyncTest();

            Console.WriteLine("Hit ENTER to exit");
            Console.ReadLine();
        }

        private static void TestAsyncDelegate()
        {
            FileStream fs = new FileStream(TEST_FILE, FileMode.Open,
                FileAccess.Read, FileShare.Read, 1024, true);

            byte[] data = new byte[100];

            fs.BeginRead(data, 0, data.Length,
                delegate (IAsyncResult ar)
                {
                    DumpThreadInfo("AsyncCallback");
                    int bytesRead = fs.EndRead(ar);
                    fs.Close();
                    PrintData(data, bytesRead);
                },
                null);
        }

        private static void TestCallback(object threadName)
        {
            DumpThreadInfo((string)threadName);
            using (FileStream fs = new FileStream(TEST_FILE, FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[100];
                int bytesRead = fs.Read(data, 0, data.Length);
                PrintData(data, bytesRead);
            }
        }

        private static void ThreadPoolTest()
        {
            ThreadPool.QueueUserWorkItem(TestCallback, "ThreadPoolTest");
        }

        private static void ThreadTest()
        {
            Thread t = new Thread(TestCallback);
            t.SetApartmentState(ApartmentState.STA);
            t.Start("ThreadTest");
        }

        private static async void AsyncTest()
        {
            using (FileStream fs = new FileStream(TEST_FILE, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                byte[] data = new byte[100];
                int bytesRead = await fs.ReadAsync(data, 0, data.Length);

                DumpThreadInfo("Async");
                PrintData(data, bytesRead);
            }
        }

        private static void PrintData(byte[] data, int bytesRead)
        {
            Array.Resize(ref data, bytesRead);
            Console.WriteLine("Data read: " + BitConverter.ToString(data));
        }

        private static void DumpThreadInfo(string desc)
        {
            Thread t = Thread.CurrentThread;

            Console.WriteLine(
                $"{desc} thread >>> ID = {t.ManagedThreadId}, IsThreadPool = {t.IsThreadPoolThread}, " +
                $"IsBackground = {t.IsBackground}, Apartment = {t.GetApartmentState()}");
        }
    }
}
