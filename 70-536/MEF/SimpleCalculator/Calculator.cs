using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text.RegularExpressions;
using Calculator.Interfaces;

namespace SimpleCalculator
{
	[Export(typeof(ICalculator))]
	internal sealed class Calculator : ICalculator
	{
		private static readonly Regex _expressionRegEx;

		static Calculator()
		{
			_expressionRegEx = new Regex(@"^(?<lhs_op>\d{1,5})(?<operator>.)(?<rhs_op>\d{1,5})$");
		}

		[ImportMany]
		public IEnumerable<Lazy<IOperation, IOperationData>> Operations
		{
			get;
			set;
		}

		#region ICalculator Members

		int ICalculator.Calculate(string input)
		{
			Match m = _expressionRegEx.Match(input);

			if (null == m || !m.Success)
			{
				throw new ArgumentException("Invalid input.");
			}

			int l = int.Parse(m.Groups["lhs_op"].Value),
				r = int.Parse(m.Groups["rhs_op"].Value);

			IOperation op = this.Operations.Single(o => o.Metadata.Operator == m.Groups["operator"].Value[0]).Value;

			return op.Perform(l, r);
		}

		#endregion
	}
}
