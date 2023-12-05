struct MapItem
{
    public long src;
    public long len;
    public long dst;
}

class Map : SortedSet<MapItem>
{
    class MyComparer : IComparer<MapItem>
    {
        public static MyComparer Instance = new ();
        public int Compare(MapItem x, MapItem y) { return x.src == y.src ? 0 : (x.src < y.src ? -1 : 1); }    
    }

    public readonly string name;
    public Map(string name) : base(MyComparer.Instance)
    {
        this.name = name;
    }
}

class Algo
{
    public static long FindLocation(List<Map> maps, long seed)
    {
        long source = seed;
        long dest = -1;
        foreach(var map in maps)
        {
            dest = FindDestination(map, source);
            source = dest;
        }
        return dest;
    }

    public static long FindDestination(Map map, long s)
    {
        foreach(var m in map)
        {
            if(s >= m.src && s < m.src + m.len)
                return m.dst + (s - m.src);
            else if(s < m.src)
                return s;   
        }
        return s;
    }
}
