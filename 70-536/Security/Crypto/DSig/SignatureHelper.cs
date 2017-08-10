using System;
using System.IO;
using System.Security.Cryptography;

public static class SignatureHelper
{
	public const string HashAlgorithm = "SHA1";
	private const string PUBLIC_KEY_FILE = "ComputeSignature.key";
	
	public static void SavePublicKey(AsymmetricAlgorithm alg)
	{
		if (!File.Exists(PUBLIC_KEY_FILE))
		{
			string xml = alg.ToXmlString(false);
			File.WriteAllText(PUBLIC_KEY_FILE, xml);
		}
	}

	public static void LoadPublicKey(AsymmetricAlgorithm alg)
	{
		string publicKey = File.ReadAllText(PUBLIC_KEY_FILE);
		alg.FromXmlString(publicKey);
	}
}