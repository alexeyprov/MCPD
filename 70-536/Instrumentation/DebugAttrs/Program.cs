using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DebugAttrs
{
	class Program
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		const string TEST = "This string is hidden intentionally";

		static void Main(string[] args)
		{
			HashTableEx ht = new HashTableEx();
			ht.Add(1, "one");
			ht.Add(2, "two");
			Console.WriteLine("Hashtable = {0}", ht);
		}
	}
}
