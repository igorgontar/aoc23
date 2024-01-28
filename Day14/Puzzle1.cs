using static IOUtils;
using Utils.Profiler;

class Puzzle1
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        List<char>[] temp = null;

        // read and swap rows and columns on the fly
        using var reader = new StreamReader(file);
        foreach (var line in reader.NonEmptyLines())
        {
            if(temp == null) 
            {
                 temp = new List<char>[line.Length];
                 for(int i=0; i<temp.Length; i++)
                    temp[i] = new List<char>();
            }

            for(int i=0; i<line.Length; i++)
                temp[i].Add(line[i]);
        }
    
        // convert char[] to string for easy debugging 
        string[] grid = new string[temp.Length];
        for(int i=0; i<temp.Length; i++)
            grid[i] = '#' + string.Join(null, temp[i]);

        // 0  1 2 .. 9
        // 10 9 8... 1
        int ProcessRow(string row)
        {
            int c = row.Length;
            int sum = 0;
            for(int i=0; i<c;)
            {
                if(row[i] == '#')
                {
                    int oc = 0;
                    int n = i;
                    while(++n < c)
                    {
                        if(row[n] == 'O')
                            oc++;
                        if(row[n] == '#')
                            break;
                    }        
                    
                    while(oc-- > 0)
                    {
                        int score = c - (i+oc+1);
                        sum += score;
                    }
                    i = n;
                }
                else
                    i++;
            }
            return sum;
        }

        int sum = grid.Select(r => ProcessRow(r)).Sum();
        return sum;
    }
}
