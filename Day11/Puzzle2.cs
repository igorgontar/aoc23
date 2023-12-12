class Puzzle2
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    class Grid : List<List<byte>> { }
    class Flags : List<byte> { }
    class Galaxies : List<Galaxy> {} 
    class Pairs : List<Pair> { }

    public static long Solve(string file, int scale=1000*1000)
    {
        Grid grid = new();
        Flags rowFlags = new();

        using var reader = new StreamReader(file);
        foreach (var line in reader.NonEmptyLines())
        {
            var row = line.Select(ch => ch == '#' ? (byte)1 : (byte)0).ToList();
            grid.Add(row);
            bool flag = row.Any(b => b == 1);
            rowFlags.Add(flag ? (byte)0 : (byte)1);
        }
        
        int DY = grid.Count;
        int DX = grid[0].Count;

        Flags colFlags = new();
        for (int x = 0; x < DX; x++)
        {
            bool flag = ColumnAt(grid, x).Any(b => b == 1);
            colFlags.Add(flag ? (byte)0 : (byte)1);
        }
 
        // Console.WriteLine("flags:");
        // rowFlags.Print();
        // colFlags.Print();
        // Console.WriteLine("grid:");
        // foreach(var r in grid)
        //     r.Print();

        //scale = 1000*1000;
        
        Galaxies galaxies = new();
        int yc = 0, id = 0;    
        for (int y = 0; y < DY; y++, yc++)
        {
            int xc = 0;
            
            if(rowFlags[y]==1) 
                yc += scale-1; // count as double space expaned universe
            else
            {
                for (int x = 0; x < DX; x++, xc++)
                {
                    if(colFlags[x]==1) 
                        xc += scale-1;
                    else
                    {
                        if(grid[y][x] == 1)
                            galaxies.Add(new Galaxy(++id, xc, yc));
                    }    
                }
            }
        }

        // Console.WriteLine("galaxies:");
        // foreach(var g in galaxies)
        // {
        //     Console.WriteLine("{0} - ({1} {2})", g.id, g.x, g.y);
        // }

        //Console.WriteLine("build unique connections:");
        int n=0;
        Pairs pairs = new();
        foreach(var g1 in galaxies)
        {
            foreach(var g2 in galaxies)
            {
                if(g2.id > g1.id)
                {
                    var p = new Pair(g1, g2);
                    pairs.Add(p);        
                    //Console.WriteLine("{0,-3} - ({1},{2})", n, p.g1.id, p.g2.id);
                    ++n;
                }
            }
        }
        
        long sum = 0;
        foreach(var p in pairs)
        {
            var steps = Math.Abs(p.g2.x - p.g1.x) + Math.Abs(p.g2.y - p.g1.y);
            sum += steps;        
        }        

        return sum;
    }

    static IEnumerable<byte> ColumnAt(Grid grid, int x)
    {
        for (int y = 0; y < grid.Count; y++)
        {
            yield return grid[y][x];
        }
    }
}
