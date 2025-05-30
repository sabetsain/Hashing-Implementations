using System.Numerics;

namespace Hashing
{
    public class CountSketch
    {
        int t;
        Func<ulong, int> s;
        Func<ulong, BigInteger> h;
        int m;
        BigInteger[] c;
        // double k;

        // We're supposed to give hash functions 'h' and 's' to count sketch as arguments, though since we never 
        // implement these functions but rather, a method that returns the results of these functions, I decided to give
        // a hash function 'g' as an argument, that then gets split in the class into two hash functions, 'h' and 's'.
        public CountSketch(Func<ulong, BigInteger> g)
        {
            this.h = (ulong x) => HashingFunctions.splitHashFunction(g, t, x).Item1;
            this.s = (ulong x) => HashingFunctions.splitHashFunction(g, t, x).Item2;
            this.m = (int)Math.Pow(2, t);
            this.c = new BigInteger[(int)m];
            for (int i = 0; i < m; i++)
            {
                c[i] = 0;
            }
        }

        public void cs_process(ulong key, int value)
        {
            c[(ulong)h(key)] += s(key) * value;
        }

        public int cs_query(ulong x)
        {
            return (int)(s(x) * c[(int)h(x)]);
        }


    }
}