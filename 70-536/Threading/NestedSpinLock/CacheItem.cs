using System;
using System.Diagnostics;
using System.Threading;

internal sealed class CacheItem
{
    private readonly int _key;

    //IMPORTANT: SpinLock is a value type => the field should NOT be read-only
    private SpinLock _lock;
    private bool _isInitialized;
    private string _data;

    public CacheItem(int key)
    {
        _lock = new SpinLock();
        _key = key;
    }

    public string GetData(int subKey)
    {
        if (_isInitialized)
        {
            Debug.Assert(_data != null);
            return _data;
        }

        bool isLockTaken = false;
        while (!isLockTaken)
        {
            _lock.Enter(ref isLockTaken);
        }

        try
        {
            // no need for Volatile.Read, since SpinLock is a memory fence
            if (_isInitialized)
            {
                Debug.Assert(_data != null);
                return _data;
            }

            Debug.Assert(!_isInitialized);
            Debug.Assert(_data == null);

            // expensive initialization goes here
            for (int i = 0; i < 10; ++i)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}>> ({_key}, {subKey}): querying...");
                Thread.Sleep(100);
            }

            Volatile.Write(ref _data, $"CacheItem #{_key}");
            Volatile.Write(ref _isInitialized, true);

            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}>> ({_key}, {subKey}): initialized '{_data}'");

            return _data;
        }
        finally
        {
            _lock.Exit();
        }
    }
}