class Puzzle2
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        using var reader = new StreamReader(file);

        long sum = 0;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            long[] a = line.Split(' ', splitOptions).Select(s => long.Parse(s)).ToArray();
            long res = Algo.Extrapolate(a.Reverse().ToArray());
            sum += res;
        }

        return sum;
    }
}
