using Hashing;
using static Hashing.HashingFunctions;

class Exp
{
    static int L = Implentation.l;
    const int loops = 100;
    static int M = 1 << L;

    public static void Main()
    {
        var stream = Hashing.Stream.CreateStream(M - 10, L);

        var hash = new HashingTable(L, hashingFunctionModPrime);
        hash.SetUp(stream);

        uint S = SquareSums.SumOfSquares(hash, stream);

        List<ulong> estimates = new();
        for (int i = 0; i < loops; i++) {
            var g = new FourUniversalHash();
            var sketch = new CountSketch(L, g.Evaluate);
            sketch.ProcessStream(stream);
            estimates.Add(sketch.EstimateSecondMoment());
        }

        var sortest = estimates.OrderBy(x => x).ToList();
        double mse = sortest
            .Select(x => (double)x)
            .Select(x => Math.Pow(x - S, 2))
            .Sum() / loops;


        Console.WriteLine($"S: {S}");
        Console.WriteLine($"MSE: {mse}");

        Console.WriteLine("Est.:");
        for (int i = 0; i < loops; i++) {
            Console.WriteLine($"{sortest[i]}");
        }

        List<ulong> medians = new();
        for (int i = 0; i < 9; i++) {
            var group = estimates.Skip(i * 11).Take(11).ToList();
            group.Sort();
            medians.Add(group[5]);
        }

        medians.Sort();
        Console.WriteLine("\nMedianer:");
        for (int i = 0; i < medians.Count; i++) {
            Console.WriteLine($"{medians[i]}");
        }
    }
}


