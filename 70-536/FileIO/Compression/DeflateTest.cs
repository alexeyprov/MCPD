using System;
using System.IO;
using System.IO.Compression;

class DeflateTest
{
	static void Main(string[] args)
	{
		const string USAGE_TEXT = "Usage: DeflateTest <inputfilename>";

		//If no file name is specified, write usage text.
		if (args.Length != 1)
		{
			Console.WriteLine(USAGE_TEXT);
		}
		else
		{
			if (File.Exists(args[0]))
			{
				TestCompression(args[0], false); //DeflateStream
				TestCompression(args[0], true); //GZipStream
			}
		}
	}

	static void TestCompression(string fileName, bool isGzip)
	{
		MemoryStream ms = new MemoryStream();
		byte[] data = File.ReadAllBytes(fileName);
		Type t;

		using (Stream zip = GetStream(ms, true, isGzip))
		{
			t = zip.GetType();
			Console.WriteLine("Compression using {0}", t);
			zip.Write(data, 0, data.Length);
		}
		int comprSize = (int) ms.Length;
		Console.WriteLine("Compressed from {0} to {1} bytes. Ratio: {2:P}",
			data.Length, comprSize, (double) comprSize / data.Length);
		
		ms.Position = 0;
		byte[] unpackedData = new byte[data.Length + 100];		
		int bytesRead;

		using (Stream unzip = GetStream(ms, false, isGzip))
		{
			Console.WriteLine("Decompression using {0}", t);
			bytesRead = ReadAllBytesFromStream(unzip, unpackedData);
		}
		Console.WriteLine("Decompressed from {0} to {1} bytes",
			comprSize, bytesRead);

		Array.Resize(ref unpackedData, bytesRead);
		Console.WriteLine("Data integrity check {0} for {1}", 
			(Compare(data, unpackedData)) ? "OK" : "FAILED",
			t);
	}

	static Stream GetStream(Stream baseStream, bool isCompress, bool isGzip)
	{
		CompressionMode mode = (isCompress) ? CompressionMode.Compress :
			CompressionMode.Decompress;

		if (isGzip)
		{
			return new GZipStream(baseStream, mode, isCompress);
		}

		return new DeflateStream(baseStream, mode, isCompress);
			
	}

	static int ReadAllBytesFromStream(Stream s, byte[] buffer)
	{
		const int CHUNK_SIZE = 100;
		int bytesRead = s.Read(buffer, 0, CHUNK_SIZE);
        int offset = bytesRead;

		while (bytesRead != 0)
		{
			bytesRead = s.Read(buffer, offset, CHUNK_SIZE);
            offset += bytesRead;
		}
		return offset;
	}

	static bool Compare(byte[] buf1, byte[] buf2)
	{
		int len = buf1.Length;
		if (len != buf2.Length) 
		{ 
			return false;
		}

		for (int i= 0; i < len; ++i) 
		{
			if (buf1[i] != buf2[i]) 
			{
				return false;
			}
		}
		return true;
	}
}
