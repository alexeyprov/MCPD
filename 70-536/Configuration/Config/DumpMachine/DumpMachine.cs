using System;
using System.Configuration;

class DumpMachine
{
	static void Main()
	{
		Configuration c = ConfigurationManager.OpenMachineConfiguration();
		DumpConfiguration(c);
	}

	static void DumpConfiguration(Configuration c)
	{
		Console.WriteLine(">>> Dumping \"{0}\" config file", 
			c.FilePath);
		DumpGroups(0, c.SectionGroups);
		DumpSections(0, c.Sections);
	}

	static void DumpGroups(int level, ConfigurationSectionGroupCollection sgs)
	{
		string padding = new System.String(' ', level << 2);
		foreach (ConfigurationSectionGroup sg in sgs)
		{
			Console.WriteLine("{0}{1} : {2}", 
				padding,
				sg.Name, 
				ExtractClassName(sg.Type));
			DumpGroups(level + 1, sg.SectionGroups);
			DumpSections(level + 1, sg.Sections);
		}
	}

	static void DumpSections(int level, ConfigurationSectionCollection ss)
	{
		string padding = new System.String(' ', level << 2);
		foreach (ConfigurationSection s in ss)
		{
			SectionInformation si = s.SectionInformation;
			Console.WriteLine("{0}{1} : {2} ({3})", 
				padding,
				si.Name, 
				ExtractClassName(si.Type),
				si.AllowExeDefinition);
		}		
	}

	static string ExtractClassName(string typeName)
	{
		string[] fullClassName = typeName.Split(',')[0].Split('.');
		return fullClassName[fullClassName.Length - 1];
	}
}