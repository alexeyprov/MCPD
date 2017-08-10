using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

class DataWriter
{
	private const string PUBLIC_DATA = "This is public. ";
	private const string PRIVATE_DATA = "This is private.";

	[STAThread]
	private static void Main()
	{
		//set public data in file to be read
		//by any user
		SetFileData(PUBLIC_DATA, "public.dat",
			DataProtectionScope.LocalMachine);

		//set private data in file to be read
		//by the same user only
		SetFileData(PRIVATE_DATA, "private.dat",
			DataProtectionScope.CurrentUser);
		
		//set public data in memory to be read 
		//by any process
		SetMemoryDataAndWait(PUBLIC_DATA, 
			MemoryProtectionScope.CrossProcess);

		//set private data in memory to be read
		//by current process only
		SetMemoryDataAndWait(PRIVATE_DATA, 
			MemoryProtectionScope.SameProcess);
	}

	private static void SetMemoryDataAndWait(string s, MemoryProtectionScope scope)
	{
		byte[] data = Encoding.ASCII.GetBytes(s);

		ProtectedMemory.Protect(data, scope);
		string ct = Convert.ToBase64String(data);

		ProtectedMemory.Unprotect(data, scope);

		if (Encoding.ASCII.GetString(data) != s)
		{
			throw new ArgumentException("s");
		}

		Clipboard.SetText(ct);		

		Console.WriteLine("[{0}] copied to clipboard. Press RETURN to continue.", s);
		Console.ReadLine();
	}

	private static void SetFileData(string s, string fileName, DataProtectionScope scope)
	{
		byte[] data = Encoding.ASCII.GetBytes(s);
		byte[] ct = ProtectedData.Protect(data, null, scope);

		byte[] dataCopy = ProtectedData.Unprotect(ct, null, scope);
		if (Encoding.ASCII.GetString(dataCopy) != s)
		{
			throw new ArgumentException("s");
		}

		File.WriteAllBytes(fileName, ct);
	}
}