using System;
using System.Collections.Generic;
using System.Diagnostics;

public sealed class GenericCollectionHelper
{
	public static void PrintEnumerator<T>(IEnumerator<T> col)
	{
		Console.WriteLine("Printing unknown collection of {0} via {1} enumerator...", 
			typeof(T), col.GetType());
		col.Reset();
		while (col.MoveNext())
		{
			T t = col.Current;
			Console.Write("\t{0}", t);
		}
		Console.WriteLine("\nEnd printing");
	}

	private static void PrintEnumerable<T>(IEnumerable<T> col)
	{
		foreach (T item in col)
		{
			Console.Write("\t{0}", item);
		}
	}

	public static void PrintCollection<T>(ICollection<T> col)
	{
		Console.WriteLine("Printing {0} collection with {1} items...", col.GetType(), col.Count);
		T[] ar = new T[col.Count];
		col.CopyTo(ar, 0);

		// System.Array implements IList<T> (and, hence, IEnumerable<T>)
		IEnumerable<T> enumerable = ar;

		PrintEnumerable(enumerable);
		Console.WriteLine("\nEnd printing");
	}

	public static void PrintCollectionEx<T>(ICollection<T> col) where T : new()
	{
		try
		{
			T t = new T();
			col.Add(t);
			col.Remove(t);
		}
		catch (NotSupportedException ex)
		{
			Console.WriteLine(ex);
			if (!col.IsReadOnly)
			{
				throw;
			}
		}
		Console.WriteLine("Printing {0} collection with {1} items...", col.GetType(), col.Count);
		T[] ar = new T[col.Count];
		col.CopyTo(ar, 0);

		// System.Array implements IList<T> (and IEnumerable)
		IEnumerable<T> enumerable = ar;

		PrintEnumerable(enumerable);
		Console.WriteLine("\nEnd printing");
	}

	public static void PrintList<T>(IList<T> list)
	{
		Console.WriteLine("Printing {0} list with {1} items...", list.GetType(), list.Count);
		for (int i = 0; i < list.Count; i++)
		{
			Console.Write("\t#{0}={1}", i, list[i]);	
		}
		Console.WriteLine("\nEnd printing");

		CheckListAccess(list);
	}

	private static void CheckListAccess<T>(IList<T> list)
	{
		if (list.IsReadOnly)
		{
			Debug.Assert(list.Count > 0);
			try
			{
				list.Insert(0, default(T));
			}
			catch (NotSupportedException ex)
			{
				Console.WriteLine(ex);
			}
		}
	}

	public static void PrintDictionary<TKey, TValue>(IDictionary<TKey, TValue> col)
	{
		Console.WriteLine("Printing {0} dictionary with {1} items...", col.GetType(), col.Count);

		// IDictionary<TKey, TValue>::Keys
		foreach (TKey k in col.Keys)
		{
			// IDictionary<TKey, TValue>::get_Item
			Console.Write("\t{0}={1}", k, col[k]);
		}
		Console.WriteLine("\nEnd printing");
	}

	public static void EnumerateDictionary<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> en)
	{
		Console.WriteLine("Printing {0} dictionary via enumerator...", en.GetType());

		// IEnumerable<K, V> returns IEnumerator<K, V>
		IEnumerator<KeyValuePair<TKey, TValue>> er = en.GetEnumerator();
		er.Reset();
		while (er.MoveNext())
		{
			Console.Write("\t{0}={1}", er.Current.Key, er.Current.Value);
		}
		Console.WriteLine("\nEnd printing");
	}
}