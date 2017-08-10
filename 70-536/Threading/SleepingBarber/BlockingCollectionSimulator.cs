using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace SleepingBarber
{
    internal sealed class BlockingCollectionSimulator : BaseSimulator
    {
        private BlockingCollection<int> _collection;

        protected override void Initialize(int barberCount, int customerCount, int chairCount)
        {
            base.Initialize(barberCount, customerCount, chairCount);

            if (_collection != null)
            {
                _collection.Dispose();
            }

            _collection = new BlockingCollection<int>(chairCount);
        }

        protected override void CompleteCustomers()
        {
            _collection.CompleteAdding();
        }

        protected override void BarberThread(int id)
        {
            int customerId;
            while (_collection.TryTake(out customerId, Timeout.Infinite))
            {
                Parallel.Invoke(
                    () => GetHaircut(customerId),
                    () => CutHair(id));
            }
        }

        protected override void CustomerThread(int id)
        {
            if (!_collection.TryAdd(id))
            {
                Log($"Customer {id} is leaving without a haircut");
            }
        }
    }
}