using System.Numerics;

namespace Hashing
{
    // Opgave 1
    public static class HashingFunctions
    {
        // This uses the logic from the second problem in week 3's assignment to write an efficient
        // modulo function.
        public static BigInteger modulo(BigInteger x, BigInteger p, int q)
        {
            BigInteger y = (x & p) + (x >> q);
            if (y >= p) y -= p;
            return y;
        }

        // Implementing multiply shift hash function
        public static ulong multiplyShift(ulong x, ulong a, int l)
        {
            return (a * x) >> (64 - l);
        }

        // Implementing multiply mod prime hash function
        public static BigInteger multiplyModPrime(ulong x, BigInteger a, BigInteger b, int l, BigInteger p)
        {
            BigInteger axb = (x * a) + b;
            return modulo(axb, p, 89) % BigInteger.Pow(2, l);
        }

        // The two hash functions below establish arguments for the above two methods.
        // These can be used as arguments in the Hash Table implentation.
        public static BigInteger hashingFunctionModPrime(ulong x)
        {
            return multiplyModPrime(x, Implentation.aMP, Implentation.bMP, Implentation.l, Implentation.p);
        }

        public static BigInteger hashingFunctionShift(ulong x)
        {
            return multiplyShift(x, Implentation.aMS, Implentation.l);
        }
    }
}