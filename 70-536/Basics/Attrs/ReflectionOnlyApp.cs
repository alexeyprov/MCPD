using System;
using System.Collections.Generic;
using System.Reflection;

[assembly:Shape(ShapeType.Circle, Size = 10)]

[Shape(ShapeType.Octagon)]
class ReflectionOnlyApp
{
	static void Main()
	{
		Assembly a = Assembly.ReflectionOnlyLoad("ReflectionOnlyApp");
		ShowAttrInfo(CustomAttributeData.GetCustomAttributes(a));

		Type t = a.GetType("ReflectionOnlyApp");
		ShowAttrInfo(CustomAttributeData.GetCustomAttributes(t));

		MethodInfo mi = t.GetMethod("TestMethod");
		ShowAttrInfo(CustomAttributeData.GetCustomAttributes(mi));

		ParameterInfo pi = mi.GetParameters()[0];
		ShowAttrInfo(CustomAttributeData.GetCustomAttributes(pi));
	}

	static void ShowAttrInfo(IList<CustomAttributeData> attrs)
	{
		Console.WriteLine("\r\n=====================");
		foreach (CustomAttributeData cad in attrs)
		{
			ConstructorInfo ci = cad.Constructor;
			Console.WriteLine(ci.DeclaringType);
			Console.WriteLine("Constructor applied: {0}", ci);

			Console.WriteLine("Constructor parameters:");
			int i = 0;
			foreach (CustomAttributeTypedArgument cp in cad.ConstructorArguments)
			{
				Console.WriteLine("{0} param{1} = {2}", 
					cp.ArgumentType, ++i, cp.Value);
			}

			Console.WriteLine("Named parameters:");
			foreach (CustomAttributeNamedArgument np in cad.NamedArguments)
			{
				Console.WriteLine("{0} {1} = {2}", 
					np.TypedValue.ArgumentType, 
					np.MemberInfo.Name, 
					np.TypedValue.Value);
			}
			Console.WriteLine("=====================");
		}
		
	}

	[Shape(ShapeType.Square, Size = 5)]
	public void TestMethod(
		[Shape]
		int param)
	{
	}
}