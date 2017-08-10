using System;
using System.Collections;
using System.Collections.Generic;

public class Employee : 
	IComparable<Employee>,
	IComparable,
	IEquatable<Employee>
{
	private string _name;
	private int _age;

	public Employee(string name, int age)
	{
		_name = name;
		_age = age;
	}

	public override string ToString()
	{
		return String.Format("{0}, {1} years old.", _name, _age);
	}

	// IComparable<T>.CompareTo
	public int CompareTo(Employee e)
	{
		// Compare names by default
		return this._name.CompareTo(e._name);
	}

	// IComparable.CompareTo
	int IComparable.CompareTo(object e)
	{
		return CompareTo(TryCastToEmployee(e, "e"));
	}

	// IEquatable<T>.Equals
	public bool Equals(Employee e)
	{
		return this._name.Equals(e._name);
	}

	public override bool Equals(object o)
	{
		return Equals(TryCastToEmployee(o, "o"));		
	}

	public override int GetHashCode()
	{
		return _age ^ _name.GetHashCode();
	}

	public string Name
	{
		get
		{
			return _name;
		}
	}

	public int Age
	{
		get
		{
			return _age;
		}
	}

	private static Employee TryCastToEmployee(object o, string paramName)
	{
		Employee e = o as Employee;
		if (null == e)
		{
			throw new ArgumentException("The passed value cannot be casted to Employee", paramName);
		}
		return e;
	}

	// This class compares Employees by age
	public class AgeComparer : 
		Comparer<Employee>, 
		//IComparer<Employee>, IComparer
		//EqualityComparer<Employee> 
		IEqualityComparer<Employee>, IEqualityComparer
	{
		// Comparer<T>
		public override int Compare(Employee l, Employee r)
		{		
			return l._age - r._age;
		}

		// IEqualityComparer<T>
		public bool Equals(Employee l, Employee r)
		{		
			return l._age == r._age;
		}

		public int GetHashCode(Employee e)
		{
			return e._age;
		}

		/*
		// IComparer (explicit implementation)
		int IComparer.Compare(object l, object r)
		{		
			return Compare(TryCastToEmployee(l, "l"), TryCastToEmployee(r, "r"));
		}
		*/

		// IEqualityComparer (explicit implementation)
		bool IEqualityComparer.Equals(object l, object r)
		{		
			return Equals(TryCastToEmployee(l, "l"), TryCastToEmployee(r, "r"));
		}

		int IEqualityComparer.GetHashCode(object e)
		{
			return GetHashCode(TryCastToEmployee(e, "e"));
		}
	}
	
	public static Employee[] GetTestData()
	{
		return new Employee[] {
			new Employee("Bill Gates", 53),
			new Employee("Jeffrey Richter", 36),
			new Employee("Bjarne Stroustrup", 61),
			new Employee("Alexey Provotorov",28)
		};
	}

	public static IDictionary<Employee, Decimal> GetPayroll()
	{
		Dictionary<Employee, Decimal> d = new Dictionary<Employee, Decimal>();
		Random rnd = new Random();
		foreach (Employee e in GetTestData())
		{
			d[e] = (Decimal) (1000.0 + 9000.0 * rnd.NextDouble());
		}
		return d;
	}
}