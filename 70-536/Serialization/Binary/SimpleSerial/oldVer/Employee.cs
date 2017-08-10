using System;
using System.Runtime.Serialization;

[Serializable]
public class Employee :
	IEquatable<Employee>,
	IDeserializationCallback
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

	void IDeserializationCallback.OnDeserialization(object caller)
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