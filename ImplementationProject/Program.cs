using System.Numerics;
using System.Diagnostics;

namespace ImplementationProject;

class Program
{
    static IEnumerable<Tuple<ulong, long>> TestStream;

    // Interface
    static void Main()
    {
        int seed = 1729;
        TestStream = Stream.CreateStream((int)Math.Pow(2, 16), 20, seed);

        int l;
        while (true)
        {
            Console.WriteLine("What should be run?");
            string? ProgramToRun = Console.ReadLine();
            switch (ProgramToRun)
            {
                case "compare hash functions":
                    Console.WriteLine("Value for l:");
                    l = int.Parse(Console.ReadLine()!);
                    CompareHashingFunctions(l);
                    break;

                case "sum of squares":
                    Console.WriteLine("Value for l:");
                    l = int.Parse(Console.ReadLine()!);
                    SumOfSquares(l);
                    break;

                case "count sketch":
                    Console.WriteLine("Value for l:");
                    l = int.Parse(Console.ReadLine()!);
                    TestCountSketch(l);
                    break;

                case "X":
                    return;

                default:
                    Console.WriteLine("Unknown function.");
                    break;
            }
            Console.WriteLine();
        }
    }

    // Algorithm to compare the multiplyshift and multiply mod prime hash functions.
    static void CompareHashingFunctions(int l)
    {
        ulong aMS = 0xf784b8c1be342f9f;
        MultiplyShift MultiplyShiftHasher = new MultiplyShift(aMS, l);

        BigInteger aMMP = BigInteger.Parse("F5A9CA34582AA4B080FBEA", System.Globalization.NumberStyles.HexNumber);
        BigInteger bMMP = BigInteger.Parse("152FC6CDC1964D16E909972", System.Globalization.NumberStyles.HexNumber);
        MultiplyModPrime MultiplyModPrimeHasher = new MultiplyModPrime(aMMP, bMMP, l);

        var watchMS = Stopwatch.StartNew();
        foreach (var (key, val) in TestStream)
        {
            MultiplyShiftHasher.Hash(key);
        }
        watchMS.Stop();
        var elapsedMS = watchMS.ElapsedMilliseconds;

        var watchMMP = Stopwatch.StartNew();
        foreach (var (key, val) in TestStream)
        {
            MultiplyModPrimeHasher.Hash(key);
        }
        watchMMP.Stop();
        var elapsedMMP = watchMMP.ElapsedMilliseconds;

        Console.WriteLine($"Time elapsed for multiply shift hashing is: {elapsedMS}");
        Console.WriteLine($"Time elapsed for multiply mod prime hashing is: {elapsedMMP}");
    }

    // Algorithm to calculate the square sum S, using our hash tabel.
    static ulong SumOfSquares(int l)
    {
        var watchSS = Stopwatch.StartNew();
        HashingTable hashingTable = new HashingTable(l);
        hashingTable.SetUp(TestStream);
        ulong sum = 0;
        foreach ((ulong key, long value) in TestStream)
        {
            sum += (ulong)(hashingTable.Get(key) * hashingTable.Get(key));
            hashingTable.Set(key, 0);
        }
        watchSS.Stop();
        var elapsedSS = watchSS.ElapsedMilliseconds;
        Console.WriteLine($"Time elapsed for sum of squares for l={l}: {elapsedSS}");
        return sum;
    }

    // Algorithm to test the our count sketch approximation of the square sum S.
    static void TestCountSketch(int l)
    {
        ulong actualSumOfSquares = SumOfSquares(l);
        Console.WriteLine($"Actual sum of squares calculated with hash table: {actualSumOfSquares}");

        ulong[] X_list = new ulong[100];

        var watchCS = Stopwatch.StartNew();
        for (int i = 0; i < 100; i++)
        {
            CountSketch CountSketchObject = new CountSketch(l, i);
            CountSketchObject.ProcessStream(TestStream);
            X_list[i] = CountSketchObject.EstimateSecondMoment();
        }
        watchCS.Stop();
        var elapsedCS = watchCS.ElapsedMilliseconds;
        Console.WriteLine($"Time elapsed for 100 repetitions of count sketch is: {elapsedCS}");

        double EX = 0;
        double mean_square_error = 0;
        for (int i = 0; i < 100; i++)
        {
            EX += X_list[i];
            mean_square_error += (X_list[i] - actualSumOfSquares) * (X_list[i] - actualSumOfSquares);
        }
        EX /= 100D;
        mean_square_error /= 100D;
        Console.WriteLine($"E[X]: {EX}");
        Console.WriteLine($"Mean square error: {mean_square_error}");
        double m = 1 << l;
        Console.WriteLine($"Forventet mean square error: {2*actualSumOfSquares*actualSumOfSquares/m}");

        ulong[][] G = new ulong[9][];
        ulong[] M = new ulong[9];
        int ctr = 0;
        for (int i = 0; i < 9; i++)
        {
            G[i] = new ulong[11];
            for (int j = 0; j < 11; j++)
            {
                G[i][j] = X_list[ctr++];
            }
            Array.Sort(G[i]);
            M[i] = G[i][5];
        }
        Array.Sort(M);
        for (int i = 0; i < 9; i++)
        {
            Console.WriteLine(M[i]);
        }
    }
}