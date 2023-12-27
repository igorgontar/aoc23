using System.Text;
using Utils.Profiler;
using static IOUtils;
using static Algo;

class Puzzle1
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        //long c = 0;
        //var startTime = DateTime.Now;

        long sum = 0;
        using var reader = new StreamReader(file);
        foreach (var line in reader.NonEmptyLines())
        {
            var split = line.Split(' ', splitOptions);
            string springs = split[0]; 
            int[] runs = split[1].Split(',', splitOptions).Select(s => int.Parse(s)).ToArray();

            Cache cache = new();
            long count = CountWays1(springs, runs);
            //long count = CountWays2(cache, springs, runs);
            sum += count;

            //var t = DateTime.Now;
            //var dt = t - startTime;
            //++c;
            //Profiler.Trace("{0:hh':'mm':'ss.fff} Processed {1} seeds of {2} - {3}% ({4:hh':'mm':'ss})", t, c, 1000, (c * 100) / 1000, dt);

        }

        return sum;
    }

}
