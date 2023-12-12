class Galaxy
{
    public readonly int id;
    public readonly int x;
    public readonly int y;

    public Galaxy(int id, int x, int y)
    {
        this.id = id;
        this.x = x;
        this.y = y;
    }
}

class Pair
{
    public readonly Galaxy g1;   
    public readonly Galaxy g2;

    public Pair(Galaxy g1, Galaxy g2)
    {
        this.g1 = g1;
        this.g2 = g2;
    }
}
