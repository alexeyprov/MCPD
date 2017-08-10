using System;
using System.Collections;

public class EmployeesByID : 
	DictionaryBase,
	IEmployeeGetter
{
	// These are strong-typed analogs of IDictionary methods
	public void Add(int id, Employee e)
	{
		Dictionary.Add(id, e); //Calling Dictionary instead of InnerHashtable invokes OnXxx methods
	}

	public void Remove(int id)
	{
		Dictionary.Remove(id);
	}

	public Employee this[int id]
	{
		get
		{
			return (Employee) Dictionary[id];
		}
		set
		{
			Dictionary[id] = value;
		}
	}

	public bool Contains(int id)
	{
		return Dictionary.Contains(id);
	}

	//This is an example of values validation
	protected override void OnValidate(object key, object value)
	{
		if (key.GetType() != typeof(int)) // || ((int) key) <= 0)
		{
			throw new ArgumentException("key must be integer", "key");
		}

		if (value.GetType() != typeof(Employee))
		{
			throw new ArgumentException("value must be Employee", "value");
		}
	}
}