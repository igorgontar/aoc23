using Utils;
using Utils.Profiler;

class Puzzle2
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

        Node[] nodes = graf.Nodes.Where(n => n.A).ToArray();

        Profiler.Trace("A nodes count = {0}", nodes.Length);
        long[] counts = nodes.Select(n =>
        {
            long c = CountSteps(graf, steps, n);
            Profiler.Trace("{0} -> {1}", n.id, c);
            return c;
        }).ToArray();

        counts.print();
        
        long res = AocMath.LCM(counts);

        return res;
    }

    static int CountSteps(Graf graf, string steps, Node start)
    {
        Node node = start;
        int count = 0;
        for (int i = 0; ; i++)
        {
            if (i == steps.Length) i = 0;

            char go = steps[i];
            if (go == 'L')
                node = graf.Left(node);
            else
                node = graf.Right(node);

            ++count;

            if (node.Z)
                break;
        }
        return count;
    }
}
