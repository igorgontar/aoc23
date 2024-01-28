using Utils.Profiler;

class Puzzle2b
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

        var startTime = DateTime.Now;

        long count = 0;
        for (int i = 0; ; i++)
        {
            if (i == steps.Length) i = 0;

            char go = steps[i];
            if (go == 'L')
            {
                for (int n = 0; n < nodes.Length; n++)
                    nodes[n] = graf.Left(nodes[n]);
            }
            else if (go == 'R')
            {
                for (int n = 0; n < nodes.Length; n++)
                    nodes[n] = graf.Right(nodes[n]);
            }
            else
                throw new Exception("invalid path");

            ++count;

            // if (count % 1000 ==0 && nodes.Any(x => x.Z))
            // {
            //     foreach (var z in nodes)
            //     {
            //         Console.Write("{0} - {1,-5} | ", z.id, z.Z ? 'Z' : ' ');
            //     }
            //     Console.WriteLine();
            // }
            if(count % (100*1000*1000) == 0)
            {
                var t = DateTime.Now;
                var dt = t - startTime;
                var result = 10668805667831;
                Profiler.Trace("{0:hh':'mm':'ss.fff} Processed {1} {2:0.00##}% ({3:hh':'mm':'ss})", t, count, (count*100.0)/result, dt);    
            }

            if (nodes.All(x => x.Z))
                break;
        }

        return count;
    }

}
