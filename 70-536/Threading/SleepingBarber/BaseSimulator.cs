using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SleepingBarber
{
    internal abstract class BaseSimulator : ISimulator
    {
        private readonly Random _random;

        protected BaseSimulator()
        {
            _random = new Random();
        }

        protected static void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] {message}");
        }

        protected int ChairCount
        {
            get;
            private set;
        }

        protected virtual void Initialize(int barberCount, int customerCount, int chairCount)
        {
            ChairCount = chairCount;
        }

        protected virtual void CompleteCustomers()
        {
        }

        protected virtual void CompleteBarbers()
        {
        }

        protected abstract void BarberThread(int id);

        protected abstract void CustomerThread(int id);

        protected void CutHair(int barberId)
        {
            Log($"Barber {barberId} is cutting hair");
            Thread.Sleep(2000);
        }

        protected void GetHaircut(int customerId)
        {
            Log($"Customer {customerId} is getting a haircut");
            Thread.Sleep(2000);
        }

        async Task ISimulator.Run(int barberCount, int customerCount, int chairCount)
        {
            Initialize(barberCount, customerCount, chairCount);

            IReadOnlyCollection<Task> barbers = SpawnBarbers(barberCount);
            IReadOnlyCollection<Task> customers = SpawnCustomers(customerCount);

            await Task.WhenAll(customers);
            CompleteCustomers();

            await Task.WhenAll(barbers);
            CompleteBarbers();

            //Task.WhenAll(customers).ContinueWith(t => CompleteCustomers());
            //Task.WhenAll(barbers).ContinueWith(t => CompleteBarbers());

            //Task.WaitAll(customers.Concat(barbers).ToArray());
        }

        private IReadOnlyCollection<Task> SpawnBarbers(int barberCount)
        {
            return Enumerable.Range(0, barberCount)
                .Select(i => Task.Run(() => BarberThread(i)))
                .ToArray();
        }

        private IReadOnlyCollection<Task> SpawnCustomers(int customerCount)
        {
            return Enumerable.Range(0, customerCount)
                .Select(i => Task.Run(() => 
                {
                    Thread.Sleep(i * _random.Next(500));
                    CustomerThread(i);
                }))
                .ToArray();
        }
    }
}