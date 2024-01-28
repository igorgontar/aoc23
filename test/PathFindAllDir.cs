using static IOUtils;

class Path : List<int>
{
    public Path(int capacity) : base(capacity) {}    
    public Path(IEnumerable<int> other) : base(other) {}    
    public Path Clone() 
    {
        return new Path(this);
    }

    public void Push(int x) { Add(x); }
    public void Pop() { RemoveAt(Count-1); }
}

class PathFindAllDir
{
    readonly int DY;
    readonly int DX;
    readonly int[][] grid;
    readonly int[][] hash;

    public PathFindAllDir(int[][] grid)
    {
        this.grid = grid;
        
        DY = grid.Length;
        DX = grid[0].Length;

        hash = new int[DY][];
        for(int i=0; i<DY; i++)
            hash[i] = new int[DX];
    }

    Action<Path> onResult;
    Path path;
    void FindBacktrack(Action<Path> onResult)
    {
        this.onResult = onResult;
        this.path = new Path(DX * DY + 1);
        Backtrack(0, 0);
    }

    void Backtrack(int row, int col)
    {
        // we reached outside of the grid
        if (row < 0 || row >= DY || col < 0 || col >= DX)
            return;
        // already visited cell during current transition
        if(hash[row][col] == 1)
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
        hash[row][col] = 1; // mark cell as visited

        Backtrack(row    , col + 1); // move right
        Backtrack(row + 1, col    ); // move down
        Backtrack(row - 1, col    ); // move up
        Backtrack(row    , col - 1); // move left

        path.Pop(); // remove current cell (this is the main trick to understand the algo!)
        hash[row][col] = 0; // unmark cell for other transitions
    }

    public static void Run()
    {
        int[][] grid = {
            new int[] { 1, 2, 3 },
            new int[] { 4, 5, 6 },
            new int[] { 7, 8, 9 },
        };

        var algo = new PathFindAllDir(grid);
        println($"====== {algo.GetType().Name} =====");

        println("--- Grid ----");
        println(grid, " ");

        println("--- Paths ----");

        //List<Path> paths = new();
        //algo.FindBacktrack((p) => paths.Add(p.Clone()));
        //println(paths, "->");

        println("--- Paths 2 ----");

        int c = 0;
        algo.FindBacktrack((p) => { print($"{++c, 2}. "); println(p, "->"); });

    }

}