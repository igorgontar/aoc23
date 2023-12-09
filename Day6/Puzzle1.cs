class Race
{
    public long time;
    public long dist;
}

class Algo
{
    public static long CalcNumberOfWays(Race r)
    {
        long count = 0;
        for(long i=0; i<r.time; i++)
        {
            long speed = i;
            long t = r.time - i;
            long d = t * speed;
            if(d > r.dist)
                count++; 
        }
        return count;
    }
}

class Puzzle1
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        using var reader = new StreamReader(file);
        long[] times = reader.ReadLine().Split(':', splitOptions)[1].Split(' ',splitOptions).Select(s => long.Parse(s)).ToArray();
        long[] distances = reader.ReadLine().Split(':', splitOptions)[1].Split(' ',splitOptions).Select(s => long.Parse(s)).ToArray();
        Race[] races = times.Select((t,i) => new Race {time=t, dist=distances[i]}).ToArray();
        long mult = races.Select(r => Algo.CalcNumberOfWays(r)).Aggregate((x,y) => x*y);
        return mult;
    }
}
