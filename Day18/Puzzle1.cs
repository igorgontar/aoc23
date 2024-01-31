using static IOUtils;

class Puzzle1
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        Polygon poly = new();
        //poly.Add(new Point(0, 0));

        int x = 0, y = 0;
        int minX=int.MaxValue; 
        int minY=int.MaxValue;
        int maxX=0;
        int maxY=0;

        using var reader = new StreamReader(file);
        foreach (var line in reader.NonEmptyLines())
        {
            var split = line.Split(new char[]{' ','(',')','#',}, 4, splitOptions);
            char dir = split[0][0];
            int steps = int.Parse(split[1]);
            uint rgb = uint.Parse(split[2], System.Globalization.NumberStyles.HexNumber);

            switch(dir)
            {
                case 'R' : 
                    {
                        for(int i=0; i<steps; i++)
                            poly.Add(new Point(x++, y));
                    }
                    break;
                
                case 'D' : 
                    {
                        for(int i=0; i<steps; i++)
                            poly.Add(new Point(x, y++));
                    }
                    break;

                case 'U' : 
                    {
                        for(int i=0; i<steps; i++)
                            poly.Add(new Point(x, y--));
                    }
                    break;
                case 'L' : 
                    {
                        for(int i=0; i<steps; i++)
                            poly.Add(new Point(x--, y));
                    }
                    break;
            }
            
            if(x > maxX) maxX = x;
            if(y > maxY) maxY = y;
            if(x < minX) minX = x;
            if(y < minY) minY = y;
        }
        //poly.RemoveLast();

        long sum = 0;
        for(y=minY; y<=maxY; y++)
        {
            for(x=minX; x<=maxX; x++)
            {
                bool yes = poly.Contains(new Point(x,y));
                if(yes)
                    sum++;

                //print(yes ? "* " : "- ");        

            }
            //println("");
        }


        return sum;
    }
}
