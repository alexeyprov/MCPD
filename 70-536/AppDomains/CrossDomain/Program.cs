using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Serialization;

class Program
{
	static int _testCnt;

	static void Main()
	{
		Console.WriteLine("Default appdomain: [{0}]",
			AppDomain.CurrentDomain.FriendlyName);
		Assembly thisAssembly = Assembly.GetEntryAssembly();
		Console.WriteLine("Main assembly: {0}",
			thisAssembly.FullName);

		RunTest("Marshal-by-ref", thisAssembly, "MarshalByRefType");
		RunTest("Marshal-by-value", thisAssembly, "MarshalByValType");
		RunTest("Non-Marshalled", thisAssembly, "NonMarshalledType");
	}

	private static void RunTest(string testName, Assembly a, string typeName)
	{
		Console.WriteLine("{0}Demo #{1}: {2}",
			Environment.NewLine,
			++_testCnt,
			testName);

		AppDomain ad = AppDomain.CreateDomain(String.Format("AD # {0}", _testCnt));

		// AppDomain::CreateInstanceAndUnwrap will do it
		//ad.Load(a.GetName());
		ITestedType it = null;

		// Try creating object and calling its method
		try
		{			
			it = (ITestedType) ad.CreateInstanceAndUnwrap(a.FullName, typeName);
			Console.WriteLine("Class type {0}, Is proxy: {1}", 
				it.GetType(),
				RemotingServices.IsTransparentProxy(it));
			it.TestMethod(AppDomain.CurrentDomain.FriendlyName);
		}
		catch (SerializationException sex)
		{
			Console.WriteLine("Demo failed: " + sex.ToString());
		}
		finally
		{
			AppDomain.Unload(ad);
		}

		// Try accessing object in the unloaded domain
		if (it != null)
		{
			try
			{
				it.TestMethod(AppDomain.CurrentDomain.FriendlyName);
				Console.WriteLine("Successful call");
			}
			catch (AppDomainUnloadedException)
			{
				Console.WriteLine("Failed call");
			}
		}
	}
}