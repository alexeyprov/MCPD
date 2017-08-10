using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DebugAttrs
{
	[DebuggerDisplay("Count = {Count}", Type="")]
	[DebuggerTypeProxy(typeof(HashTableViewer))]
	class HashTableEx : Hashtable
	{
		private const string TEST = "This shouldn't appear in the Watch window";

		class HashTableViewer
		{
			const string TEST = "This should appear in the Watch window";
			Hashtable _ht;

			public HashTableViewer(Hashtable ht)
			{
				_ht = ht;
			}

			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public KeyValuePairEx[] Items
			{
				get
				{
					KeyValuePairEx[] retArray = new KeyValuePairEx[_ht.Count];
					int i = 0;
					foreach (object key in _ht.Keys)
					{
						retArray[i++] = new KeyValuePairEx(key, _ht[key]);
					}
					return retArray;
				}
			}
		}
	}
}
