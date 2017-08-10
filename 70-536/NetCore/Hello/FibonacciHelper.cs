using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    internal sealed class FibonacciHelper
    {
        public int GetFibonacciNumber(int ordinal)
        {
            if (ordinal < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(ordinal));
            }

            int last = 1;
            int nextToLast = 1;

            for (int index = 3; index <= ordinal; ++index)
            {
                int temp = last;
                last += nextToLast;
                nextToLast = temp;
            }

            return last;
        }

        public IEnumerable<int> GetFibonacciSequence(int size)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            int[] sequence = new int[size];

            for (int index = 0; index < Math.Min(2, size); ++index)
            {
                sequence[index] = 1;
            }

            for (int index = 2; index < size; ++index)
            {
                sequence[index] = sequence[index - 1] + sequence[index - 2];
            }

            return sequence;
        }
    }
}