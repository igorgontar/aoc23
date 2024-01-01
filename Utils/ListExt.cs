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

    public static IEnumerable<string> AllLines(this TextReader reader)
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
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
        
        // avoid looping for trivial edge cases 
        if (a1.Length == 0)
            return true; 

        if (a1.Length == 1)
            return a1[0] == a2[0]; 

        for (int i = 0; i < a1.Length; i++)
            if (a1[i] != a2[i])
                return false;
        return true;
    }

    public static bool inside(this int x, params int[] a)
    {
        if (a.Length == 0)
            return false;
        for (int i = 0; i < a.Length; i++)
            if (x == a[i])
                return true;
        return false;
    }

    public static bool inside(this char x, params char[] a)
    {
        if (a.Length == 0)
            return false;
        for (int i = 0; i < a.Length; i++)
            if (x == a[i])
                return true;
        return false;
    }
}

