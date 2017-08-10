using System;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FibonacciHelper helper = new FibonacciHelper();

            int index;
            for (index = 1; index <= 20; ++index)
            {
                Console.WriteLine($"{index}: {helper.GetFibonacciNumber(index)}");
            }

            Console.WriteLine("=====");

            index = 1;
            foreach (int number in helper.GetFibonacciSequence(20))
            {
                Console.WriteLine($"{index++}: {number}");
            }
        }
    }
}
