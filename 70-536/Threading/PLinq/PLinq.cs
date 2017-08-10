using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

internal static class Program
{
	private static void Main()
	{
		Debugger.Launch();

		Assembly a = Assembly.ReflectionOnlyLoad("mscorlib");

		ParallelQuery<string> query = 
			from t in a.GetExportedTypes().AsParallel()
			from m in t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
			where FilterMember(m)
			orderby t.FullName, m.Name
			select string.Format("{0}.{1}", t.FullName, m.Name);

		foreach (string s in query)
		{
			Console.WriteLine(s);
		}
	}

	private static bool FilterMember(MethodInfo m)
	{
		return Attribute.IsDefined(m, typeof(ObsoleteAttribute));
	}
}
