using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public sealed class AsyncOneManyLock
{
    #region Private Fields

    private static Task _noContentionAccessor;

    private int _state;
    private int _pendingReaderCount;
    private SpinLock _lock;
    private TaskCompletionSource<object> _waitingReaderSignal;
    private readonly Queue<TaskCompletionSource<object>> _waitingWriterSignals;

    #endregion

    #region Constructors

    static AsyncOneManyLock()
    {
        _noContentionAccessor = Task.FromResult<object>(null);
    }

    public AsyncOneManyLock()
    {
        _lock = new SpinLock(false);
        _waitingWriterSignals = new Queue<TaskCompletionSource<object>>();
        _waitingReaderSignal = new TaskCompletionSource<object>();
    }

    #endregion

    #region Public Methods

    public Task WaitAsync(OneManyMode mode)
    {
        Task accessor = _noContentionAccessor;

        bool lockTaken = false;
        _lock.Enter(ref lockTaken);

        switch (mode)
        {
            case OneManyMode.Shared:
                if (IsFree || IsReading)
                {
                    AddReaders(1);
                }
                else
                {
                    // contention: increment pending readers and return reader task
                    accessor = _waitingReaderSignal.Task.ContinueWith(t => t.Result);
                    _pendingReaderCount++;
                }
                break;
            case OneManyMode.Exclusive:
                if (IsFree)
                {
                    IsWriting = true;
                }
                else
                {
                    // contention: queue a new waitable task
                    TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
                    _waitingWriterSignals.Enqueue(tcs);
                    accessor = tcs.Task;
                }
                break;
        }

        _lock.Exit();

        return accessor;
    }

    public void Release()
    {
        bool lockTaken = false;
        _lock.Enter(ref lockTaken);

        TaskCompletionSource<object> tcs = null;

        if (IsReading)
        {
            AddReaders(-1);
        }
        else if (IsWriting)
        {
            IsFree = true;
        }

        if (IsFree)
        {
            // if free, wake one waiting writer or all waiting readers
            if (_waitingWriterSignals.Count > 0)
            {
                IsWriting = true;
                tcs = _waitingWriterSignals.Dequeue();
            }
            else if (_pendingReaderCount > 0)
            {
                AddReaders(_pendingReaderCount);
                _pendingReaderCount = 0;
                tcs = _waitingReaderSignal;

                _waitingReaderSignal = new TaskCompletionSource<object>();
            }
        }

        _lock.Exit();

        if (tcs != null)
        {
            tcs.SetResult(null);
        }
    }

    #endregion

    #region Implementation

    private bool IsFree
    {
        get
        {
            return _state == 0;
        }
        set
        {
            if (value)
            {
                _state = 0;
            }
        }
    }

    private bool IsWriting
    {
        get
        {
            return _state == -1;
        }
        set
        {
            if (value)
            {
                _state = -1;
            }
        }
    }

    private bool IsReading
    {
        get
        {
            return _state > 0;
        }
    }

    private void AddReaders(int readers)
    {
        _state += readers;
    }

    #endregion
}