class Part
{
    public readonly int partNo;
    public readonly int x;
    public readonly int y;
    public readonly int dx;

    public Part(int partNo, int x, int y, int dx)
    {
        this.partNo = partNo;
        this.x = x;
        this.y = y;
        this.dx = dx;
    }
}

class Point 
{ 
    public readonly int x; 
    public readonly int y; 
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
};

class PartCollection
{
    readonly List<Part>[] rows = new List<Part>[] {};

    public PartCollection(int rowCount)
    {
        rows = new List<Part>[rowCount];
    }

    public void Add(Part p)
    {
        var row = rows[p.y];
        if(row == null)
        {
            row = new List<Part>();
            rows[p.y] = row;
        }  
        row.Add(p);
    }

    public Part FindBy(int x, int y)
    {
        var row = rows[y];
        if(row == null)
            return null;

        foreach(var p in row)
        {
            if(x >= p.x && x < p.x + p.dx)
                return p;
        }
        return null;
    } 

    public ISet<int> FindPartsAroundGear(int x, int y)
    {
        HashSet<int> set = new();

        foreach(var pt in GetCoordsAroungGear(x,y))
        {
            var part = FindBy(pt.x, pt.y);
            if(part != null)
                set.Add(part.partNo);
            if(set.Count > 2)
                break;           
        }

        return set;
    }

    static IEnumerable<Point> GetCoordsAroungGear(int x, int y)
    {
        // usie yield for the sake of over engineering
        yield return new Point(x-1, y);
        yield return new Point(x+1, y);
        
        yield return new Point(x-1, y-1);
        yield return new Point(x  , y-1);
        yield return new Point(x+1, y-1);

        yield return new Point(x-1, y+1);
        yield return new Point(x  , y+1);
        yield return new Point(x+1, y+1);
    }

}