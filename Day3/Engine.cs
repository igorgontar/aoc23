class Engine
{
    readonly List<string> list = new();

    public int DX => list[0].Length;
    public int DY => list.Count;

    public void AddRow(string row)
    {
        list.Add(row); // TODO: add gatekeeper to ensure all rows have the same length
    }

    public char this[int y, int x]
    {
        get
        {
            if (y < 0 || y >= list.Count) return (char)0;
            var row = list[y];
            if (x < 0 || x >= row.Length) return (char)0;
            char c = row[x];
            if (c == '.') return (char)0;
            if (c == '*') return (char)2;
            if (!char.IsDigit(c)) return (char)1;
            return c;

        }
        set { }
    }

    public int CheckPotentialPart(string part, int x, int y)
    {
        Engine engine = this;
        int dx = part.Length;
        bool isPart = false;
        
        // check for adjacent symbols to the left and to the right
        if(engine[y, x-1] != 0 || engine[y, x+dx] != 0)
        {
            isPart = true;    
        }
        
        if(!isPart)
        {
            // check for adjacent symbols in the row above 
            int j = y-1;
            for (int i = x-1; i <= x+dx; i++)
            {
                if(engine[j, i] != 0)        
                {
                    isPart = true;    
                    break;
                }
            }
        }
        
        if(!isPart)
        {
            // check for adjacent symbols in the row below
            int j = y+1;
            for (int i = x-1; i <= x+dx; i++)
            {
                if(engine[j, i] != 0)        
                {
                    isPart = true;    
                    break;
                }
            }
        }

        if(!isPart)
            return -1;

        int partNo = int.Parse(part);
        return partNo;
    }


}
