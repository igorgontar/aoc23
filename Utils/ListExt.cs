public static class ListExt
{
    public static IEnumerable<string> NonEmptyLines(this TextReader reader)
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (!string.IsNullOrWhiteSpace(line))
                yield return line;
        }
    }

    public static bool eq(this int[] a1, params int[] a2)
    {
        return cmp(a1, a2);
    }

    public static bool cmp(int[] a1, params int[] a2)
    {
        if (a1.Length != a2.Length)
            return false;
        for (int i = 0; i < a1.Length; i++)
            if (a1[i] != a2[i])
                return false;
        return true;
    }

    public static bool iside(this int x, params int[] a)
    {
        if (a.Length == 0)
            return false;
        for (int i = 0; i < a.Length; i++)
            if (x == a[i])
                return true;
        return false;
    }
}

