using System;
using System.Collections.Generic;
using System.Text;

namespace InterlockedTest
{
	class ProtectedResource
	{
		PowerThreading.SpinWaitLock _swl = new PowerThreading.SpinWaitLock();
		double _a, _b, _c;

		public void SetData(double a, double b)
		{
			_swl.Enter();
			try
			{
				_a = a;
				_b = b;
				_c = Math.Sqrt(a * a + b * b);
			}
			finally
			{
				_swl.Leave();
			}
		}

		public void CheckData()
		{
			_swl.Enter();
			try
			{
				if (_c != Math.Sqrt(_a * _a + _b * _b))
				{
					throw new Exception("Data validation failed");
				}
			}
			finally
			{
				_swl.Leave();
			}
		}
	}
}
