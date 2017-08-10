using System;
using System.Threading;

class BadNews
{
	static byte[] _data;
	static ReaderWriterLock _rwl;
	static Random _rnd;
	const int DATA_SIZE = 100;
	const int CHECKSUM = 5050;
	

	static BadNews()
	{
		_data = new byte[DATA_SIZE];
		for (int i = 0; i < DATA_SIZE; i++)
		{
			_data[i] = (byte) (i + 1);
		}
		_rwl = new ReaderWriterLock();
		_rnd = new Random();
	}

	static void Main()
	{
		Thread t;
		//start writer
		t = new Thread(WriterFnc);
		t.IsBackground = true;
		t.Start();
		
		//start readers
		for (int i = 0; i < 10; i++)
		{
			t = new Thread(ReaderFnc);
			t.IsBackground = true;
			t.Name = i.ToString();
			t.Start();		
		}
		_rwl.AcquireReaderLock(Timeout.Infinite);
		LockCookie lc = _rwl.ReleaseLock();

		//wait for 10 seconds and exit
		Thread.Sleep(10000);
		_rwl.RestoreLock(ref lc);
		try
		{
			Console.WriteLine("{0} permutations have been made for 10 seconds.", _rwl.WriterSeqNum);
			Console.WriteLine("Resulting buffer: " + BitConverter.ToString(_data));
		}
		finally
		{
			_rwl.ReleaseReaderLock();
		}
	}

	static void ReaderFnc()
	{
		for (int i = 0; i < 100000; i++)
		{
			try
			{
				_rwl.AcquireReaderLock(Timeout.Infinite);
				int sum = 0;
				for (int j = 0; j < DATA_SIZE; j++)
				{
					sum += _data[j];
				}
				if (sum != CHECKSUM)
				{
					Console.WriteLine("Reader thread {0} " +
						"detects data corruption at iteration {1}",
						Thread.CurrentThread.Name,
						i);
				}
			}
			finally
			{
				_rwl.ReleaseReaderLock();
			}
			//Thread.Sleep(100); //Give a chance for the writer
		}
	}

	static void WriterFnc()
	{
		for (int i = 0; i < 100000; i++)
		{
			int k = _rnd.Next(DATA_SIZE);
			int l = _rnd.Next(DATA_SIZE);
			if (k == l)
			{
				continue;
			}

			try
			{
				_rwl.AcquireWriterLock(Timeout.Infinite);
				byte tmp = _data[k];
				_data[k] = _data[l];
				_data[l] = tmp;
			}
			finally
			{
				_rwl.ReleaseWriterLock();
			}
			//Console.WriteLine("Writer completed iteration {0}", i);
		}
	}
}