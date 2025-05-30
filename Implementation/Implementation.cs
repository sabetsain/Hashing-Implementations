using System.Numerics;
using System.Diagnostics;

namespace Hashing
{
    // Opgave 1
    public class Implentation
    {
        // The below Main function makes runtime comparisons as required by Opgave 1.
        // Note that currently, one hashing function uses BigIntegers more than the other, likely causing
        // a significant difference in runtimes.
        public static int l;                        // This 'l' variable can be used consistently through the project

        public static void Main(string[] args)
        {
            l = 20;
            IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream((int)Math.Pow(2, 16), 256);
            BigInteger finalSum = 0;

            var watchMP = Stopwatch.StartNew();
            foreach (Tuple<ulong, int> key in stream)
            {
                finalSum += HashingFunctions.multiplyModPrime(key.Item1, HashingFunctions.aMP,
                                                                         HashingFunctions.bMP,
                                                                         l,
                                                                         HashingFunctions.p);
            }
            watchMP.Stop();
            var elapsedMP = watchMP.ElapsedMilliseconds;

            finalSum = 0;
            var watchMS = Stopwatch.StartNew();
            foreach (Tuple<ulong, int> key in stream)
            {
                finalSum += HashingFunctions.multiplyShift(key.Item1, HashingFunctions.aMS,
                                                                      l);
            }
            watchMS.Stop();
            var elapsedMS = watchMS.ElapsedMilliseconds;

            Console.WriteLine($"Time elapsed for multiply shift hashing is: {elapsedMS}");
            Console.WriteLine($"Time elapsed for multiply mod prime hashing is: {elapsedMP}");

            // for (int i = 0; i <= 25; i++)
            // {
            //     l = i;
            //     var watchSumsSquaredShift = Stopwatch.StartNew();
            //     HashingTable table = new HashingTable(l, HashingFunctions.hashingFunctionShift);
            //     SquareSums.SumOfSquares(table);
            //     watchSumsSquaredShift.Stop();
            //     var elapsedSumsSquaredShift = watchSumsSquaredShift.ElapsedMilliseconds;
            //     Console.WriteLine($"Time elapsed for l={i}: {elapsedSumsSquaredShift}");
            // }

            l = 26;
            var watchSumsSquaredShift = Stopwatch.StartNew();
            HashingTable table = new HashingTable(l, HashingFunctions.hashingFunctionShift);
            SquareSums.SumOfSquares(table);
            watchSumsSquaredShift.Stop();
            var elapsedSumsSquaredShift = watchSumsSquaredShift.ElapsedMilliseconds;
            Console.WriteLine($"Time elapsed for l={26}: {elapsedSumsSquaredShift}");
            
        }
    }
}
