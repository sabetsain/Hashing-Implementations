namespace ImplementationProject;

public class HashingTable
{
    int size;
    MultiplyShift hashFunc;
    List<Tuple<ulong, long>>[] table;

    // Defining the hash table structure.
    public HashingTable(int l)
    {
        size = 1 << l;
        hashFunc = new MultiplyShift(0xf784b8c1be342f9f, l);
        table = new List<Tuple<ulong, long>>[size];
        for (int i = 0; i < size; i++)
        {
            table[i] = new List<Tuple<ulong, long>>();
        }
    }

    // The below three functions work as described in Opgave 2.
    public long Get(ulong x)
    {
        ulong index = hashFunc.Hash(x);
        foreach (var node in table[index])
        {
            if (node.Item1 == x)
            {
                return node.Item2;
            }
        }
        return 0;
    }

    public void Set(ulong x, long v)
    {
        ulong index = hashFunc.Hash(x);
        for (int i = 0; i < table[index].Count; i++)
        {
            if (table[index][i].Item1 == x)
            {
                table[index][i] = new Tuple<ulong, long>(x, v);
                return;
            }
        }
        table[index].Add(new Tuple<ulong, long>(x, v));
    }

    public void Increment(ulong x, long d)
    {
        ulong index = hashFunc.Hash(x);
        for (int i = 0; i < table[index].Count; i++)
        {
            if (table[index][i].Item1 == x)
            {
                table[index][i] = new Tuple<ulong, long>(x, table[index][i].Item2 + d);
                return;
            }
        }
        table[index].Add(new Tuple<ulong, long>(x, d));
    }

    // This function inserts a stream created by the code given in the assignment and uses the above 2 methods
    // to set up a hash table given an array of key-value pairs.
    public void SetUp(IEnumerable<Tuple<ulong, long>> stream)
    {
        foreach ((ulong key, long value) in stream)
        {
            Increment(key, value);
        }
    }
}