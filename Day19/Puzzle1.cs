class Puzzle1
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        long sum = 0;
        Workflow wf = new();
        Parts parts = new();

        using var reader = new StreamReader(file);
        
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
                    rule.Add(p,op,v,res);
                }
            }
            wf.Add(rule.Name, rule);
        }

        // parse parts list
        foreach (var line in reader.AllLines())
        {
            if (string.IsNullOrWhiteSpace(line))
                break;

            var split = line.Split(new char[] { '{', '}',',' }, 5, splitOptions);
            
            Part p = new();
            p.x = int.Parse(split[0].Substring(2)); 
            p.m = int.Parse(split[1].Substring(2)); 
            p.a = int.Parse(split[2].Substring(2)); 
            p.s = int.Parse(split[3].Substring(2)); 

            parts.Add(p);
        }

        foreach (var p in parts)
        {
            string name = "in";
            for (; ; )
            {
                Rule rule = wf[name];
                name = rule.Evaluate(p);
                if (name == "A")
                {
                    sum += p.Rating();
                    break;
                }
                else if (name == "R")
                {
                    break;
                }
            }
        }

        return sum;
    }
}

