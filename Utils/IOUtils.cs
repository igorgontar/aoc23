public static class IOUtils
{
    static readonly TextWriter log = Console.Out;

    public static void print(string format, params object[] args)
    {
        log.Write(format, args);
    }

    public static void println(string format, params object[] args)
    {
        log.WriteLine(format, args);
    }

    public static void println<T>(IEnumerable<T> list, string separator = ",") where T : struct
    {
        if (separator.IndexOf('{') >= 0)
        {
            // treat it as format string
            foreach (var x in list)
                print(separator, x);
            println("");
        }
        else
        {
            var s = string.Join(separator, list);
            log.WriteLine(s);
        }
    }

    public static void println<T>(IEnumerable<IEnumerable<T>> grid, string colsep = ",") where T : struct
    {
        foreach (var row in grid)
            println(row, colsep);
    }
}