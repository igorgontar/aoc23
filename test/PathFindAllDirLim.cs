using static IOUtils;

record Cell // use record to benefit from automatic hash and equals
{
    public readonly int row;
    public readonly int col;
    public readonly int cost;

    public Cell(int row, int col, int cost)
    {
        this.row = row;
        this.col = col;
        this.cost = cost;
    }
};

class Path2 : List<Cell>
{
    public Path2(int capacity) : base(capacity) {}    
    public Path2(IEnumerable<Cell> other) : base(other) {}    
    public Path2 Clone() 
    {
        return new Path2(this);
    }

    public void Push(int row, int col, int cost) { Add(new Cell(row,col,cost)); }
    public void Pop() { RemoveAt(Count-1); }
}

class PathFindAllDirLim
{
    readonly int DY;
    readonly int DX;
    readonly int[][] grid;
    readonly int[][] hash;

    public PathFindAllDirLim(int[][] grid)
    {
        this.grid = grid;
        
        DY = grid.Length;
        DX = grid[0].Length;

        hash = new int[DY][];
        for(int i=0; i<DY; i++)
            hash[i] = new int[DX];
    }

    Action<Path2> onResult;
    Path2 path;
    void FindBacktrack(Action<Path2> onResult)
    {
        this.onResult = onResult;
        this.path = new Path2(DX * DY + 1);
        Backtrack(0, 0);
    }

    const int maxSteps = 3;

    bool StepLimitReached(int row, int col)
    {
        if(path.Count < maxSteps) return false;
   
        // 0 1 2 {3 4 5 6} 
        int c = path.Count;
        bool all = true;
        for(int i = c-maxSteps; i<c; i++)
            if(path[i].row != row)
            {
                all = false;
                break;
            }
        
        if(all) 
            return true;
        
        all = true;
        for(int i = c-maxSteps; i<c; i++)
            if(path[i].col != col)
            {
                all = false;
                break;
            }

        if(all) 
            return true;

        return false;
    }

    void Backtrack(int row, int col)
    {
        // we reached outside of the grid
        if (row < 0 || row >= DY || col < 0 || col >= DX)
            return;
        // already visited cell during current transition
        if(hash[row][col] == 1)
            return;    

        if(StepLimitReached(row, col))
            return;

        int cost = grid[row][col];
        // If the bottom right cell is reached, save the result
        // we store the copy of the path in the result list, in reality we would call callback here and let user
        // decide what to do with the path we found
        if (row == DY - 1 && col == DX - 1)
        {
            path.Push(row, col, cost);
            onResult(path);
            path.Pop();
            return;
        }
        
        path.Push(row, col, cost); // add current cell to the path
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
        // int[][] grid = {
        //     new int[] { 1, 2, 3 },
        //     new int[] { 4, 5, 6 },
        //     new int[] { 7, 8, 9 },
        // };

        int[][] grid = {
            new int[] { 1, 2, 3, 4 },
            new int[] { 5, 6, 7, 8 },
            new int[] { 9, 10, 11, 12 },
            new int[] { 13, 14, 15, 16 },
        };

        var algo = new PathFindAllDirLim(grid);
        println($"====== {algo.GetType().Name} =====");

        println("--- Grid ----");
        println(grid, "{0,2} ");

        println("--- Paths ----");
        int no = 0;
        algo.FindBacktrack((p) => 
        { 
            print($"{++no, 2}. "); 
            println(p.Select(cell => cell.cost), "->"); 
        });

    }

}