namespace Hashing
{
    // Opgave 3
    public class SquareSums
    {
        public static uint SumOfSquares(HashingTable hashingTable)
        {
            IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream((int)Math.Pow(2, 27), Implentation.l);
            uint sum = 0;
            foreach ((ulong key, int value) in stream)
            {
                sum += (uint)(hashingTable.Get(key) * hashingTable.Get(key));
            }
            return sum;
        }
    }
}