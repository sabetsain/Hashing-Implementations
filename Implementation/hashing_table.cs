using System.Numerics;

namespace Hashing
{
    // Opgave 2
    public class HashingTable
    {
        int size;
        Func<ulong, BigInteger> hashFunc;
        List<Tuple<ulong, int>>[] table;

        // Defining the hash table structure.
        public HashingTable(int l, Func<ulong, BigInteger> h)
        {
            this.size = (int)Math.Pow(2, l);
            this.hashFunc = h;
            table = new List<Tuple<ulong, int>>[size];
            for (int i = 0; i < size; i++)
            {
                table[i] = new List<Tuple<ulong, int>>();
            }
        }

        // The below three functions work as described in Opgave 2.
        public int Get(ulong x)
        {
            uint index = (uint)hashFunc(x);
            foreach (var node in table[index])
            {
                if (node.Item1 == x)
                {
                    return node.Item2;
                }
            }
            return 0;
        }

        public void Set(ulong x, int v)
        {
            uint index = (uint)hashFunc(x);
            for (int i = 0; i < table[index].Count; i++)
            {
                if (table[index][i].Item1 == x)
                {
                    table[index][i] = new Tuple<ulong, int>(x, v);
                    return;
                }
            }
            table[index].Add(new Tuple<ulong, int>(x, v));
        }

        public void Increment(ulong x, int d)
        {
            uint index = (uint)hashFunc(x);
            for (int i = 0; i < table[index].Count; i++)
            {
                if (table[index][i].Item1 == x)
                {
                    table[index][i] = new Tuple<ulong, int>(x, table[index][i].Item2 + d);
                    return;
                }
            }
            table[index].Add(new Tuple<ulong, int>(x, d));
        }

        // This function inserts a stream created by the code given in the assignment and uses the above 2 methods
        // to set up a hash table given an array of key-value pairs.
        public void SetUp(IEnumerable<Tuple<ulong, int>> stream)
        {
            foreach ((ulong key, int value) in stream)
            {
                uint index = (uint)hashFunc(key);
                bool found = false;

                for (int i = 0; i < table[index].Count; i++)
                {
                    if (table[index][i].Item1 == key)
                    {
                        Increment(key, value);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Set(key, value);
                }
            }
        }
    }
}