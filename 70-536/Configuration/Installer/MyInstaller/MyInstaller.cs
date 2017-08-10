using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

[RunInstaller(true)]
public class MyInstaller : Installer
{
	private const string RESPONSE_PARAM = "response";

	public MyInstaller()
	{
		this.Committing += new InstallEventHandler(Installer_Committing);
		this.Committed += new InstallEventHandler(Installer_Committed);
	}

	public override void Install(IDictionary savedState)
	{
		base.Install(savedState);
		Console.Write("Abort (y/n)? ");
		char c = Char.ToUpper(Convert.ToChar(Console.Read()));

		savedState[RESPONSE_PARAM] = c;
		if ('Y' == c)
		{
			throw new Exception("Aborted");
		}
	}

	public override void Rollback(IDictionary savedState)
	{
		base.Rollback(savedState);
		Console.WriteLine(">>> Aborting (response = {0})", savedState[RESPONSE_PARAM]);
	}

	private void Installer_Committing(object sender, InstallEventArgs e)
	{
		Console.WriteLine(">>> Commiting with {0} params", e.SavedState.Count);
	}
	
	private void Installer_Committed(object sender, InstallEventArgs e)
	{
		Console.WriteLine(">>> Committed");
	}

	// Entry Point
	private static void Main()
	{
	}
}