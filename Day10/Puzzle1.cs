class Puzzle1
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
            if(x >= 0)
            {
                start = new Point(x, y);
            }

            grid.Add(line.ToList());        
            y++;
        }

        int steps = Algo.WalkTheLoop(grid, start);

        return steps/2;
    }
}
