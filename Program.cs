using System;
using System.Numerics;
using System.Diagnostics;
using Hashing;

namespace ImplenteringsProjekt
{
    class Program
    {
        private static Random rnd = new Random();
        private static BigInteger p = BigInteger.Pow(2, 89) - 1;
        private static BigInteger aMP = BigInteger.Parse("F5A9CA34582AA4B080FBEA", System.Globalization.NumberStyles.HexNumber);
        private static BigInteger bMP = BigInteger.Parse("152FC6CDC1964D16E909972", System.Globalization.NumberStyles.HexNumber);      // From random bit generator
        private static int l = rnd.Next(64);
        private static ulong aMS = 0xf784b8c1be342f9f;                // From random bit generator

        public static void Main(string[] args)
        {
            // ulong x = 0xa2efef76056b23e4;
            IEnumerable<Tuple<ulong, int>> stream = HashingFunctions.CreateStream((int)Math.Pow(2, 16), 256);
            BigInteger finalSum = 0;

            var watchMP = System.Diagnostics.Stopwatch.StartNew();
            foreach (Tuple<ulong, int> key in stream)
            {
                finalSum += HashingFunctions.multiplyModPrime(key.Item1, aMP, bMP, l, p);
            }
            watchMP.Stop();
            var elapsedMP = watchMP.ElapsedMilliseconds;

            finalSum = 0;
            var watchMS = System.Diagnostics.Stopwatch.StartNew();
            foreach (Tuple<ulong, int> key in stream)
            {
                finalSum += HashingFunctions.multiplyShift(key.Item1, aMS, l);
            }
            watchMS.Stop();
            var elapsedMS = watchMS.ElapsedMilliseconds;

            Console.WriteLine($"Time elapsed for multiply shift hashing is: {elapsedMS}");
            Console.WriteLine($"Time elapsed for multiply mod prime hashing is: {elapsedMP}");
        }
    }
}
