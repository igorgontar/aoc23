class Puzzle1
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    class Grid : List<List<byte>> { }

    public static long Solve(string file, int scale=1000*1000)
    {
        Grid grid = new();
        long sum  = 0;

        using var reader = new StreamReader(file);
        foreach (var line in reader.AllLines())
        {
            if(string.IsNullOrWhiteSpace(line))
            {
                long count = Process(grid);
                sum += count;
                grid = new();
                continue;
            }
            var row = line.Select(ch => ch == '#' ? (byte)1 : (byte)0).ToList();
            grid.Add(row);
        }

        sum += Process(grid);
        return sum;
    }

    static long Process(Grid grid)
    {   
        if(grid.Count == 0) return 0;

        int DY = grid.Count;
        int DX = grid[0].Count;
        
        long count = 0;
        int prev;

        // search similar columns
        prev = -1;
        for (int x = 0; x < DX; x++)
        {
            bool skip=false;
            if(prev >= 0 && CompareCols(grid, prev, x))
            {
                bool reflection = true;
                for(int x1=x-2, x2=x+1; x1>=0 && x2 < DX; x1--, x2++)
                {
                    if(!CompareCols(grid, x1, x2)) 
                    {
                        reflection = false;    
                        break;
                    }
                }
    
                if(reflection)
                {
                    count += x; 
                    skip = true;
                    break;
                }
            }
            if(skip)
                break;
            prev = x;
        }

        if(count > 0)
            return count;

        // search similar rows
        prev = -1;
        for (int y = 0; y < DY; y++)
        {
            bool skip=false;
            if(prev >= 0 && CompareRows(grid, prev, y))
            {
                bool reflection = true;
                for(int y1=y-2, y2=y+1; y1>=0 && y2 < DY; y1--, y2++)
                {
                    if(!CompareRows(grid, y1,y2)) 
                    {
                        reflection = false;    
                        break;
                    }
                }
                if(reflection)
                {
                    count += (y * 100); 
                    skip = true;
                    break;
                }
            }
            if(skip)
                break;
            prev = y;
        }
        
        return count;
    }

    static bool CompareRows(Grid grid, int y1, int y2)
    {
        if(y1 < 0 || y2 >= grid.Count) return false;

        int DX = grid[0].Count;
        for (int x = 0; x < DX; x++)
        {
            if(grid[y1][x] != grid[y2][x])
                return false; 
        }
        return true;
    }

    static bool CompareCols(Grid grid, int x1, int x2)
    {
        if(x1 < 0 || x2 >= grid[0].Count) return false;
        
        int DY = grid.Count;
        for (int y = 0; y < DY; y++)
        {
            if(grid[y][x1] != grid[y][x2])
                return false; 
        }
        return true;
    }
}

