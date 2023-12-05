using Utils.Profiler;

class Puzzle1
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        using var reader = new StreamReader(file);
        string line = reader.ReadLine();

        List<long> seeds = line.Split(':', splitOptions)[1].Split(' ',splitOptions).Select(x => long.Parse(x)).ToList();

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

        //foreach(var m in maps)
        //    Profiler.Trace("{0} - {1}", m.name, m.Count);

        long min = seeds.Min(s => Algo.FindLocation(maps, s));
        return min;
    }
}
