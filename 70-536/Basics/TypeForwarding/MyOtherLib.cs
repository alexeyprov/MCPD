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

		//This is a correct formula
		return Math.PI * r * r;
	}
}
