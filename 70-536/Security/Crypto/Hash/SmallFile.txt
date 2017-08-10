using System;

class ComputeHash
{
	public static void Main(string[] args)
	{
		if (args.Length != 1)
		{
			PrintUsageInfo();
			return;
		}

		Console.WriteLine(HashHelper.GetFileHash(args[0]));
	}

	private static void PrintUsageInfo()
	{
		Console.WriteLine("Usage: ComputeHash.exe <filename>");
	}
}