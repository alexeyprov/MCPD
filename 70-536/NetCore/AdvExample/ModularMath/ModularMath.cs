using System;

namespace Modular
{
    public static class ModularMath
    {
        public static int Pow(int @base, int power, int mod)
        {
            if (power < 0)
            {
                throw new ArgumentException(nameof(power));
            }
            else if (power == 0)
            {
                return 1;
            }

            if (mod <= 0)
            {
                throw new ArgumentException(nameof(mod));
            }
            else if (mod == 1)
            {
                return (int)Math.Pow(@base, power);
            }

            if (@base == 0)
            {
                return 0;
            }

            int product = 1;
            int current = @base;
            for (int bit = 0; bit < 31; ++bit, power >>= 1)
            {
                if ((power & 0x01) == 0x01)
                {
                    product *= current;
                    product %= mod;
                }

                current *= current;
                current %= mod;
            }

            return product;
        }
    }
}