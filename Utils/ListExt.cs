    public static class ListExt
    {
        public static void Print<T>(this IEnumerable<T> list)
        {
            var s = string.Join(',', list);
            Console.WriteLine(s);
        }
    }
