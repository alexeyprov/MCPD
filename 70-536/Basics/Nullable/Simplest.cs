using System;

public class App
{
	static void Main()
	{
		DateTime? dt = null;
		Console.WriteLine("Default date value is {0}", dt ?? default(DateTime));
	}
}