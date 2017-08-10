using System;
using System.Diagnostics;

class ListModules
{
	private static void Main()
	{
		Process p = Process.GetCurrentProcess();
		Console.WriteLine("Dumping modules for {0}:{1}", p.ProcessName, 
			Environment.NewLine);
		foreach (ProcessModule m in p.Modules)
		{
			Console.WriteLine("{0,8:X}: {1}", m.BaseAddress.ToInt32(), 
				m.ModuleName);
		}
	}
}