using static IOUtils;
using Utils.Profiler;

class Puzzle1
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        List<byte[]> list = new();

        using var reader = new StreamReader(file);
        foreach (var line in reader.NonEmptyLines())
        {
            var row = line.Select(ch => byte.Parse(ch.ToString())).ToArray();
            list.Add(row);
        }

        var grid = list.ToArray();
        var algo = new PathFindAllDirLim(grid, 3+1);
        
        println("--- Paths ----");
        int no = 0;
        long cost = algo.FindMinCost2((p) => 
        //long cost = algo.FindMinCost_DpWithMemo((p) => 
        { 
            print($"{++no, 2}. [{p.Cost}] "); 
            println(p.Select(cell => cell.cost), "->"); 
            //if(++no % 10 == 0)
            {
                //println($"{no, 2}. "); 
                //Profiler.Trace("{0, 2}.", no);
            }
        });

        long res = cost - grid[0][0];
        println("actual MinCost={0}", cost);
        println("puzzle MinCost={0}", res);
        return res;
    }
}
