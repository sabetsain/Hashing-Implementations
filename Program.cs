using System;
using Hashing;

namespace ImplenteringsProjekt
{
    class Program
    {
        public static void Main(string[] args)
        {
            ulong x = 0xa2efef76056b23e4; 
            Console.WriteLine(HashingFunctions.multiplyShift(x));
            Console.WriteLine(HashingFunctions.multiplyModPrime(x));
        }
    }
}
