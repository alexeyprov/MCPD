using System;

class MetaDataEx
{
    static void Main()
	{
		string s = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
		Console.WriteLine(s);
	}
}