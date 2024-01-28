using static IOUtils;

public class BitCombinations
{
    readonly ISet<int[]> set = new HashSet<int[]>(IntArrayComparer.Instance);
    readonly int[] a = new int[8];

    public BitCombinations(int numBits)
    {
        a = new int[numBits];
    }

    void ThreeBits()
    {
        var bits = new[] { 0, 1 };
        foreach (var a in bits)
            foreach (var b in bits)
                foreach (var c in bits)
                    println(new[] { a, b, c });
    }


    bool walk(int bit, int level)
    {
        if (level >= a.Length)
        {
            set.Add((int[])a.Clone());
            println(a);
            return false;
        }

        a[level] = bit;
        if (!walk(0, level + 1))
            return true;

        var ret = walk(1, level + 1);
        return ret;
    }

    void ManyBits(int numBits)
    {
        walk(0, 0);
        walk(1, 0);

        println("\nset count={0}:", set.Count);
        foreach (var e in set)
            println(e);
    }

    public static void Run()
    {
        BitCombinations algo = new(3);

        println($"====== {algo.GetType().Name} =====");

        println($"--- {nameof(ThreeBits)} ---");
        algo.ThreeBits();

        int n = 8;
        algo = new(n);
        println($"--- {nameof(ManyBits)}({n}) ---");
        algo.ManyBits(n);
    }
}