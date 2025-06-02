using System.Numerics;

namespace ImplementationProject;

// Object to perform multiply shift hashing.
public class MultiplyShift
{
    // Initialising of the object.
    static ulong a;
    static int l;
    public MultiplyShift(ulong _a, int _l)
    {
        a = _a;
        l = _l;
    }

    // The actual hashing.
    public ulong Hash(ulong x)
    {
        return (a * x) >> (64 - l);
    }
}

// Object to perform multiply mod prime hashing.
public class MultiplyModPrime
{
    // Initialisation of the object.
    static BigInteger a, b, p;
    static int q, l, mod_l;

    public MultiplyModPrime(BigInteger _a, BigInteger _b, int _l)
    {
        a = _a; b = _b; q = 89; l = _l;
        p = (BigInteger.One << q) - 1;
        mod_l = (1 << l) - 1;
    }

    // The actual hashing.
    public BigInteger Hash(BigInteger x)
    {
        x = (a * x) + b;
        x = (x & p) + (x >> q); // mod p perfomed efficiently using the result
        if (x >= p) x -= p;     // in week 3's assignment.
        return x & mod_l;       // mod 2^l perfomed effeciently by & with 2^l-1.
    }
};

// Object to perform the four universal hashing used for the count sketch algorithm.
public class CountSketchHash
{
    static BigInteger[] A = new BigInteger[4];
    static BigInteger p, bit_q;
    static int q, l, mod_l;
    public CountSketchHash(int _l, int seed)
    {
        q = 89;
        p = (BigInteger.One << q) - 1;
        bit_q = BigInteger.One << (q - 1);
        RandomDevice rng = new RandomDevice(seed);
        for (int i = 0; i < 4; i++)
        {
            A[i] = rng.RandomBigInteger(p);
        }
        l = _l;
        mod_l = (1 << l) - 1;
    }

    // Helper function to do efficient modular arithmetic mod Mersenne primes.
    BigInteger Mod_p(BigInteger x)
    {       // Essentially still just the result from week 3's assignment, but expanded slightly so it works for numbers much larger 
        while (x > p)
            x = (x & p) + (x >> q);
        return x;
    }

    // The actual hashing.
    public ulong h(BigInteger x)
    {       // Evaluate 3rd degree polynomial g(x) modulo p
        BigInteger g = 0;
        for (int i = 3; i >= 0; i--)
        {
            g = Mod_p(g * x + A[i]);
        }
        return (ulong) (g & mod_l);
    }

    public long s(BigInteger x)
    {       // Evaluate 3rd degree polynomial g(x) modulo p
        BigInteger g = 0;               // A little annoying that we have to calculate g(x) again. Perhaps we
        for (int i = 3; i >= 0; i--)    // should have implemented the h(x) and s(x) as one combined function,
        {                               // or even better just gotten a random bit in a simpler way (thus
            g = Mod_p(g * x + A[i]);    // deviating from the instructions in opgave 5).
        }
        return g >= bit_q ? -1 : 1;     // 2^q-1 floor division avoided by comparing with bit_q
    }
}