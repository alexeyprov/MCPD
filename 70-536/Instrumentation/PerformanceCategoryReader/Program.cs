using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PerformanceCategoryReader
{
	class Program
	{
		static void Main()
		{
			PerformanceCounterCategory cat = SelectCategory();
			if (cat != null)
			{
				ReadCategory(cat);
			}
		}

		private static void ReadCategory(PerformanceCounterCategory cat)
		{
			Debug.Assert(cat != null);
			InstanceDataCollectionCollection idColCol = cat.ReadCategory();
			int cnt = idColCol.Count;
			Console.WriteLine("The selected category contains {0} counters:", cnt);
			object[] idColColKeys = new object[cnt];
			idColCol.Keys.CopyTo(idColColKeys, 0);
			for (int i = 0; i < cnt; i++)
			{
				string counterName = (string)idColColKeys[i];
				Console.WriteLine("{0, 4} - {1}:", i + 1, counterName);
				ReadCounterInstances(idColCol[counterName]);
			}
		}

		private static void ReadCounterInstances(InstanceDataCollection idCol)
		{
			int cnt = idCol.Count;
			Console.WriteLine("\tThe \"{0}\" counter belongs to {1} instances",
				idCol.CounterName, cnt);
			object[] idColKeys = new object[cnt];
			idCol.Keys.CopyTo(idColKeys, 0);
			for (int i = 0; i < cnt; i++)
			{
				string instanceName = (string)idColKeys[i];
				Console.WriteLine("\t\t{0, 4} - {1}:", i + 1, instanceName);
				ReadInstanceValues(idCol[instanceName]);
			}
		}

		private static void ReadInstanceValues(InstanceData id)
		{
			CounterSample sample = id.Sample;

			Console.WriteLine("\t\t\tFrom InstanceData:");
			PrintValue("InstanceName", id.InstanceName);
			PrintValue("RawValue", id.RawValue);

			Console.WriteLine("\t\t\tFrom CounterSample:");

			PrintValue("CounterType", sample.CounterType);
			PrintValue("SystemFrequency", sample.SystemFrequency);
			PrintValue("BaseValue", sample.BaseValue);
			PrintValue("RawValue", sample.RawValue);
			PrintValue("CounterFrequency", sample.CounterFrequency);
			PrintValue("CounterTimeStamp", sample.CounterTimeStamp);
			PrintValue("TimeStamp", sample.TimeStamp);
			PrintValue("TimeStamp100nSec", sample.TimeStamp100nSec);
		}

		private static void PrintValue(string name, object value)
		{
			Console.WriteLine("\t\t\t\t{0}: {1}", name, value);
		}

		private static PerformanceCounterCategory SelectCategory()
		{
			PerformanceCounterCategory[] cats = PerformanceCounterCategory.GetCategories();
			int cnt = cats.Length;
			for (int i = 0; i < cnt; i++)
			{
				Console.WriteLine("{0,4} - {1}", i + 1, cats[i].CategoryName);
			}

			Console.Write("Select a category (1 - {0}): ", cnt);
			string catIdx = Console.ReadLine();
			int realIdx = -1;
			if (Int32.TryParse(catIdx, out realIdx) && realIdx >= 1 && realIdx <= cnt)
			{
				return cats[realIdx - 1];
			}
			return null;
		}
	}
}
