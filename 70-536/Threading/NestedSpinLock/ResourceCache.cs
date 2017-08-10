using System.Collections.Generic;
using System.Threading;

internal sealed class ResourceCache
{
    private readonly IDictionary<int, CacheItem> _items;
    //IMPORTANT: SpinLock is a value type => the field should NOT be read-only
    private SpinLock _lock;

    public ResourceCache()
    {
        _items = new Dictionary<int, CacheItem>();
        _lock = new SpinLock();
    }

    public CacheItem this[int key]
    {
        get
        {
            CacheItem result;

            if (_items.TryGetValue(key, out result))
            {
                return result;
            }

            bool isLockTaken = false;
            while (!isLockTaken)
            {
                _lock.Enter(ref isLockTaken);
            }

            try
            {
                if (!_items.TryGetValue(key, out result))
                {
                    result = new CacheItem(key);
                    _items.Add(key, result);
                }
            }
            finally
            {
                _lock.Exit();
            }
            return result;
        }
    }
}