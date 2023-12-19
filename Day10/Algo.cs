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

class Grid : List<List<char>> { }

class Polygon
{
    readonly List<Point> points = new();
    readonly HashSet<Point> index = new();

    public void Add(Point pt)
    {
        points.Add(pt);
        index.Add(pt);
    }

    public bool Contains(Point pt)
    {
        if(index.Contains(pt))
            return false; // excluding polygon borders
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

class Algo
{
    //     S   
    //   F---7
    // E |   | W 
    //   L---J
    //     N 
    public static int WalkTheLoop(Grid grid, Point start)
    {
        int xp = start.x;
        int yp = start.y;
        int x = xp;
        int y = yp + 1;

        int steps = 1;
        char dir = '.';
        for (; ; )
        {
            dir = grid[y][x];
            if (dir == 'S')
                break;

            switch (dir)
            {
                case '|': if (y > yp) { yp = y++; } else { yp = y--; } break;
                case '-': if (x > xp) { xp = x++; } else { xp = x--; } break;
                case 'F': if (y < yp) { yp = y; xp = x++; } else { xp = x; yp = y++; } break;
                case '7': if (y < yp) { yp = y; xp = x--; } else { xp = x; yp = y++; } break;
                case 'J': if (y > yp) { yp = y; xp = x--; } else { xp = x; yp = y--; } break;
                case 'L': if (y > yp) { yp = y; xp = x++; } else { xp = x; yp = y--; } break;

                default:
                    throw new Exception($"invalid input ({x},{y}): {dir}");
            }

            //print($"{dir} -> [{grid[y][x]}]({x},{y})");

            steps++;
        }

        return steps;
    }
    
    public static Polygon WalkTheLoop2(Grid grid, Point start)
    {
        Polygon p = new();

        int xp = start.x;
        int yp = start.y;
        int x = xp;
        int y = yp + 1;

        p.Add(new Point(xp,yp)); // add 'S' too
        p.Add(new Point(x,y));

        char dir = '.';
        for (; ; )
        {
            dir = grid[y][x];
            if (dir == 'S')
                break;

            switch (dir)
            {
                case '|': if (y > yp) { yp = y++; } else { yp = y--; } break;
                case '-': if (x > xp) { xp = x++; } else { xp = x--; } break;
                case 'F': if (y < yp) { yp = y; xp = x++; } else { xp = x; yp = y++; } break;
                case '7': if (y < yp) { yp = y; xp = x--; } else { xp = x; yp = y++; } break;
                case 'J': if (y > yp) { yp = y; xp = x--; } else { xp = x; yp = y--; } break;
                case 'L': if (y > yp) { yp = y; xp = x++; } else { xp = x; yp = y--; } break;

                default:
                    throw new Exception($"invalid input ({x},{y}): {dir}");
            }

            //print($"{dir} -> [{grid[y][x]}]({x},{y})");
            p.Add(new Point(x,y));
        }

        return p;
    }
}
