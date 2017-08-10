using System.ComponentModel.Composition;
using Calculator.Interfaces;

namespace Calculator.FirstExtension
{
	[Export(typeof(IOperation))]
	[ExportMetadata("Operator", '%')]
	public class Modulo : IOperation
	{
		int IOperation.Perform(int l, int r)
		{
			return l % r;
		}
	}
}
