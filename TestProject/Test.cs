using Hashing;
using System.Diagnostics;
namespace TestProject;

[TestClass]
public sealed class Test
{
    // Run the below tests using 'dotnet test --logger "console;verbosity=detailed"' in order to see the 
    // runtime of each test.
    public TestContext? TestContext { get; set; }
    
    // Opgave 3
    [TestMethod]
    public void TestModPrimeSquareSums()
    {
        var watch = Stopwatch.StartNew();
        HashingTable table = new HashingTable(Implentation.l, HashingFunctions.hashingFunctionModPrime);
        SquareSums.SumOfSquares(table);
        watch.Stop();
        var elapsed = watch.ElapsedMilliseconds;
        TestContext?.WriteLine($"Time elapsed: {elapsed}");
    }

    [TestMethod]
    public void TestShiftSquareSums()
    {
        var watch = Stopwatch.StartNew();
        HashingTable table = new HashingTable(Implentation.l, HashingFunctions.hashingFunctionShift);
        SquareSums.SumOfSquares(table);
        watch.Stop();
        var elapsed = watch.ElapsedMilliseconds;
        TestContext?.WriteLine($"Time elapsed: {elapsed}");
    }
}
