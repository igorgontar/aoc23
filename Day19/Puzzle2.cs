using Utils.Profiler;

class Puzzle2
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        Workflow wf = new();

        using (var reader = new StreamReader(file))
        {
            // parse workflow
            foreach (var line in reader.AllLines())
            {
                if (string.IsNullOrWhiteSpace(line))
                    break;

                var split = line.Split(new char[] { '{', '}' }, 3, splitOptions);
                var name = split[0];
                var exprs = split[1].Split(',');

                var rule = new Rule(name);
                foreach (var ex in exprs)
                {
                    if (ex.IndexOf(':') < 0)
                    {
                        rule.Add(ex);
                    }
                    else
                    {
                        var tokens = ex.Split(new char[] { '>', '<', ':' }, 3, splitOptions);
                        char op = ex[1];
                        string p = tokens[0];
                        int v = int.Parse(tokens[1]);
                        var res = tokens[2];
                        rule.Add(p, op, v, res);
                    }
                }
                wf.Add(rule.Name, rule);
            }
        }

        var startTime = DateTime.Now;
        long count = 0;
        double total = Math.Pow(4000, 4);

        long sum = 0;
        for (int x = 1; x <= 4000; x++)
        for (int m = 1; m <= 4000; m++)
        for (int a = 1; a <= 4000; a++)
        for (int s = 1; s <= 4000; s++)
        {
            Part p = new();
            p.x = x;
            p.m = m;
            p.a = a;
            p.s = s;

            string name = "in";
            for (; ; )
            {
                Rule rule = wf[name];
                name = rule.Evaluate(p);
                if (name == "A")
                {
                    sum++;
                    break;
                }
                else if (name == "R")
                {
                    break;
                }
            }

            if(++count % (10*1000*1000) == 0)
            {
                var t = DateTime.Now;
                var dt = t - startTime;
                Profiler.Trace("{0:hh':'mm':'ss.fff} Processed {1} {2:0.00##}% ({3:hh':'mm':'ss})", t, count, (count*100.0)/total, dt);    
            }
        }

        return sum;
    }
}

