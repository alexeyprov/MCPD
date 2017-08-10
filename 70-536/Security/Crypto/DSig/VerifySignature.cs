using System;
using System.IO;
using System.Security.Cryptography;

class VerifySignature
{
	public static void Main(string[] args)
	{
		if (args.Length != 2)
		{
			PrintUsageInfo();
			return;
		}

		Console.WriteLine("Verification {0}", 
			(VerifyFileSignature(args[0], args[1])) ?
			"succeeded" : "failed");
	}

	private static void PrintUsageInfo()
	{
		Console.WriteLine("Usage: VerifySignature.exe <filename> <signaturefile>");
	}

	private static bool VerifyFileSignature(string filename, string signaturefile)
	{
		byte[] data = File.ReadAllBytes(filename);
		byte[] signature = File.ReadAllBytes(signaturefile);
	
		using (RSACryptoServiceProvider verifier = GetVerifier())
		{
			return verifier.VerifyData(data, 
				SignatureHelper.HashAlgorithm,
				signature);
		}
	}

	private static RSACryptoServiceProvider GetVerifier()
	{
		RSACryptoServiceProvider verifier = new RSACryptoServiceProvider();
		SignatureHelper.LoadPublicKey(verifier);
		return verifier;
	}
}