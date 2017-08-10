using System;

public class App
{
	static void DisplayValue(int? i)
	{
		if (i.HasValue)
		{
			Console.WriteLine("Integer = {0}", i);
		}
		else
		{
			Console.WriteLine("Integer is null");
		}

		try
		{
			Console.WriteLine("Accessing value succeeded: value = {0}", i.Value);
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine("Accessing value failed: {0}", ex.Message);
		}
	}

	static void Main()
	{
		DisplayValue(7);
		DisplayValue(null);
	}
}