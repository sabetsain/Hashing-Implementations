using System.Numerics;

namespace ImplementationProject;

// Object to generate random numbers.
public class RandomDevice
{
    Random rnd;

    public RandomDevice(int seed)
    {
        rnd = new Random(seed);
    }
    
    public BigInteger RandomBigInteger(BigInteger max)
    {       // Random positive BigInteger less than max
        byte[] bytes = max.ToByteArray();
        BigInteger result;

        do
        {
            rnd.NextBytes(bytes);
            bytes[^1] &= 0x7F;  // Ensure positivity
            result = new BigInteger(bytes);
        } while (result >= max);

        return result;
    }
}