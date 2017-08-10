using System;
using System.Diagnostics;

namespace Modular
{
    public sealed class PrimalityTester
    {
        private readonly int _iterations;
        private readonly Random _random;

        public PrimalityTester(int iterations = 10)
        {
            if (iterations < 1 || iterations > 100)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(iterations),
                    "Number of iterations should be between 1 and 100");
            }

            _iterations = iterations;
            _random = new Random();
        }

        public bool RunFermatTest(int n)
        {
            if (!VerifyTestParameters(n))
            {
                return false;
            }

            for (int iteration = 0; iteration < _iterations; ++iteration)
            {
                int @base = _random.Next(n - 2) + 2;
                Debug.WriteLine($">> Fermat test iteration for {n} with base {@base}");

                if (ModularMath.Pow(@base, n - 1, n) != 1)
                {
                    return false;
                }    
            }

            return true;
        }

        public bool RunMillerRabinTest(int n)
        {
            if (!VerifyTestParameters(n))
            {
                return false;
            }

            // factor n - 1 into odd and even parts
            int oddPart = 1, squarings = 0;
            for (int current = n - 1; current != 0; current >>= 1, ++squarings)
            {
                if ((current & 0x01) == 0x01)
                {
                    oddPart = current;
                    break;
                }
            }
            
            for (int iteration = 0; iteration < _iterations; ++iteration)
            {
                int @base = _random.Next(n - 2) + 2;
                if (!DoSingleMillerRabinIteration(@base, n, oddPart, squarings))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool VerifyTestParameters(int n)
        {
            if (n <= 2)
            {
                throw new ArgumentException(nameof(n));
            }

            return n % 2 != 0;
        }

        private static bool DoSingleMillerRabinIteration(int @base, int n, int initialExp, int squarings)
        {
            Debug.WriteLine($">> Miller-Rabin test iteration for {n} with base {@base}");
            int pow = ModularMath.Pow(@base, initialExp, n);

            if (pow == 1)
            {
                return true;
            }

            for (int counter = 0, previous = pow; counter < squarings; ++counter, previous = pow)
            {
                pow = ModularMath.Pow(previous, 2, n);
                if (pow == 1 && previous != 1 && previous != n - 1)
                {
                    return false;
                }    
            }
            
            return pow == 1;
        }
    }
}
