using System;

public sealed class ValueTypeItf
{
	static void Main()
	{
		Point p = new Point();
		Console.WriteLine(p);    //undefined

		p.ChangeTo(1, 1);
		Console.WriteLine(p);	//(1, 1)

		object o = p;
		((Point) o).ChangeTo(2, 2);
		Console.WriteLine(o);	//(1, 1)

		((IChangeBoxedPoint) p).ChangeTo(3, 3);
		Console.WriteLine(p);	//(1, 1)
		
		((IChangeBoxedPoint) o).ChangeTo(4, 4);
		Console.WriteLine(o);	//(4, 4)
	}
}