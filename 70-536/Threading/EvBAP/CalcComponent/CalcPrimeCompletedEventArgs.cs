using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace CalcComponent
{
	public class CalcPrimeCompletedEventArgs : AsyncCompletedEventArgs
	{
		int _numberToTest;
		int _firstDivisor = 1;
		bool _isPrime;

		public CalcPrimeCompletedEventArgs(
			int numberToTest,
			int firstDivisor,
			bool isPrime,
			Exception error,
			bool cancelled,
			object userState
			)
			: base(error, cancelled, userState)
		{
			_numberToTest = numberToTest;
			_firstDivisor = firstDivisor;
			_isPrime = isPrime;
		}

		public int NumberToTest
		{
			get 
			{ 
				RaiseExceptionIfNecessary();
				return _numberToTest;
			}
		}
		
		public int FirstDivisor
		{
			get
			{
				RaiseExceptionIfNecessary();
				return _firstDivisor;
			}
		}
		public bool IsPrime
		{
			get
			{
				RaiseExceptionIfNecessary();
				return _isPrime;
			}
		}

	}
}
