
class Puzzle2
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        using var reader = new StreamReader(file);
        //string time = string.Concat(reader.ReadLine().Split(':', splitOptions)[1].Split(' ',splitOptions));
        long t = long.Parse(
            reader.ReadLine()
            .Split(':', splitOptions)[1]
            .Split(' ',splitOptions)
            .Aggregate((x,y) => x+y)
        );

        string dist = string.Concat(reader.ReadLine().Split(':', splitOptions)[1].Split(' ',splitOptions));
        long d = long.Parse(dist);

        var r = new Race { time = t , dist = d };
        long c = Algo.CalcNumberOfWays(r);
        return c;
    }
}
