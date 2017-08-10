using System;
using System.Runtime.Serialization;

[Serializable]
public class Employee :
	IEquatable<Employee>
{
	public Employee(string name, int yb)
	{
		_name = name;
		_yb = yb;
		RecalcAge();
	}

	public int YearOfBirth
	{
		get
		{
			return _yb;
		}
	}

	public int Age
	{
		get
		{
			return _age;
		}
	}

	public string Name
	{
		get
		{
			return _name;
		}
	}

	// Overrides
	public bool Equals(Employee e)
	{
		if (null == e)
		{
			return false;
		}

		return this.Age == e.Age &&
			this.YearOfBirth == e.YearOfBirth &&
			this.Name == e.Name;
	}

	public override string ToString()
	{
		return String.Format("{0}, {1} year of birth, {2} years old",
			_name,
			_yb,
			_age);
	}

	// Implementation
	[OnSerializing]
	[OnDeserialized]
	protected void RecalcAgeStub(StreamingContext ctx)
	{
		RecalcAge();
	}
	
	protected void RecalcAge()
	{
		_age = (int) ((DateTime.Now - new DateTime(_yb, 1, 1)).Days / 365.25);
	}


	private int _yb;
	[NonSerialized]
	private int _age;
	private string _name;
}