using System;
using System.Collections.Generic;
using System.Text;

namespace InterlockedTest
{
	class Program
	{
		static void Main(string[] args)
		{
			for (int i = 0; i < 10; i++)
			{
				ThreadingTest.Run();
			}
			Console.WriteLine("Hit ENTER to stop tests");
			Console.ReadLine();
		}
	}
}
