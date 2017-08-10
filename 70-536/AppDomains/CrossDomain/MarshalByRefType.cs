using System;
using System.Threading;

public class MarshalByRefType : 
	MarshalByRefObject,
	ITestedType
{
	DateTime _creationDate;

	public MarshalByRefType()
	{
		_creationDate = DateTime.Now;
		Console.WriteLine("{0} .ctor running in [{1}] at {2:G}",
			this.GetType(),
			AppDomain.CurrentDomain.FriendlyName,
			_creationDate);
	}

	public void TestMethod(string callingDomain)
	{
		Console.WriteLine("Calling from [{0}] to [{1}]",
			callingDomain,
			Thread.GetDomain().FriendlyName);
	}
}