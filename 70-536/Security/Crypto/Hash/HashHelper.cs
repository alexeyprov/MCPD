using System;
using System.IO;
using System.Security.Cryptography;

public static class HashHelper
{
	public static string GetFileHash(string fileName)
	{
		const int SIZE_THRESHOLD = 0x10000; //64 KB

		using (HashAlgorithm hasher = new SHA512Managed())
		{
			FileInfo fi = new FileInfo(fileName);
			byte[] result = null;

			if (fi.Length > SIZE_THRESHOLD)
			{
				result = GetStreamHash(hasher, fi.OpenRead());
			}
			else
			{
				result = GetArrayHash(hasher, File.ReadAllBytes(fileName));	
			}

			return Convert.ToBase64String(result);
		}
	}

	private static byte[] GetStreamHash(HashAlgorithm hasher, Stream stm)
	{
		const int CHUNK_SIZE = 0x100; //256 B
		using (stm)
		{
			byte[] buffer = new byte[CHUNK_SIZE];
			int bytesRead = stm.Read(buffer, 0, CHUNK_SIZE);

			while (CHUNK_SIZE == bytesRead)
			{
				hasher.TransformBlock(buffer, 0, CHUNK_SIZE,
					buffer, 0);

				bytesRead = stm.Read(buffer, 0, CHUNK_SIZE);
			}

			hasher.TransformFinalBlock(buffer, 0, bytesRead);
			return hasher.Hash;
		}
	}

	private static byte[] GetArrayHash(HashAlgorithm hasher, byte[] data)
	{
		hasher.ComputeHash(data);
		return hasher.Hash;	
	}
}