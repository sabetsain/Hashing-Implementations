using System.Numerics;
using System.Diagnostics;

namespace Hashing
{
    // Opgave 1
    public class Implentation
    {
        // public static Random rnd = new Random();
        public static BigInteger p = BigInteger.Pow(2, 89) - 1;
        public static BigInteger aMP = BigInteger.Parse("F5A9CA34582AA4B080FBEA", System.Globalization.NumberStyles.HexNumber);
        public static BigInteger bMP = BigInteger.Parse("152FC6CDC1964D16E909972", System.Globalization.NumberStyles.HexNumber);      // From random bit generator
        public static int l = 20;                        // This 'l' variable can be used consistently through the project
        public static ulong aMS = 0xf784b8c1be342f9f;               // From random bit generator

        // The below Main function makes runtime comparisons as required by Opgave 1.
        // Note that currently, one hashing function uses BigIntegers more than the other, likely causing
        // a significant difference in runtimes.
        public static void SomeTests(string[] args)
        {
            IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream((int)Math.Pow(2, 16), 256);
            BigInteger finalSum = 0;

            var watchMP = Stopwatch.StartNew();
            foreach (Tuple<ulong, int> key in stream)
            {
                finalSum += HashingFunctions.multiplyModPrime(key.Item1, aMP, bMP, l, p);
            }
            watchMP.Stop();
            var elapsedMP = watchMP.ElapsedMilliseconds;

            finalSum = 0;
            var watchMS = Stopwatch.StartNew();
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
