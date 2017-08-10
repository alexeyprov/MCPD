using System;
using System.Diagnostics;
using System.Threading;

namespace SleepingBarber
{
    internal sealed class PlainSimulator : BaseSimulator
    {
        private readonly object _lock;
        private SemaphoreSlim _customerLock;
        private SemaphoreSlim _barberLock;
        private int _waiting;
        private bool _isNoMoreCustomers;

        public PlainSimulator()
        {
            _lock = new object();
        }

        protected override void Initialize(int barberCount, int customerCount, int chairCount)
        {
            base.Initialize(barberCount, customerCount, chairCount);

            if (_customerLock != null)
            {
                _customerLock.Dispose();
            }

            _customerLock = new SemaphoreSlim(0, customerCount);

            if (_barberLock != null)
            {
                _barberLock.Dispose();
            }

            _barberLock = new SemaphoreSlim(0, barberCount);
            
            _isNoMoreCustomers = false;
        }

        protected override void BarberThread(int id)
        {
            while (!Volatile.Read(ref _isNoMoreCustomers))
            {
                if (!_customerLock.Wait(2000))
                {
                    continue;
                }

                lock (_lock)
                {
                     Debug.Assert(_waiting > 0);
                     _waiting--;
                }

                // signal to customers that the barber is available
                _barberLock.Release();

                CutHair(id);
            }
        }

        protected override void CustomerThread(int id)
        {
            int chairCount = ChairCount;
            lock (_lock)
            {
                Debug.Assert(_waiting <= chairCount && _waiting >= 0);

                if (_waiting == ChairCount)
                {
                    Log($"Customer {id} is leaving without a haircut");
                    return;
                }

                _waiting++;
            }

            _customerLock.Release();
            _barberLock.Wait();

            GetHaircut(id);
        }

        protected override void CompleteCustomers()
        {
            _isNoMoreCustomers = true;
        }
    }
}