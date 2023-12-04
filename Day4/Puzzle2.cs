
class Map : Dictionary<int,int>
{
    public new int this[int key] 
    {
        get { TryGetValue(key, out int v); return v;}
        set { base[key] = value;}
    }
}

class Puzzle2
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries;

    public static int Solve(string file)
    {
        using var reader = new StreamReader(file);
        Map map = new();
        int i = 0;
        string line;
        while((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            ++i;
            string card = line.Split(':', splitOptions)[1];
            var scratch = card.Split('|',splitOptions);
            string[] win = scratch[0].Split(' ',splitOptions);
            string[] my = scratch[1].Split(' ',splitOptions);
            int count = my.Count(x => win.Contains(x));
            map[i] += 1;
            for(int j=0; j<count; j++)
            {
                map[i+1+j] += map[i];
            }   
        }
        int sum = map.Values.Sum();
        return sum;
    }

}
