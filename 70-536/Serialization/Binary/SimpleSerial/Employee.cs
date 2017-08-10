using System;
using System.Runtime.Serialization;

[Serializable]
public class Employee :
	IEquatable<Employee>,
	IDeserializationCallback
{
	public Employee(string ssn, string name, int yb)
	{
		_ssn = ssn;
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

	public override string ToString()
	{
		return String.Format("{0}: {1}, {2} yb",
			_ssn,
			_name,
			_yb);
			
	}

	void IDeserializationCallback.OnDeserialization(object caller)
	{
		RecalcAge();
		CheckForSsn();
	}
	
	
	protected void RecalcAge()
	{
		_age = (int) ((DateTime.Now - new DateTime(_yb, 1, 1)).Days / 365.25);
	}

	protected void CheckForSsn()
	{
		const string UNKNOWN_SSN = "(unknown)";
		if (null == _ssn)
		{
			_ssn = UNKNOWN_SSN;
		}
	}


	private int _yb;
	[NonSerialized]
	private int _age;
	private string _name;
	// Field added in the new version
	[OptionalField]
	private string _ssn;
}