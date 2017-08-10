using System;
using System.Collections;
using System.Globalization;

class CollectionTest
{
	static void Main()
	{
		TestArrayList();
		TestHashTable();	
		TestCustomCollection();
		TestComparer();
		TestStack();
	}
	
	private static void TestArrayList()
	{
		ArrayList al = new ArrayList(15);

		al.AddRange(Employee.GetTestData());
		al.Add("Hello");
		al.Add("World");
		al.Add(1);

		Console.WriteLine("List capacity is {0}", al.Capacity);

		// ArrayList implements ICollection
		CollectionHelper.PrintCollection(al);
		CollectionHelper.PrintCollection(ArrayList.Synchronized(al), true);

		al.Clear();
		al.AddRange(Employee.GetTestData());
		al.Sort();

		// ArrayList implements IList
		CollectionHelper.PrintList(ArrayList.ReadOnly(al));
		IComparer comparer = new Employee.AgeComparer();
		al.Sort(comparer);
		al.Reverse();
		CollectionHelper.PrintList(al);
		
		// Test IEqualityComparer
		Employee e1 = new Employee("George Bush", 61);
		Employee e2 = (Employee) al[0]; //IList.get_Item[int]
		Console.WriteLine("{0} {2} {1} using age-based comparison", e1, e2,
			((IEqualityComparer) comparer).Equals(e1, e2) ? "==" : "!=");
		Console.WriteLine("{0} {2} {1} using intrinsic comparison", e1, e2,
			e1.Equals(e2) ? "==" : "!=");
	}

	private static void TestHashTable()
	{
		string[] data = {"Welcome", "to", "Babylon", "five"};
		Hashtable ht = new Hashtable(data.Length);

		foreach (string s in data)
		{
			ht[s] = s.Length;
		}
		
		// Hashtable implements ICollection
		CollectionHelper.PrintCollection(ht);

		// The commented line results in NullReferenceException in
		// SyncHashtable.IEnumerable.GetEnumerator()
		// Since the latter inherits Hashtable, it cannot override explicit
		// implementation of IEnumerable.GetEnumerator. Hence, the parent
		// (non-synchronized) version is called and results in crash.
		
		//CollectionHelper.PrintCollection(Hashtable.Synchronized(ht), true);

		// Call overridden method instead of IEnumerable.GetEnumerator()
		CollectionHelper.EnumerateDictionary(ht.GetEnumerator());
		ht[data[0]] = ((int) ht[data[0]]) * 2;
		// Hashtable implements IDictionary
		CollectionHelper.PrintDictionary(ht);
	}

	private static void TestCustomCollection()
	{
		// Iterators
		EnumerableEmployees ecol = new EnumerableEmployees();
		Console.WriteLine("Enumerable collection via default iterator:");
		CollectionHelper.PrintEnumerable(ecol);
		Console.WriteLine("\nEnumerable collection via non-default iterator:");
		CollectionHelper.PrintEnumerable(ecol.Reversed);
		Console.WriteLine(Environment.NewLine);

		// CollectionBase
		Employees col = new Employees();
		col.Add(new Employee("Egor Titov", 30));
		col.Add(new Employee("Sergey Semak", 32));
		col[1] = new Employee("Nikita Bazhenov", 24);
		try
		{
			((IList) col).Add("test string");
		}
		catch (ArgumentException ex)
		{
			Console.WriteLine(ex);
		}
		PrintCustomCollection(col);

		// ReadOnlyCollectionBase
		RoEmployees rcol = new RoEmployees(Employee.GetTestData());
		PrintCustomCollection(rcol);

		// DictionaryBase
		EmployeesByID idcol = new EmployeesByID();
		foreach (Employee e in col)
		{
			idcol[e.GetHashCode()] = e;
		}
		try
		{
			CollectionHelper.PrintDictionary(idcol);
		}
		catch (ArgumentException ex)
		{
			Console.WriteLine(ex);
		}
	}

	private static void PrintCustomCollection(IEmployeeGetter eg)
	{
		int count = ((ICollection) eg).Count; //Both CollectionBase and ReadOnlyCollectionBase implement ICollection
		Console.WriteLine("Printing {0} collection with {1} items via IEmployeeGetter...", 
			eg.GetType(), count);
		for (int i = 0; i < count; i++)
		{
			Employee e = eg[i];
			Console.Write("\t#{0}={1}", i, e);
		}
		Console.WriteLine("\nEnd printing");
	}

	private static void TestComparer()
	{
		Comparer[] comps = new Comparer[] {
			Comparer.Default,
			Comparer.DefaultInvariant,
			new Comparer(new CultureInfo("es-ES")),
			new Comparer(new CultureInfo(0x40A))
		};
		string s1 = "llegar";
		string s2 = "lugar";
		string[] ops = new string[] {"<", "=", ">"};

		foreach (Comparer comp in comps)
		{
			Console.WriteLine("\"{0}\" {1} \"{2}\" using {3} comparison",
				s1,
				ops[Math.Sign(comp.Compare(s1, s2)) + 1],
				s2,
				comp);
		}
	}

	private static void TestStack()
	{
		Employee[] es = Employee.GetTestData();
		Stack s = new Stack(es.Length);
		foreach (Employee e in es)
		{
			s.Push(e);
		}

		CollectionHelper.PrintCollection(s);

		bool stackValid = true;
		for (int i = es.Length - 1; i >= 0; --i)
		{
			if (!es[i].Equals(s.Pop()))
			{
				stackValid = false;
				Console.WriteLine("Stack Corruption at position {0}", i);
			}
		}
		Console.WriteLine("Stack is {0}", (stackValid) ? "valid" : "invalid");
	}
}