using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;

namespace PowerThreading
{
	public struct SpinWaitLock
	{
		private const int FREE = 0;
		private const int OWNED = 1;

		private int _lockFlag; // = FREE

		public void Enter()
		{
			Thread.BeginCriticalRegion();
			while (true)
			{
				if (FREE == Interlocked.Exchange(ref _lockFlag, OWNED))
				{
					// This thread took ownership
					return;
				}

				while (Thread.VolatileRead(ref _lockFlag) != FREE)
				{
					StallThread();
				}
			}
		}

		public void Leave()
		{
			Interlocked.Exchange(ref _lockFlag, FREE);
			Thread.EndCriticalRegion();
		}

		private void StallThread()
		{
			if (1 == Environment.ProcessorCount)
			{
				SwitchToThread();
			}
			else
			{
				Thread.SpinWait(1);
			}
		}

		[DllImport("kernel32.dll")]
		private static extern bool SwitchToThread();
	}
}
