using static IOUtils;

void combinations()
{
    var bits = new []{0,1};
    foreach(var a in bits)
    foreach(var b in bits)
    foreach(var c in bits)
        new []{a,b,c}.println();
}

ISet<int[]> set = new HashSet<int[]>(IntArrayComparer.Instance);

int[] a = new int[8];
bool walk(int bit, int level)
{
    if(level >= a.Length) 
    {
        set.Add((int[])a.Clone());
        a.println();
        return false;
    }
    
    a[level] = bit;
    if(!walk(0, level+1))
        return true;

    var ret = walk(1, level+1);
    return ret;
}

println("hello");


walk(0, 0);
walk(1, 0);

println("set count={0}:", set.Count);
foreach(var e in set)
    e.println();

