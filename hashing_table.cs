using System.Numerics;

namespace Hashing
{

    public class HashingTable
    {
        int size;
        Func<ulong, BigInteger> hashFunc;
        List<Tuple<ulong, ulong>>[] table;

        public HashingTable(int l, Func<ulong, BigInteger> h)
        {
            this.size = (int)Math.Pow(2, l);
            this.hashFunc = h;
            table = new List<Tuple<ulong, ulong>>[size];
        }

        public BigInteger hashingFunction(ulong x)
        {
            BigInteger a = BigInteger.Parse("F5A9CA34582AA4B080FBEA", System.Globalization.NumberStyles.HexNumber);
            BigInteger b = BigInteger.Parse("152FC6CDC1964D16E909972", System.Globalization.NumberStyles.HexNumber);      // From random bit generator
            BigInteger p = BigInteger.Pow(2, 89) - 1;

            return Hashing.HashingFunctions.multiplyModPrime(x, a, b, size, p);
        }

        public ulong Get(ulong x)
        {
            if (table[(uint)hashFunc(x)] != null)
            {
                int i = 0;
                while (table[(uint)hashFunc(x)][i] != null)
                {
                    if (table[(uint)hashFunc(x)][i].Item1 == x)
                    {
                        return table[(uint)hashFunc(x)][i].Item2;
                    }
                }
            }
            return 0;
        }

        public void Set(ulong x, ulong v)
        {
            int i = 0;
            while (table[(int)hashFunc(x)][i] != null)
            {
                if (table[(uint)hashFunc(x)][i].Item1 == x)
                {
                    table[(uint)hashFunc(x)][i] = new Tuple<ulong, ulong>(x, v);
                    return;
                }
                i++;
            }
            table[(uint)hashFunc(x)].Add(new Tuple<ulong, ulong>(x, v));
        }

        public void Increment(ulong x, ulong d)
        {
            int i = 0;
            while (table[(int)hashFunc(x)][i] != null)
            {
                if (table[(uint)hashFunc(x)][i].Item1 == x)
                {
                    table[(uint)hashFunc(x)][i] = new Tuple<ulong, ulong>(x, table[(uint)hashFunc(x)][i].Item2 + d);
                    return;
                }
                i++;
            }
            table[(uint)hashFunc(x)].Add(new Tuple<ulong, ulong>(x, d));
        }
    }
}