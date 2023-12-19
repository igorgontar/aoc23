class Puzzle2
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        Grid grid = new();
        Point start = null;

        int y = 0;
        using var reader = new StreamReader(file);
        foreach (var line in reader.NonEmptyLines())
        {
            int x = line.IndexOf('S');
            if (x >= 0)
            {
                start = new Point(x, y);
            }

            grid.Add(line.ToList());
            y++;
        }

        int count = 0;
        Polygon p = Algo.WalkTheLoop2(grid, start);
        for (y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Count; x++)
            {
                var pt = new Point(x,y);
                if(p.Contains(pt))
                    count++;
            }
        }

        return count;
    }
}
