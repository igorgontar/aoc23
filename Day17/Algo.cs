using static IOUtils;

struct Cell 
{
    public readonly int row;
    public readonly int col;
    public readonly byte cost;

    public Cell(int row, int col, byte cost)
    {
        this.row = row;
        this.col = col;
        this.cost = cost;
    }
};

class Path : List<Cell>
{
    public int Cost {get;private set; }
    
    public Path(int capacity) : base(capacity) {}    
    public Path(IEnumerable<Cell> other) : base(other) {}    
    public Path Clone() 
    {
        return new Path(this);
    }

    public void Add(int row, int col, byte cost) 
    { 
        Add(new Cell(row,col,cost)); 
        Cost += cost;
    }

    public void Rem(byte cost) 
    { 
        RemoveAt(Count-1); 
        Cost -= cost;
    }
}

class PathFindAllDirLim
{
    readonly byte[][] grid;
    readonly int maxSteps;

    readonly int DY;
    readonly int DX;
    
    byte[][] hash;
    long[,] memo;

    Action<Path> onResult;
    Path path;
    long minCost;

    public PathFindAllDirLim(byte[][] grid, int maxSteps)
    {
        this.grid = grid;
        this.maxSteps = maxSteps;
        
        DY = grid.Length;
        DX = grid[0].Length;
    }

    void InitHash()
    {
        hash = new byte[DY][];
        for(int i=0; i<DY; i++)
            hash[i] = new byte[DX];
    }

    void InitMemo()
    {
        memo = new long[DY,DX];
        for(int y=0; y<DY; y++)
            for(int x=0; x<DX; x++)
                memo[y,x] = -1;
    }
    
    public long FindMinCost(Action<Path> onResult)
    {
        InitHash();
        this.onResult = onResult;
        this.path = new Path(DX * DY + 1);
        this.minCost = long.MaxValue;

        Backtrack(0, 0);

        return this.minCost; 
    }

    public long FindMinCost2(Action<Path> onResult)
    {
        InitHash();
        this.onResult = onResult;
        this.path = new Path(DX * DY + 1);
        this.minCost = long.MaxValue;

        Backtrack2(0, 0, 0,0,0,0);

        return this.minCost; 
    }

    public long FindMinCost_DpWithMemo(Action<Path> onResult)
    {
        InitHash();
        InitMemo();
        this.onResult = onResult;
        this.path = new Path(DX * DY + 1);

        return DpWithMemo(0,0); 
    }

    bool StepLimitReached(int row, int col)
    {
        if(path.Count < maxSteps) return false;
   
#if use_local_function
        // let's try local function
        // for some reason using local fucntion is 3 times slower (because of predicate?)
        bool LastFewSteps(Func<Cell, bool> predicate)
        {
            // 0 1 2 {3 4 5 6} 
            int c = path.Count;
            bool all = true;
            for(int i = c-maxSteps; i<c; i++)
            {
                if(!predicate(path[i]))
                {
                    all = false;
                    break;
                }
            }
            return all;    
        }
        // check if last few steps in the path follow same row or same column as current
        bool ret = LastFewSteps(c => c.row == row) ||  LastFewSteps(c => c.col == col);
        return ret;
#else
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
#endif
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

        byte cost = grid[row][col];
        if(path.Cost + cost > minCost)
            return; 

        // If the bottom right cell is reached, save the result
        // we store the copy of the path in the result list, in reality we would call callback here and let user
        // decide what to do with the path we found
        if (row == DY - 1 && col == DX - 1)
        {
            path.Add(row, col, cost);
            onResult(path);
            if(path.Cost < minCost)
                minCost = path.Cost;
            path.Rem(cost);
            return;
        }

        path.Add(row, col, cost); // add current cell to the path
        hash[row][col] = 1; // mark cell as visited

        Backtrack(row    , col + 1); // move right
        Backtrack(row + 1, col    ); // move down
        Backtrack(row    , col - 1); // move left
        Backtrack(row - 1, col    ); // move up

        path.Rem(cost); // remove current cell (this is the main trick to understand the algo!)
        hash[row][col] = 0; // unmark cell for other transitions
    }

    void Backtrack2(int row, int col, int rc, int dc, int lc, int uc)
    {
        // we reached outside of the grid
        if (row < 0 || row >= DY || col < 0 || col >= DX)
            return;
        // already visited cell during current transition
        if(hash[row][col] == 1)
            return;    

        if(rc >= maxSteps || dc >= maxSteps || lc >= maxSteps || uc >= maxSteps)
            return;

        byte cost = grid[row][col];
        if(path.Cost + cost > minCost)
            return; 

        // If the bottom right cell is reached, save the result
        // we store the copy of the path in the result list, in reality we would call callback here and let user
        // decide what to do with the path we found
        if (row == DY - 1 && col == DX - 1)
        {
            path.Add(row, col, cost);
            onResult(path);
            if(path.Cost < minCost)
                minCost = path.Cost;
            path.Rem(cost);
            return;
        }

        path.Add(row, col, cost); // add current cell to the path
        hash[row][col] = 1; // mark cell as visited

        Backtrack2(row    , col + 1, rc+1,0,0,0); // move right
        Backtrack2(row + 1, col    , 0,dc+1,0,0); // move down

        if(row > 0 && row < DY-1)
            Backtrack2(row    , col - 1, 0,0,lc+1,0); // move left
        
        if(col > 0 && col < DX - 1)
            Backtrack2(row - 1, col    , 0,0,0,uc+1); // move up

        path.Rem(cost); // remove current cell (this is the main trick to understand the algo!)
        hash[row][col] = 0; // unmark cell for other transitions
    }

    // still can't make it working
    long DpWithMemo(int row, int col)
    {
        // we reached outside of the grid
        if (row < 0 || row >= DY || col < 0 || col >= DX)
            return long.MaxValue;
        // already visited cell during current transition
        if(hash[row][col] == 1)
            return long.MaxValue;    

        if(StepLimitReached(row, col))
            return long.MaxValue;

        byte cost = grid[row][col];
        
        // If the bottom right cell is reached, save the result
        // we store the copy of the path in the result list, in reality we would call callback here and let user
        // decide what to do with the path we found
        if (row == DY - 1 && col == DX - 1)
        {
            path.Add(row, col, cost);
            onResult(path);
            //var ret = path.Cost;
            path.Rem(cost);
            //return cost;
        }

        var computed = memo[row,col];
        if(computed != -1)
            return computed;

        path.Add(row, col, cost); // add current cell to the path
        hash[row][col] = 1; // mark cell as visited

        var c1 = DpWithMemo(row    , col + 1); // move right
        var c2 = DpWithMemo(row + 1, col    ); // move down
        var c3 = DpWithMemo(row    , col - 1); // move left
        var c4 = DpWithMemo(row - 1, col    ); // move up

        var min = Min(c1, c2, c3, c4);
        computed = cost;
        if(min != long.MaxValue)
            computed += min;

        memo[row, col] = computed;

        path.Rem(cost); // remove current cell (this is the main trick to understand the algo!)
        hash[row][col] = 0; // unmark cell for other transitions
        
        return computed;
    }    

    static long Min(long x1, long x2, long x3, long x4)
    {
        return Math.Min( Math.Min(x1, x2), Math.Min(x3, x4) );
    }
}