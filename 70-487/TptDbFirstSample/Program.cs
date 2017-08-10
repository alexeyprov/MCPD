using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDataAccessComponents;

namespace TptDbFirstSample
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (SchoolEntities context = new SchoolEntities())
            {
                OutputPersons(context.People);
                OutputPersons(context.People.OfType<Instructor>());
                OutputPersons(context.People.OfType<Student>());
            }
        }

        private static void OutputPersons<T>(IEnumerable<T> persons) where T : Person
        {
            Console.WriteLine("============================");

            foreach (T person in persons)
            {
                Console.WriteLine("{0}: {1} {2}", typeof(T).Name, person.FirstName, person.LastName);
            }

            Console.ReadLine();
        }
    }
}
