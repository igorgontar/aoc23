    public static class ListExt
    {
        public static void Print<T>(this IEnumerable<T> list)
        {
            var s = string.Join(',', list);
            Console.WriteLine(s);
        }

        public static IEnumerable<string> NonEmptyLines(this TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    yield return line;
            }
        }
    }
