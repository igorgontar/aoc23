public class RecordComparer<T> : IEqualityComparer<T>
{
    readonly Func<T, T, bool> compare;
    
    public RecordComparer(Func<T, T, bool> compare)
    {
        this.compare = compare;

    }
    public bool Equals(T x, T y)
    {
        return compare(x, y);
    }

    public int GetHashCode(T obj)
    {
        return obj.GetHashCode();
    }
}

public class IntArrayComparer : IEqualityComparer<int[]>
{
    public static readonly IntArrayComparer Instance = new ();
    
    public bool Equals(int[] x, int[] y)
    {
        if (x.Length != y.Length)
        {
            return false;
        }
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] != y[i])
            {
                return false;
            }
        }
        return true;
    }

    public int GetHashCode(int[] obj)
    {
        int result = 17;
        for (int i = 0; i < obj.Length; i++)
        {
            unchecked
            {
                result = result * 23 + obj[i];
            }
        }
        return result;
    }
}
