using System;
using System.Collections;
using System.Diagnostics;

public class CollectionHelper
{
	public static void PrintEnumerable(IEnumerable col)
	{
		foreach (object item in col)
		{
			Console.Write("\t{0}", item);
		}
	}

	public static void PrintCollection(ICollection col)
	{
		PrintCollection(col, false);
	}

	public static void PrintCollection(ICollection col, bool threadSafe)
	{
		Console.WriteLine("Printing {0} collection with {1} items...", col.GetType(), col.Count);
		IEnumerable enumerable;
		if (threadSafe)
		{
			Debug.Assert(col.IsSynchronized);

			// ICollection inherits IEnumerable
			enumerable = col;
		}
		else
		{
			if (col.IsSynchronized)
			{
				enumerable = col;
			}
			else
			{
				object[] ar = new object[col.Count];
				col.CopyTo(ar, 0);

				// System.Array implements IList (and IEnumerable)
				enumerable = ar;
			}

		}

		PrintEnumerable(enumerable);
		Console.WriteLine("\nEnd printing");
	}

	public static void PrintList(IList col)
	{
		Console.WriteLine("Printing {0} list with {1} items...", col.GetType(), col.Count);
		for (int i = 0; i < col.Count; i++)
		{
			Console.Write("\t#{0}={1}", i, col[i]);	
		}
		Console.WriteLine("\nEnd printing");

		CheckListAccess(col);
	}

	private static void CheckListAccess(IList list)
	{
		if (list.IsReadOnly)
		{
			Debug.Assert(list.Count > 0);
			try
			{
				list[0] = null;
			}
			catch (NotSupportedException ex)
			{
				Console.WriteLine(ex);
			}
		}
		else if (!list.IsFixedSize)
		{
			object cookie = new object();
			int idx = list.Add(cookie);
        		//list.Remove(cookie);
			list.RemoveAt(idx);
		}
	}

	public static void PrintDictionary(IDictionary col)
	{
		Console.WriteLine("Printing {0} dictionary with {1} items...", col.GetType(), col.Count);

		// IDictionary inherits ICollection that inherits IEnumerable
		object lastKey = null;
		foreach (DictionaryEntry de in col)
		{
			Console.Write("\t{0}={1}", de.Key, de.Value);
			lastKey = de.Key;
		}
		Console.WriteLine("\nEnd printing");

		CheckDictionaryAccess(col, lastKey);
	}

	public static void EnumerateDictionary(IDictionaryEnumerator en)
	{
		Console.WriteLine("Printing dictionary via enumerator...");

		// IDictionaryEnumerator inherits IEnumerator
		en.Reset();
		while (en.MoveNext())
		{
			Console.Write("\t{0}={1}", en.Key, /*en.Value*/ en.Entry.Value);
		}
		Console.WriteLine("\nEnd printing");
	}

	private static void CheckDictionaryAccess(IDictionary dict, object key)
	{
		if (dict.IsReadOnly)
		{
			try
			{
				dict[key] = null;
			}
			catch (NotSupportedException ex)
			{
				Console.WriteLine(ex);
			}
		}
		else if (!dict.IsFixedSize)
		{
			object cookie = new object();
			dict.Add(cookie, cookie);
        		dict.Remove(cookie);
		}
	}
}