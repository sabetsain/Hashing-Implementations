namespace Hashing
{
    // Opgave 3
    public class SquareSums
    {
        public static uint SumOfSquares(HashingTable hashingTable, IEnumerable<Tuple<ulong, int>> stream)
        {
            uint sum = 0;
            foreach ((ulong key, int value) in stream)
            {
                int freq = hashingTable.Get(key);
                sum += (uint)(freq * freq);
            }
            return sum;
        }
    }
}