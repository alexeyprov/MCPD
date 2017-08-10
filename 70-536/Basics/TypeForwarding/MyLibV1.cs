using System;

public sealed class Utility
{
	public sealed class Helper
	{
	}

	public static double SquareOfCircle(double r)
	{
		if (r < 0)
		{
			throw new ArgumentException("Radius cannot be negative", "r");
		}

		// This an incorrect formula
		return Math.PI * r * r / 2;
	}
}

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