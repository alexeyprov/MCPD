// This class is a strong-typed collection of Employee objects
// implemented via ReadOnlyCollectionBase

using System;
using System.Collections;

public class RoEmployees : 
	ReadOnlyCollectionBase,
	IEmployeeGetter
{
	//Read-only collection typically implements a non-default constructor
	// allowing to initialize it with data
	public RoEmployees(System.Collections.Generic.IEnumerable<Employee> initialData)
	{
		//ReadOnlyCollectionBase does not implement IList (unlike CollectionBase)
		//Hence, we have only InnerList available for data access
		foreach (Employee e in initialData)
		{
			InnerList.Add(e);
		}
	}

	// These are strong-typed analogs of read-only IList methods
	public Employee this[int idx]
	{
		get
		{
			return (Employee) InnerList[idx];
		}
	}

	public bool Contains(Employee e)
	{
		return InnerList.Contains(e);
	}

	public int IndexOf(Employee e)
	{
		return InnerList.IndexOf(e);
	}
}