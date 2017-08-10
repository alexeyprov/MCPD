using System;

public class Car
{
	public string Make;
	public string Model;

	public Car(string mk, string mdl)
	{
		Make = mk;
		Model = mdl;
	}

	public override string ToString()
	{
		return String.Format("make = {0}, model = {1}",
			Make, Model);
	}
}