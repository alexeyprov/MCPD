using System;

class VerifyHash
{
	public static void Main(string[] args)
	{
		if (args.Length != 2)
		{
			PrintUsageInfo();
			return;
		}

		string hash = HashHelper.GetFileHash(args[0]);
		Console.WriteLine("Verification {0}", (args[1] == hash) ?
			"succeeded" : "failed");
	}

	private static void PrintUsageInfo()
	{
		Console.WriteLine("Usage: VerifyHash.exe <filename> <base64hash>");
	}
}