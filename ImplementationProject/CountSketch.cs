namespace ImplementationProject;

// Object for the count sketch algorithm. (Variable naming follows that from the notes (somewhat), NOT the problem statement).
public class CountSketch
{
    // double epsilon, delta;
    int k, l; // m;
    long[] Counters; //long[,] Counters;
    CountSketchHash CountSketchHasher;

    public CountSketch(int _l, int seed) //, int _m)
    {
        l = _l;
        k = 1 << l; // int k = (int) Math.Ceiling(4d / (epsilon * epsilon));
        //m = _m;     // int m = (int) Math.Ceiling(12 * Math.Log(1 / delta));
        CountSketchHasher = new CountSketchHash(l, seed);
        Counters = new long[k]; // Counters = new long[m,k];
    }

    public void ProcessStream(IEnumerable<Tuple<ulong, long>> stream)
    {
        foreach (var (x, delta) in stream)
        {
            Counters[CountSketchHasher.h(x)] += delta * CountSketchHasher.s(x);
            // for (int i = 0; i < m; i++)
            // {
            //     Counters[i, CountSketchHasher.h(x)] += delta * CountSketchHasher.s(x);
            // }
        }
    }

    public ulong EstimateSecondMoment()
    {
        ulong estimate = 0;
        foreach (long c in Counters)
        {
            estimate += (ulong) (c * c);
        }
        return estimate;
    }
}