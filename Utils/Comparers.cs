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