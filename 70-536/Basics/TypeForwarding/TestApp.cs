using System;

class TestApp
{
	static void Main()
	{
		const int r = 10;
		Console.WriteLine("Square of circle with radius {0} equals {1}",
			r, Utility.SquareOfCircle(r));
	}
}