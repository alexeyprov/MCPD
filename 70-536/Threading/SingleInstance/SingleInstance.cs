using System;
using System.Threading;

class SingleInstance
{
	const string MUTEX_NAME = "SINGLEINSTANCE";

	static void Main()
	{
		Mutex m = CheckRunning();
		if (m != null)
		{
			m.Close();
			Console.WriteLine("The application is already running");
			return;
		}
		else
		{
			m = new Mutex(true, MUTEX_NAME);
		}

		Console.WriteLine("Press ENTER to exit");
		Console.ReadLine();
		m.ReleaseMutex();
		m.Close();
	}

	static Mutex CheckRunning()
	{
		try
		{
			return Mutex.OpenExisting(MUTEX_NAME);	
		}
		catch (WaitHandleCannotBeOpenedException)
		{
			return null;
		}
	}
}