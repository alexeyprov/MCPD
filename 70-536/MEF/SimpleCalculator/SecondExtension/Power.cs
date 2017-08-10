using System;
using System.ComponentModel.Composition;
using Calculator.Interfaces;

namespace Calculator.SecondExtension
{
	[Export(typeof(IOperation))]
	[ExportMetadata("Operator", '^')]
	public class Power : IOperation
	{
		int IOperation.Perform(int l, int r)
		{
			return (int)Math.Pow(l, r);
		}
	}
}
