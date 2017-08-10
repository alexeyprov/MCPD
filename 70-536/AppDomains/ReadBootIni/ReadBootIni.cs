using System;
using System.IO;

public class ReadBootIni
{
	static void Main()
	{
		DumpAppDomain(AppDomain.CurrentDomain);
		using (StreamReader sr = new StreamReader(@"c:\boot.ini"))
		{
			string s;
			while ((s = sr.ReadLine()) != null)
			{
				Console.WriteLine(s);
			}
		}
	}
	
	static void DumpAppDomain(AppDomain d)
	{
		Console.WriteLine(">>> Output of boot.ini in the [{0}] appdomain", 
			AppDomain.CurrentDomain.FriendlyName);
		AppDomainSetup ds = d.SetupInformation;

		Console.WriteLine(">>> Application name = {0}", ds.ApplicationName);
		Console.WriteLine(">>> Application base path = {0}", ds.ApplicationBase);
		Console.WriteLine(">>> Disallow code download = {0}", ds.DisallowCodeDownload);
		Console.WriteLine(">>> Disallow binding redirects = {0}", ds.DisallowBindingRedirects);
	}
}