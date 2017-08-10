using System;
using System.Security;
using System.Security.Policy;

class NewDomainTest
{
	static void Main()
	{
		Console.WriteLine("[{0}] appdomain",
			AppDomain.CurrentDomain.FriendlyName);

		AppDomainSetup ds = new AppDomainSetup();
		ds.DisallowCodeDownload = true;
		// This stuff doesn't work
		// ds.PrivateBinPath = @"..\ReadBootIni";

		// But this does!
		ds.ApplicationBase = @"..\ReadBootIni";

		Evidence e = null;
		//e = new Evidence();
		//e.AddHost(new Zone(SecurityZone.Internet));

		AppDomain d = AppDomain.CreateDomain("Child Domain", e, ds);

		// Attach evidence
		try
		{
			// Execute by file name
			d.ExecuteAssembly(@"..\ReadBootIni\ReadBootIni.exe", e);
		}
		catch (SecurityException sex)
		{
			Console.WriteLine(sex);
		}

		// Execute by reference
		d.ExecuteAssemblyByName("ReadBootIni");

		AppDomain.Unload(d);
	}	
}