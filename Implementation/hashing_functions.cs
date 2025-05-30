using System.Numerics;

namespace Hashing
{
    // Opgave 1
    public static class HashingFunctions
    {
        // public static Random rnd = new Random();
        public static BigInteger p = BigInteger.Pow(2, 89) - 1;
        public static BigInteger aMP = BigInteger.Parse("F5A9CA34582AA4B080FBEA", System.Globalization.NumberStyles.HexNumber);
        public static BigInteger bMP = BigInteger.Parse("152FC6CDC1964D16E909972", System.Globalization.NumberStyles.HexNumber);      // From random bit generator
        public static ulong aMS = 0xf784b8c1be342f9f;               // From random bit generator
        public static BigInteger a0 = BigInteger.Parse("6064e652c076e60498a9de", System.Globalization.NumberStyles.HexNumber);
        public static BigInteger a1 = BigInteger.Parse("6b3bb7e24036c643b56a50", System.Globalization.NumberStyles.HexNumber);
        public static BigInteger a2 = BigInteger.Parse("9710c1a84a940087eaf5d1", System.Globalization.NumberStyles.HexNumber);
        public static BigInteger a3 = BigInteger.Parse("f4ab9a49179fa2ff4fb5d8", System.Globalization.NumberStyles.HexNumber);
        
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
            return multiplyModPrime(x, aMP, bMP, Implentation.l, p);
        }

        public static BigInteger hashingFunctionShift(ulong x)
        {
            return multiplyShift(x, aMS, Implentation.l);
        }

        // Opgave 4
        // Must have the 'a' values as arguments, and is essentially just an implementation of Algorithm 1 from
        // the second moment notes.
        // b = 89, since we are using the format p = 2^b - 1.
        public static BigInteger fourUniversalHashing(ulong x, BigInteger a0, BigInteger a1, BigInteger a2, BigInteger a3)
        {
            BigInteger y = a3;
            foreach (var a in (BigInteger[])[a2, a1, a0])
            {
                y = (y * x) + a;
                y = (y & p) + (y >> 89);
            }
            if (y >= p) y -= p;
            return y % 4;
        }

        // Opgave 5
        public static (BigInteger, int) splitHashFunction(Func<ulong, BigInteger> g, int t, ulong x)
        {
            BigInteger gX = g(x);
            BigInteger hX = gX & (t-1);
            BigInteger bX = gX >> 88;
            int sX = 1 - (2 * (int)bX);

            return (hX, sX);
        }
    }
}