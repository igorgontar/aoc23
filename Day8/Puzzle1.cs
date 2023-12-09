class Puzzle1
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        using var reader = new StreamReader(file);

        string line = reader.ReadLine();
        string steps = line;

        Graf graf = new();

        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var split = line.Split('=', splitOptions);
            var id = split[0];
            var rest = split[1].Split(new[] { ',', ' ', '(', ')' }, splitOptions);
            var left = rest[0];
            var right = rest[1];

            var n = new Node(id, left, right);
            graf.Add(n);
        }

        long count = 0;
        var node = "AAA"; //graf.Root.id; - that was a nasty one
        for(int i=0;;i++)
        {
            char go = steps[i % steps.Length];
            if (go == 'L')
                node = graf[node].left;
            else if(go == 'R') 
                node = graf[node].right;
            else
                throw new Exception("invalid path");

            ++count;

            if (node == "ZZZ")
                break;
        }

        return count;
    }
}
