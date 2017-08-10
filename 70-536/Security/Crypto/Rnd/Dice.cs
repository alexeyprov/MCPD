using System;
using System.Security.Cryptography;

public class Dice
{
	public static void Main()
	{
		const uint SIDES = 6;
		const int ATTEMPTS = 5;

		for (int i = 0; i < ATTEMPTS; ++i)
		{
			Console.WriteLine("Rolling...{0}", Roll(SIDES));
		}
	}

	private static uint Roll(uint sides)
	{              
		RandomNumberGenerator rng = new RNGCryptoServiceProvider();
		byte[] buf = new byte[4];

		rng.GetBytes(buf);
		uint retval = BitConverter.ToUInt32(buf, 0);
		return (retval % sides) + 1;
	}
}