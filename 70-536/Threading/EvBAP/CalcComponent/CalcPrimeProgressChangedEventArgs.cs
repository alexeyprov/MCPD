using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CalcComponent
{
	public class CalcPrimeProgressChangedEventArgs :
		ProgressChangedEventArgs
	{
		int _lastPrime;
	
		public CalcPrimeProgressChangedEventArgs(int lastPrime, int pct, object state) :
			base(pct, state)
		{
			_lastPrime = lastPrime;
		}

		public int LastPrime
		{
			get
			{
				return _lastPrime;
			}
		}
	}
	
}
