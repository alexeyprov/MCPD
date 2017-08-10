using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

class SpecializedTest
{
	static void Main()
	{
		TestStringEnumerator();
		TestOrderedDictionary();
		TestNameValueCollection();
	}

	private static void TestStringEnumerator()
	{
		string[] data = {"Welcome", "to", "Babylon", "five"};
		StringCollection col = new StringCollection();
		col.AddRange(data);

		StringEnumerator en = col.GetEnumerator();
		en.Reset();

		StringBuilder sb = new StringBuilder();
		while (en.MoveNext())
		{
			sb.Append(en.Current + " ");
		}

		Console.WriteLine(sb.ToString());
	}

	private static void TestOrderedDictionary()
	{
		OrderedDictionary od = new OrderedDictionary();
		IOrderedDictionary d = od;

		// Add Employees from payroll in the reverse order
		// using IOrderedDictionary::Insert list-like method
		foreach (KeyValuePair<Employee, decimal> p in Employee.GetPayroll())
		{
			d.Insert(0, p.Key, p.Value);
		}

		// Remove by index (IOrderedDictionary::RemoveAt)
		d.RemoveAt(d.Count - 1);

		PrintOrderedDictionary(d);
		CollectionHelper.PrintDictionary(od.AsReadOnly());
	}

	private static void PrintOrderedDictionary(IOrderedDictionary d)
	{
		// ICollection::Count
		int cnt = d.Count;
		Employee[] keys = new Employee[cnt];

		// ICollection::CopyTo()
		d.Keys.CopyTo(keys, 0);

		Console.WriteLine("Printing {0} (IOrderedDictionary) with {1} items...", 
			d.GetType(), cnt);
		for (int i = 0; i < cnt; i++)
		{
			// IOrderedDictionary::get_Item[int]
			Console.Write("\tItem {0}:({1},{2})",
				i, keys[i], d[i]);
		}
		Console.WriteLine("\nEnd Printing");
	}

	private static void TestNameValueCollection()
	{
		NameValueCollection col = new NameValueCollection();
		
		col.Add("black", "coal");
		col.Add("green", "tree");
		col.Add("red", "strawberry");
		// Default comparer/hcp are CaseInsensitiveComparer/CaseInsensitiveHCP
		col.Add("BLACK", "night");
		col["white"] = "snow";
		PrintNameValueCollectionViaKeys(col);

		col.Remove("white");
		PrintNameValueCollectionViaIndexes(col);
	}

	private static void PrintNameValueCollectionViaKeys(NameValueCollection col)
	{
		Console.WriteLine("Printing NameValueCollection with {0} elements via keys...", col.Count);
		NameObjectCollectionBase.KeysCollection keys = col.Keys;
		foreach (string k in keys)
		{
			Console.Write("\t{0}={1}", k, col[k]);
		}
		Console.WriteLine("\nEnd Printing");
	}

	private static void PrintNameValueCollectionViaIndexes(NameValueCollection col)
	{
		int cnt = col.Count;
		Console.WriteLine("Printing NameValueCollection with {0} elements via indexes...", cnt);
		string[] keys = col.AllKeys;

		for (int i = 0; i < cnt; i++)
		{
			Console.Write("\tItem {0}: ({1}, {2})",
				i, keys[i], col[i]);
		}
		Console.WriteLine("\nEnd Printing");
	}
}