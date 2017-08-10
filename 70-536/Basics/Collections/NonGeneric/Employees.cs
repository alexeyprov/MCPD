// This class is a strong-typed collection of Employee objects
// implemented via CollectionBase

using System;
using System.Collections;

public class Employees : 
	CollectionBase,
	IEmployeeGetter
{
	// These are strong-typed analogs of IList methods
	public void Add(Employee e)
	{
		List.Add(e); //Calling List instead of InnerList invokes OnXxx methods
	}

	public void Insert(int pos, Employee e)
	{
		List.Insert(pos, e);
	}

	public void Remove(Employee e)
	{
		List.Remove(e);
	}

	public Employee this[int idx]
	{
		get
		{
			return (Employee) List[idx];
		}
		set
		{
			List[idx] = value;
		}
	}

	public bool Contains(Employee e)
	{
		return List.Contains(e);
	}

	public int IndexOf(Employee e)
	{
		return List.IndexOf(e);
	}

	//This is an example of values validation
	protected override void OnValidate(object value)
	{
		if (value.GetType() != typeof(Employee))
		{
			throw new ArgumentException("value must be Employee");
		}
	}
}
