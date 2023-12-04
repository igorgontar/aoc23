class Puzzle1
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries;

    public static int Solve(string file)
    {
        using var reader = new StreamReader(file);
        double sum =0;
        string line;
        while((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string card = line.Split(':', splitOptions)[1];
            var scratch = card.Split('|',splitOptions);
            string[] win = scratch[0].Split(' ',splitOptions);
            string[] my = scratch[1].Split(' ',splitOptions);
            int count = my.Count(x => win.Contains(x));
            if(count > 0)
                sum += Math.Pow(2,(count-1));
        }
        return (int)sum;
    }

}
