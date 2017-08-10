using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace PowerThreading
{
	public class InterlockedEx
	{
		public static int And(ref int location1, int with)
		{
			int initial, current = location1;
			do
			{
				initial = current;
				int result = initial & with;
				// check whether location1 has changed since beginning of the loop
				// if not, assign it the calculated result
				// remember its current value in any case
				current = Interlocked.CompareExchange(ref location1, result, initial);
			}
			while (initial != current); //while location1 is being changed

			// value before the last operation start
			return current;
		}

		public static int Or(ref int location1, int with)
		{
			int initial, current = location1;
			do
			{
				initial = current;
				int result = initial | with;
				// check whether location1 has changed since beginning of the loop
				// if not, assign it the calculated result
				// remember its current value in any case
				current = Interlocked.CompareExchange(ref location1, result, initial);
			}
			while (initial != current); //while location1 is being changed

			// value before the last operation start
			return current;
		}
	}
}
