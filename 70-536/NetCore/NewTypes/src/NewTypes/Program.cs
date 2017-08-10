using System;
using System.Collections.Generic;

using Pets;

namespace ConsoleApplication
{
    internal static class Program
    {
        private static void Main()
        {
            IEnumerable<IPet> pets = new IPet[] 
            {
                new Cat(),
                new Dog()
            };

            foreach (IPet pet in pets)
            {
                Console.WriteLine(pet.TalkToOwner());
            }
        }
    }
}