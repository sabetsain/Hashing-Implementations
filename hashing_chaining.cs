using System;
using System.Numerics;

namespace Hashing
{
    public static class HashingFunctions
    {
        private static Random rndShift = new Random();
        private static Random rndModPrime = new Random();
        public static BigInteger modulo(BigInteger x, BigInteger p, int q) {
            BigInteger y = (x & p) + (x >> q);
            if (y >= p) y -= p;
            return y;
        }

        public static ulong multiplyShift(BigInteger x)
        {
            int l = rndShift.Next(64);
            ulong a = 0xf784b8c1be342f9f;                // From random bit generator
            return (ulong)((a * x) >> (64 - l));
        }

        public static BigInteger multiplyModPrime(BigInteger x)
        {
            BigInteger p = BigInteger.Pow(2, 89) - 1;
            int l = rndModPrime.Next(64);
            BigInteger a = BigInteger.Parse("F5A9CA34582AA4B080FBEA", System.Globalization.NumberStyles.HexNumber);
            BigInteger b = BigInteger.Parse("152FC6CDC1964D16E909972", System.Globalization.NumberStyles.HexNumber);      // From random bit generator

            x *= a;
            x += b;
            return ((modulo(x, p, 89)) % BigInteger.Pow(2, l));
        }
    }
}