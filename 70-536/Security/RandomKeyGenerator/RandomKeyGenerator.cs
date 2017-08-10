using System;
using System.Text;
using System.Security;
using System.Security.Cryptography;

class App 
{
  static void Main(string[] argv) 
  {
    int len = 128;
    if (argv.Length > 0)
    {
      len = int.Parse(argv[0]);
    }

    byte[] buff = new byte[len / 2];
    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
    rng.GetBytes(buff);
    Console.WriteLine(BitConverter.ToString(buff).Replace("-", String.Empty));
  }
}