using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

class DataReader
{
	[STAThread]
	private static void Main()
	{
		//try reading public data from a file
		GetFileData("public.dat");

		//try reading private data from a file
		GetFileData("private.dat");

		//try reading public memory data from clipboard
		GetMemoryData();
		
		//try reading private memory data from clipboard
		GetMemoryData();
	}

	private static void GetFileData(string fileName)
	{
		byte[] data = File.ReadAllBytes(fileName);

		try
		{
			byte[] pt = ProtectedData.Unprotect(data, null,
				DataProtectionScope.LocalMachine);

			Console.WriteLine("Decoded contents of {0}:{2}{1}{2}",
				fileName,
				Encoding.ASCII.GetString(pt),
				Environment.NewLine);
		}
		catch (CryptographicException)
		{
			Console.WriteLine("Error while decoding {0}", fileName);
		}
	}

	private static void GetMemoryData()
	{
		if (Clipboard.ContainsText())
		{
			byte[] data = Convert.FromBase64String(Clipboard.GetText());
			
			ProtectedMemory.Unprotect(data, MemoryProtectionScope.CrossProcess);
		
			Console.WriteLine("Decoded memory contents:{1}{0}{1}" + 
				"Press RETURN to continue",
				Encoding.ASCII.GetString(data),
				Environment.NewLine);
			Console.ReadLine();
		}
		else
		{
			Console.WriteLine("No data in clipboard");
		}
	}
}