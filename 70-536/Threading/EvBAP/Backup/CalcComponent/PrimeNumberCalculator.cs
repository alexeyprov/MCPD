using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace CalcComponent
{
	using CalcPrimeCompletedEventHandler = EventHandler<CalcPrimeCompletedEventArgs>;
	using CalcPrimeProgressChangedEventHandler = EventHandler<CalcPrimeProgressChangedEventArgs>;

	public partial class PrimeNumberCalculator : Component
	{
		private delegate void WorkerEventHandler(int numberToTest, AsyncOperation op);
	
		#region Data Members
		/// <summary>
		/// Called from a thread pool's thread to fire
		/// Progress event
		/// </summary>
		SendOrPostCallback _onProgressReportDelegate;
		/// <summary>
		/// Called from a thread pool's thread to fire
		/// Completed event
		/// </summary>
		SendOrPostCallback _onCompletedDelegate;
		/// <summary>
		/// Maps user state (object)
		/// to
		/// Async operation lifetime (AsyncOperation)
		/// </summary>
		HybridDictionary _userStateToLifeTime = new HybridDictionary();
		#endregion

		#region Construction/Destruction
		public PrimeNumberCalculator()
		{
			InitializeComponent();
			InitializeDelegates();
		}

		public PrimeNumberCalculator(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
			InitializeDelegates();
		}
		#endregion

		#region Operations
		public void CalculatePrimeAsync(int number, object taskId)
		{
			AsyncOperation lifetime = AsyncOperationManager.CreateOperation(taskId);
			lock (_userStateToLifeTime.SyncRoot)
			{
				if (_userStateToLifeTime.Contains(taskId))
				{
					throw new ArgumentException("Asynchronous operation with the same ID is already running",
					 "taskId");
				}
				_userStateToLifeTime[taskId] = lifetime;
			}
			
			WorkerEventHandler d = new WorkerEventHandler(CalculatePrimeHelper);
			d.BeginInvoke(number, lifetime, null, null);
		}

		public void CancelAsync(object taskId)
		{
			lock (_userStateToLifeTime.SyncRoot)
			{
				if (IsTaskCancelled(taskId))
				{
					throw new ArgumentException();
				}
				_userStateToLifeTime.Remove(taskId);
			}
		}
		#endregion

		#region Events
		public event CalcPrimeCompletedEventHandler CalcPrimeCompleted;
		public event CalcPrimeProgressChangedEventHandler ProgressChanged;
		#endregion

		#region Implementation
		protected virtual void InitializeDelegates()
		{
			_onProgressReportDelegate = new SendOrPostCallback(ReportProgress);
			_onCompletedDelegate = new SendOrPostCallback(CalculateCompleted);
		}
		
		/// <summary>
		/// Thankfully to SynchronizationContext/AsyncOperation
		/// this call comes to a proper thread
		/// </summary>
		/// <param name="state"></param>
		private void ReportProgress(object state)
		{
			OnProgressChanged((CalcPrimeProgressChangedEventArgs) state);
		}
		
		protected virtual void OnProgressChanged(CalcPrimeProgressChangedEventArgs e)
		{
			if (ProgressChanged != null)
			{
				ProgressChanged(this, e);
			}
		}

		/// <summary>
		/// Thankfully to SynchronizationContext/AsyncOperation
		/// this call comes to a proper thread
		/// </summary>
		/// <param name="state"></param>		
		private void CalculateCompleted(object state)
		{
			OnCalculateCompleted((CalcPrimeCompletedEventArgs) state);
		}
		
		protected virtual void OnCalculateCompleted(CalcPrimeCompletedEventArgs e)
		{
			if (CalcPrimeCompleted != null)
			{
				CalcPrimeCompleted(this, e);
			}
		}
		
		private void CalculatePrimeHelper(int numberToTest, AsyncOperation op)
		{
			Exception err = null;
			bool isPrime = false;
			int firstDivisor = 1;
			try
			{
				ICollection<int> primes = BuildPrimeList((int) Math.Sqrt(numberToTest), op);
				isPrime = IsPrime(numberToTest, primes, ref firstDivisor);	
			}
			catch (Exception ex)
			{
				err = ex;
			}
			CompletionMethod(numberToTest, isPrime, firstDivisor, err, op);
		}

		private void CompletionMethod(int numberToTest, bool isPrime, int firstDivisor, Exception err, AsyncOperation op)
		{
			bool cancelled;
			lock (_userStateToLifeTime.SyncRoot)
			{
				cancelled = IsTaskCancelled(op.UserSuppliedState);
				if (!cancelled)
				{
					_userStateToLifeTime.Remove(op.UserSuppliedState);
				}
			}
			
			
			// Marshal call to a proper thread via
			// SynchronizationContext::Post()
			op.PostOperationCompleted(_onCompletedDelegate,
				new CalcPrimeCompletedEventArgs(numberToTest,
													firstDivisor,
													isPrime,
													err,
													cancelled,
													op.UserSuppliedState));
		}

		private bool IsTaskCancelled(object taskId)
		{
			return !_userStateToLifeTime.Contains(taskId);
		}
		
		private bool IsPrime(int numberToTest, ICollection<int> primes, ref int firstDivisor)
		{
			firstDivisor = 1;
			int maxDivisor = (int) Math.Sqrt(numberToTest);
			foreach (int p in primes)
			{
				if (0 == numberToTest % p)
				{
					firstDivisor = p;
					return false;
				}
			}
			return true;
		}
		
		private ICollection<int> BuildPrimeList(int max, AsyncOperation op)
		{
			List<int> primes = new List<int>(new int[]{ 2, 3});
			int pct = 0,
				oldPct = -1;
			for (int n = 5; n <= max && !IsTaskCancelled(op.UserSuppliedState); n += 2)
			{
				int dummy = 0;
				if (IsPrime(n, primes, ref dummy))
				{
					primes.Add(n);
				}
				pct = n * 100 / max;
				if (pct != oldPct)
				{
					oldPct = pct;
					op.Post(_onProgressReportDelegate,
						new CalcPrimeProgressChangedEventArgs(primes[primes.Count - 1],
																pct,
																op.UserSuppliedState));
				}
				Thread.Sleep(10);
			}
			return primes;
		}
		#endregion
	}
}
