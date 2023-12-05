using Utils.Profiler;

class Puzzle2
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries;

    static IEnumerable<long> ExpandRanges(List<MapItem> ranges)
    {
        foreach(var r in ranges)
        {
            for(long i = r.src; i < r.src + r.len; i++)
                yield return i;
        }
    }

    public static long Solve(string file)
    {
        using var reader = new StreamReader(file);
        string line = reader.ReadLine();

        List<long> seeds = line.Split(':', splitOptions)[1].Split(' ',splitOptions).Select(x => long.Parse(x)).ToList();

        List<MapItem> ranges = new();
        for(int i=0; i<seeds.Count; i+=2)
        {
            MapItem range = new() { src = seeds[i], len = seeds[i+1] };
            ranges.Add(range); 
        }
 
        long seedCount = ranges.Sum(r => r.len);
        Profiler.Trace("Total seed count: {0}", seedCount);    

        List<Map> maps = new();
        Map currentMap = null;

        while((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            
            if(line.Contains(':'))
            {
                string name = line.Split(':', splitOptions)[0];
                currentMap = new Map(name);
                maps.Add(currentMap);
            } 
            else
            {
                long[] nums = line.Split(' ',splitOptions).Select(x => long.Parse(x)).ToArray();
                MapItem item = new () {
                    dst = nums[0],
                    src = nums[1],
                    len = nums[2],
                };
                currentMap.Add(item);
            }   
        }

        //long min = ExpandRanges(ranges).Min(s => Algo.FindLocation(maps, s));
        long min = long.MaxValue;
        long c = 0;
        var startTime = DateTime.Now;
        foreach(long seed in ExpandRanges(ranges))
        {
            long l = Algo.FindLocation(maps, seed);
            if(l < min)
                min = l;
            if(++c % 1000000 == 0)
            {
                var t = DateTime.Now;
                var dt = t - startTime;
                Profiler.Trace("{0:hh':'mm':'ss.fff} Processed {1} seeds of {2} - {3}% ({4:hh':'mm':'ss})", t, c, seedCount, (c*100)/seedCount, dt);    
            }
        }
        
        return min;
    }
}
