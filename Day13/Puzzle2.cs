class Puzzle2
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    class Grid : List<List<byte>> {}

    public static long Solve(string file)
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
            int smudgeCount = 0;
            if(prev >= 0)
            {
                smudgeCount += CompareCols(grid, prev, x);
                for(int x1=x-2, x2=x+1; x1>=0 && x2 < DX; x1--, x2++)
                {
                    smudgeCount += CompareCols(grid, x1, x2); 
                }
    
                if(smudgeCount==1)
                {
                    count += x; 
                    break;
                }
            }
            prev = x;
        }

        if(count > 0)
            return count;

        // search similar rows
        prev = -1;
        for (int y = 0; y < DY; y++)
        {
            int smudgeCount = 0;
            if(prev >= 0)
            { 
                smudgeCount += CompareRows(grid, prev, y);
                for(int y1=y-2, y2=y+1; y1>=0 && y2 < DY; y1--, y2++)
                {
                    smudgeCount += CompareRows(grid, y1,y2); 
                }
                
                if(smudgeCount==1)
                {
                    count += (y * 100); 
                    break;
                }
            }
            prev = y;
        }
        
        return count;
    }

    static int CompareRows(Grid grid, int y1, int y2)
    {
        if(y1 < 0 || y2 >= grid.Count) return 3;

        int DX = grid[0].Count;
        int neCount = 0;
        for (int x = 0; x < DX; x++)
        {
            if(grid[y1][x] != grid[y2][x])
                neCount++; 
        }

        return neCount;
    }

    static int CompareCols(Grid grid, int x1, int x2)
    {
        if(x1 < 0 || x2 >= grid[0].Count) return 3;
        
        int DY = grid.Count;
        int neCount = 0; 
        for (int y = 0; y < DY; y++)
        {
            if(grid[y][x2] != grid[y][x1])
                neCount++; 
        }

        return neCount;
    }
}

