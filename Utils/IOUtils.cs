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

    public static void println<T>(this IEnumerable<T> list)
    {
        var s = string.Join(',', list);
        log.WriteLine(s);
    }
}