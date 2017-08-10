using System;
using System.IO;
using System.Security.Cryptography;

class ComputeSignature
{
	public static void Main(string[] args)
	{
		if (args.Length != 2)
		{
			PrintUsageInfo();
			return;
		}

		SignFile(args[0], args[1]);
		ExportPublicKey();
	}

	private static void PrintUsageInfo()
	{
		Console.WriteLine("Usage: ComputeSignature.exe <filename> <signaturefile>");
	}

	private static void SignFile(string fileName, string signatureFile)
	{
		byte[] data = File.ReadAllBytes(fileName);
		using (RSACryptoServiceProvider signer = GetSigner())
		{
			byte[] signature = signer.SignData(data, SignatureHelper.HashAlgorithm);
			File.WriteAllBytes(signatureFile, signature);
		}
	}

	private static RSACryptoServiceProvider GetSigner()
	{
		CspParameters pars = new CspParameters();
		pars.KeyContainerName = "BookSample_Signature";

		RSACryptoServiceProvider signer = new RSACryptoServiceProvider(pars);
		signer.PersistKeyInCsp = true;
		return signer;
	}

	private static void ExportPublicKey()
	{
		using (AsymmetricAlgorithm alg = GetSigner())
		{
			SignatureHelper.SavePublicKey(alg);
		}
	}
}
