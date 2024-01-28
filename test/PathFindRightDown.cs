using static IOUtils;

class PathFindRightDown
{

    readonly int DY;
    readonly int DX;
    readonly int[][] grid;

    public PathFindRightDown(int[][] grid)
    {
        this.grid = grid;
        DY = grid.Length;
        DX = grid[0].Length;
    }

    Action<Path> onResult;
    Path path;
    void FindBacktrack(Action<Path> onResult)
    {
        this.onResult = onResult;
        this.path = new Path(DX + DY + 1);
        Backtrack(0, 0);
    }

    void Backtrack(int row, int col)
    {
        // we reached outside of the grid
        if (row < 0 || row >= DY || col < 0 || col >= DX)
            return;

        // If the bottom right cell is reached, save the result
        // we store the copy of the path in the result list, in reality we would call callback here and let user
        // decide what to do with the path we found
        if (row == DY - 1 && col == DX - 1)
        {
            path.Push(grid[row][col]);
            onResult(path);
            path.Pop();
            return;
        }
        
        path.Push(grid[row][col]); // add current cell to the path

        Backtrack(row, col + 1); // move right
        Backtrack(row + 1, col); // move down

        path.Pop(); // remove current cell (this is the main trick to understand the algo!)
    }

    public static void Run()
    {
        int[][] grid = {
            new int[] { 1, 2, 3 },
            new int[] { 4, 5, 6 },
            new int[] { 7, 8, 9 },
        };

        var algo = new PathFindRightDown(grid);
        println($"====== {algo.GetType().Name} =====");

        println("--- Grid ----");
        println(grid, " ");

        println("--- Paths ----");

        List<Path> paths = new();
        algo.FindBacktrack((p) => paths.Add(p.Clone()));
        println(paths, "->");

        println("--- Paths 2 ----");

        algo.FindBacktrack((p) => println(p, "->"));

    }

}