using System;

namespace Modular
{
    internal static class Program
    {
        private static void Main()
        {
            //TestPow();

            Console.Write("Enter number of iterations for primality tests: ");
            int iterations = int.Parse(Console.ReadLine());

            // prime number
            TestPrimality(7, iterations);

            // Carmichael's number
            TestPrimality(561, iterations);

            // Composite number
            TestPrimality(133, iterations);
        }

        private static void TestPow()
        {
            Console.Write("Enter base: ");
            int @base = int.Parse(Console.ReadLine());

            Console.Write("Enter power: ");
            int power = int.Parse(Console.ReadLine());
            
            Console.Write("Enter modulo: ");
            int mod = int.Parse(Console.ReadLine());

            int result = ModularMath.Pow(@base, power, mod);
            Console.WriteLine($"{@base}^{power} (mod {mod}) = {result}");
        }

        private static void TestPrimality(int n, int iterations)
        {
            PrimalityTester tester = new PrimalityTester(iterations);

            Console.WriteLine($"Fermat test result for {n} is {tester.RunFermatTest(n)}");
            Console.WriteLine($"Miller-Rabin test result for {n} is {tester.RunMillerRabinTest(n)}");
        }
    }
}
