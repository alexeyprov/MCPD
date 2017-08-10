using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

class SingletonSerializationTest
{
	static void Main()
	{
		DoTest(new MemoryStream(), new BinaryFormatter());
	}

	static void DoTest(Stream stream, IFormatter formatter) 
	{
	   // Create an array with multiple elements refering to 
	   // the one Singleton object
	   Singleton[] a1 = { 
	      Singleton.GetSingleton(), 
	      Singleton.GetSingleton() 
	   };

	   Console.WriteLine(
	      "Do both array elements refer to the same object? " + 
	      (a1[0] == a1[1]));     // Displays "True"

	   // Serialize the array elements
	   formatter.Serialize(stream, a1);

	   // Deserialize the array elements
	   stream.Position = 0;
	   Singleton[] a2 = (Singleton[]) formatter.Deserialize(stream);


	   Console.WriteLine(
	      "Do both array elements refer to the same object? " + 
	      (a2[0] == a2[1]));     // Displays "True"

	   Console.WriteLine(
	      "Do all  array elements refer to the same object? " + 
	      (a1[0] == a2[0]));     // Displays "True"
	}
}

