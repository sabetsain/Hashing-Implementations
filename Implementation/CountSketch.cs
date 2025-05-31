using System;
using System.Numerics;
using System.Collections.Generic;

namespace Hashing
{
    // Opgave 4
    public class FourUniversalHash
    {
        BigInteger a0, a1, a2, a3;
        BigInteger p;
        Random rnd;

        public FourUniversalHash() {
            rnd = new Random();
            p = BigInteger.Pow(2, 89) - 1;
            a0 = RandomBigInteger(p);
            a1 = RandomBigInteger(p);
            a2 = RandomBigInteger(p);
            a3 = RandomBigInteger(p);
        }

        public BigInteger Evaluate(ulong x) {// Evaluere 3-grads hash per x modulo p
            BigInteger bx = new BigInteger(x);
            BigInteger result = (a3 * BigInteger.Pow(bx, 3) + a2 * BigInteger.Pow(bx, 2) + a1 * bx + a0) % p;
            return result;
        }

        BigInteger RandomBigInteger(BigInteger max) {// Tilfældig positiv BigInteger mindre end max
            byte[] bytes = max.ToByteArray();
            BigInteger result;

            do {
                rnd.NextBytes(bytes);
                bytes[^1] &= 0x7F; //Sikre at nummeret er positivt
                result = new BigInteger(bytes);
            } while (result >= max);

            return result;
        }
    }

    // Opgave 5
    public class CountSketchHash
    {
        int t;
        int m;
        Func<ulong, BigInteger> g;
        static BigInteger msignificant = BigInteger.One << 88; //Vigtigste bit (88)

        public CountSketchHash(int t, Func<ulong, BigInteger> g) {
            if (t < 0 || t > 64) throw new ArgumentOutOfRangeException(nameof(t));
            this.t = t;
            this.m = 1 << t;
            this.g = g;
        }

        public int H(ulong x) {
            BigInteger gx = g(x);
            return (int)(gx % m);
        }

        public int S(ulong x) {
            BigInteger gx = g(x);
            return gx >= msignificant ? -1 : 1;
        }
    }

    // Opgave 6
    public class CountSketch
    {
        int[] counters;
        CountSketchHash hash;

        public CountSketch(int l, Func<ulong, BigInteger> g) {
            int k = 1 << l;
            counters = new int[k];
            hash = new CountSketchHash(l, g);
        }

        public void ProcessStream(IEnumerable<Tuple<ulong, int>> stream) {
            foreach (var (x, delta) in stream) {
                int i = hash.H(x);
                int sign = hash.S(x);
                counters[i] += sign * delta;
            }
        }

        public ulong EstimateSecondMoment() {
            ulong estimate = 0;
            foreach (int c in counters)
            {
                estimate += (ulong)(c * c);
            }
            return estimate;
        }
    }
}
