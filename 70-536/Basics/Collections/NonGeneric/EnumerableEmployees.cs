// This class is a simple wrapper with iterators around
// Employee.GetTestData()

using System.Collections;
using System.Collections.Generic;

public class EnumerableEmployees : IEnumerable<Employee>
{
	//IEnumerable<T>.GetEnumerator
	public IEnumerator<Employee> GetEnumerator()
	{
		foreach (Employee e in Employee.GetTestData())
		{
			yield return e;
		}
	}

	//IEnumerable.GetEnumerator()
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	//Non-default iterator
	public IEnumerable<Employee> Reversed
	{
		get
		{
			Employee[] data = Employee.GetTestData();
			System.Array.Reverse(data);
			foreach (Employee e in data)
			{
				yield return e;
			}
		}
	}
}
