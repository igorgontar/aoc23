using System.Text;
using Utils.Profiler;
using static IOUtils;
using static Algo;

class Puzzle2
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;


    public static long Solve(string file)
    {
        long sum = 0;
        using var reader = new StreamReader(file);
        foreach (var line in reader.NonEmptyLines())
        {
            //using(Profiler.CheckPoint($"Line{++c}"))
            {
                var split = line.Split(' ', splitOptions);
                string sp = split[0];
                string sr = split[1];

                string springs = string.Join('?', sp, sp, sp, sp, sp);
                string dams = string.Join(',', sr, sr, sr, sr, sr);
                int[] damaged = dams.Split(',', splitOptions).Select(s => int.Parse(s)).ToArray();

                Cache cache = new();
                long count = CountWays2(cache, springs, damaged);
                sum += count;
            }
        }

        return sum;
    }
}
