using System.Runtime.CompilerServices;

[assembly:TypeForwardedTo(typeof(Utility))]

//This line generates CS0730
//[assembly:TypeForwardedTo(typeof(Utility.Helper))]

public sealed class MetaInfo
{
	public static string Description
	{
		get
		{
			return "MyLib assembly with math helper functions";
		}
	}
}