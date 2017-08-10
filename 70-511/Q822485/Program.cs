using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace Q822485
{
	class Program
	{
		static void Main(string[] args)
		{
			Evidence myEvidence = AppDomain.CurrentDomain.Evidence;
			IEnumerator hostEvidences = myEvidence.GetHostEnumerator();
			for (int i = 1; hostEvidences.MoveNext(); ++i)
			{
				Console.WriteLine("[{0}] {1}", i, hostEvidences.Current);
			}
			Console.ReadLine();
		}
	}
}
