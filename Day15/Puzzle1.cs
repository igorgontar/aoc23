using static IOUtils;
using Utils.Profiler;

class Puzzle1
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        using var reader = new StreamReader(file);
        var line = reader.ReadLine();
        var codes = line.Split(',', splitOptions);
        
        long sum = codes.Select(c => HashKey(c)).Sum();
        return sum;
    }

    static int HashKey(string s)
    {
        int h = 0;
        foreach(var ch in s)
        {
            int c = (int)ch;
            h += c;
            h *= 17;
            h %= 256;      
        }
        return h;
    }
}
