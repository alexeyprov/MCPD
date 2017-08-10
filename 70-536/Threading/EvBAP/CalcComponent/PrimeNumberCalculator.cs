using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace CalcComponent
{
    using CalcPrimeCompletedEventHandler = EventHandler<CalcPrimeCompletedEventArgs>;
    using CalcPrimeProgressChangedEventHandler = EventHandler<CalcPrimeProgressChangedEventArgs>;

    public partial class PrimeNumberCalculator : Component
    {
        private delegate void WorkerEventHandler(int numberToTest, AsyncOperation op);

        #region Private Fields

        /// <summary>
        /// User tokens for running async tasks
        /// </summary>
        private readonly ISet<object> _runningTasks = new HashSet<object>();

        private readonly object _taskLock = new object();

        #endregion

        #region Constructors

        public PrimeNumberCalculator()
        {
            InitializeComponent();
        }

        public PrimeNumberCalculator(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region Operations

        public void CalculatePrimeAsync(int number, object taskId)
        {
            lock (_taskLock)
            {
                if (_runningTasks.Contains(taskId))
                {
                    throw new ArgumentException("Asynchronous operation with the same ID is already running",
                     "taskId");
                }
                _runningTasks.Add(taskId);
            }

            // capture current sync context and user state
            AsyncOperation op = AsyncOperationManager.CreateOperation(taskId);
            WorkerEventHandler d = new WorkerEventHandler(CalculatePrimeHelper);

            // start operation on a thread pool thread
            d.BeginInvoke(number, op, null, null);
        }

        public void CancelAsync(object taskId)
        {
            lock (_taskLock)
            {
                if (IsTaskCancelled(taskId))
                {
                    throw new ArgumentException("taskId");
                }
                _runningTasks.Remove(taskId);
            }
        }

        #endregion

        #region Events

        public event CalcPrimeCompletedEventHandler CalcPrimeCompleted;
        public event CalcPrimeProgressChangedEventHandler ProgressChanged;
        
        #endregion

        #region Implementation

        // Thanks to SynchronizationContext/AsyncOperation
        // this call comes on a proper thread
        protected virtual void OnProgressChanged(CalcPrimeProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, e);
            }
        }

        // Thanks to SynchronizationContext/AsyncOperation
        // this call comes on a proper thread
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
                IEnumerable<int> primes = BuildPrimeList((int)Math.Sqrt(numberToTest), op);
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
            lock (_taskLock)
            {
                cancelled = IsTaskCancelled(op.UserSuppliedState);
                if (!cancelled)
                {
                    _runningTasks.Remove(op.UserSuppliedState);
                }
            }

            // Marshal call to a proper thread via
            // SynchronizationContext::Post()
            op.PostOperationCompleted(
                e => OnCalculateCompleted((CalcPrimeCompletedEventArgs)e),
                new CalcPrimeCompletedEventArgs(
                    numberToTest,
                    firstDivisor,
                    isPrime,
                    err,
                    cancelled,
                    op.UserSuppliedState));
        }

        private bool IsTaskCancelled(object taskId)
        {
            return !_runningTasks.Contains(taskId);
        }

        private static bool IsPrime(int numberToTest, IEnumerable<int> primes, ref int firstDivisor)
        {
            firstDivisor = 1;
            int maxDivisor = (int)Math.Sqrt(numberToTest);
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

        private IEnumerable<int> BuildPrimeList(int max, AsyncOperation op)
        {
            IList<int> primes = new List<int> { 2, 3 };
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
                    op.Post(
                        e => OnProgressChanged((CalcPrimeProgressChangedEventArgs)e),
                        new CalcPrimeProgressChangedEventArgs(
                            primes[primes.Count - 1],
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
