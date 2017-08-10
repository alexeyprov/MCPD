using System.ComponentModel.Composition;
using Calculator.Interfaces;

namespace SimpleCalculator.Operations
{
	[Export(typeof(IOperation))]
	[ExportMetadata("Operator", '*')]
	public class Multiplication : IOperation
	{
		#region IOperation Members

		int IOperation.Perform(int l, int r)
		{
			return l * r;
		}

		#endregion
	}
}
