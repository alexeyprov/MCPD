using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

class GenericTest
{
	static void Main()
	{
		TestList();
		TestDictionary();	
		TestLinkedList();
		SortedList<Employee, Decimal> sl = new SortedList<Employee, Decimal>(Employee.GetPayroll());
		TestSortedListOrDictionary(sl);

		SortedDictionary<Employee, Decimal> sd = new SortedDictionary<Employee, Decimal>(
			Employee.GetPayroll(),
			new Employee.AgeComparer());
		TestSortedListOrDictionary(sd);

		TestStack();
		TestQueue();
	}
	
	private static void TestList()
	{
		//IList<T>
		IList<int> il = new List<int>(15);

		il.Add(3);
		il.Add(5);
		il.Add(7);

		// IList<T> inherits ICollection<T>
		GenericCollectionHelper.PrintCollectionEx(il);
		GenericCollectionHelper.PrintCollectionEx(((List<int>) il).AsReadOnly());
		il.Clear();

		// List<T>
		List<Employee> l = new List<Employee>();
		l.AddRange(Employee.GetTestData());
		l.Sort();

		// List<T> implements IList<T>
		GenericCollectionHelper.PrintList(l);
		IComparer<Employee> comparer = new Employee.AgeComparer();
		l.Sort(comparer);

		// List<T>.Enumerator internal class
		List<Employee>.Enumerator en = l.GetEnumerator();
		GenericCollectionHelper.PrintEnumerator(en);

		l.Reverse();
		GenericCollectionHelper.PrintList(l.AsReadOnly());

		// Test IEqualityComparer<T>
		Employee e1 = new Employee("George Bush", 61);
		Employee e2 = (Employee) l[0]; //IList<T>.get_Item[int]
		Console.WriteLine("{0} {2} {1} using age-based comparison", e1, e2,
			((IEqualityComparer<Employee>) comparer).Equals(e1, e2) ? "==" : "!=");
		Console.WriteLine("{0} {2} {1} using intrinsic comparison", e1, e2,
			e1.Equals(e2) ? "==" : "!=");
	}

	
	private static void TestDictionary()
	{
		string[] data = {"Welcome", "to", "Babylon", "five"};
		Dictionary<string, int> d = new Dictionary<string, int>(data.Length);

		foreach (string s in data)
		{
			d[s] = s.Length;
		}
		
		// Dictionary<K, V> implements ICollection<KeyValuePair<K, V>>
		GenericCollectionHelper.PrintCollection(d);

		// ...and IEnumarable<KeyValuePair<K, V>>
		GenericCollectionHelper.EnumerateDictionary(d);
		d[data[0]] = d[data[0]] * 2; //compile-time verifications, no boxing!

		// The main interface of Dictionary<K, V> is IDictionary<K, V>
		GenericCollectionHelper.PrintDictionary(d);

		// Also, it exposes custom enumerator which implements
		// IDictionaryEnumerator among many other interfaces
		Dictionary<string, int>.Enumerator en = d.GetEnumerator();
		CollectionHelper.EnumerateDictionary(en);

		//Keys/values collections can be also enumerated independently
		Dictionary<string, int>.KeyCollection keys = d.Keys;
		// KeyCollection implements ICollection<K>
		GenericCollectionHelper.PrintCollection(keys);

		Dictionary<string, int>.ValueCollection.Enumerator valsEn = d.Values.GetEnumerator();
		// ValueCollection.Enumerator implements IEnumerator<V>
		GenericCollectionHelper.PrintEnumerator(valsEn);
	}

	private static void TestLinkedList()
	{
		// LinkedList<T> has custom addition/removal methods
		LinkedList<Employee> lst = new LinkedList<Employee>();
		lst.AddFirst(new Employee("Sergey Semak", 32));
		lst.AddLast(new Employee("Nikita Bazhenov", 24));

		// LinkedList<T> implements ICollection<T>
		GenericCollectionHelper.PrintCollection(lst);

		// LinkedListNode<T>
		LinkedListNode<Employee> node = new LinkedListNode<Employee>(new Employee("Egor Titov", 30));
		PrintListNode(node);

		lst.AddBefore(lst.Last, node);
		PrintListNode(node);

		// LinkedList<T>::Enumerator
		LinkedList<Employee>.Enumerator en = lst.GetEnumerator();
		GenericCollectionHelper.PrintEnumerator(en);
	}

	private static void PrintListNode<T>(LinkedListNode<T> node)
	{
	        LinkedList<T> lst = node.List;
		if (null == lst)
		{
			Console.WriteLine("The node is standalone");
		}
		else
		{
			Console.WriteLine("The node belongs to a list with {0} items", lst.Count);
		}
		
		StringBuilder sb = new StringBuilder(" * " + node.Value + " * ");
		
		// The list is double-linked
		LinkedListNode<T> n = node.Previous;
		while (n != null)
		{
			sb.Insert(0, n.Value + " -> ");
			n = n.Previous;
		}

		n = node.Next;
		while (n != null)
		{
			sb.AppendFormat(" -> {0}", n.Value);
			n = n.Next;
		}

		Console.WriteLine(sb.ToString());
	}

	private static void TestSortedListOrDictionary<TKey, TValue>(IDictionary<TKey, TValue> sl)
	{
		// SortedList<K, V> is sorted by Keys
		// If custom comparer is not provided,
		// Comparer.Default uses IComparable<K> implementation
		GenericCollectionHelper.PrintDictionary(sl);

		// Keys/Values of SortedList<K, V> go in the same order
		GenericCollectionHelper.PrintCollection(sl.Keys);
		GenericCollectionHelper.PrintCollection(sl.Values);
	}

	
	private static void TestStack()
	{
		Employee[] es = Employee.GetTestData();
		Stack<Employee> s = new Stack<Employee>(es.Length);
		foreach (Employee e in es)
		{
			s.Push(e);
		}

		// Since ICollection<T> has addition/removal methods
		// Stack<T> implements non-generic version only
		CollectionHelper.PrintCollection(s);
		
		Stack<Employee>.Enumerator en = s.GetEnumerator();
		GenericCollectionHelper.PrintEnumerator(en);

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

	private static void TestQueue()
	{
		Employee[] es = Employee.GetTestData();
		Queue<Employee> q = new Queue<Employee>(es.Length);
		foreach (Employee e in es)
		{
			q.Enqueue(e);
		}

		// Since ICollection<T> has addition/removal methods
		// Queue<T> implements non-generic version only
		CollectionHelper.PrintCollection(q);
		
		Queue<Employee>.Enumerator en = q.GetEnumerator();
		GenericCollectionHelper.PrintEnumerator(en);

		bool queueValid = true;
		for (int i = 0, len = es.Length; i < len; ++i)
		{
			if (!q.Dequeue().Equals(es[i]))
			{
				queueValid = false;
				Console.WriteLine("Queue Corruption at position {0}", i);
			}
		}
		Console.WriteLine("Queue is {0}", (queueValid) ? "valid" : "invalid");
	}
}