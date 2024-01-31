using static IOUtils;

record Point // use record to benefit from automatic hash and equals
{
    public readonly int x;
    public readonly int y;
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
};

class Polygon
{
    readonly List<Point> points = new();
    readonly HashSet<Point> index = new();

    public void Add(Point pt)
    {
        points.Add(pt);
        index.Add(pt);
    }

    public void RemoveLast()
    {
        points.RemoveAt(points.Count-1);
    }

    public bool Contains(Point pt)
    {
        if (index.Contains(pt))
            return true; // including polygon borders
        bool yes = IsInPolygon(points, pt);
        return yes;
    }

    static bool IsInPolygon(List<Point> poly, Point p)
    {
        Point p1, p2;
        bool inside = false;

        var oldPoint = new Point(poly[poly.Count - 1].x, poly[poly.Count - 1].y);

        for (int i = 0; i < poly.Count; i++)
        {
            var newPoint = new Point(poly[i].x, poly[i].y);

            if (newPoint.x > oldPoint.x)
            {
                p1 = oldPoint;
                p2 = newPoint;
            }
            else
            {
                p1 = newPoint;
                p2 = oldPoint;
            }

            if ((newPoint.x < p.x) == (p.x <= oldPoint.x)
                && (p.y - (long)p1.y) * (p2.x - p1.x) < (p2.y - (long)p1.y) * (p.x - p1.x))
            {
                inside = !inside;
            }

            oldPoint = newPoint;
        }

        return inside;
    }
}

