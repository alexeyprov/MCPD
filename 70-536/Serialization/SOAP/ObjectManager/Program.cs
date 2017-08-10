using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectManager
{
    class Program
    {
        // Define some fields in the class to test the ObjectWalker.
        public String name = "Fred";
        public Int32 Age = 40;

        static void Main()
        {
            // Build an object graph using an array that refers to various objects.
            Object[] data = new Object[] { "Jeff", 123, 555L, (Byte)35, new Program() };

            // Construct an ObjectWalker and pass it the root of the object graph.
            ObjectWalker ow = new ObjectWalker(data);

            // Enumerate all of the objects in the graph and count the number of objects.
            Int64 num = 0;
            foreach(Object o in ow)
            {
                // Display each object's type and value as a string.
                Console.WriteLine("Object #{0}: Type={1}, Value's string={2}",
                   num++, o.GetType(), o.ToString());
            }
        }
    }
}
