using Utils.Collections;

class Part
{
    public int x;
    public int m;
    public int a;
    public int s;
    public int Rating() { return x + m + a + s; }
}

class Rule
{
    public readonly string Name;

    public readonly List<Func<Part, string>> exprs = new();
    
    public Rule(string name)
    {
        Name = name;
    }

    public void Add(string res) // unconditional result
    {
        exprs.Add(_ => res);
    }

    public void Add(string param, char op, int v, string res) 
    {
        if(op == '>')
        {
            Add(param, (x) => x > v ? res : null);
        }
        else if(op == '<')
        {
            Add(param, (x) => x < v ? res : null);
        }
        else
            throw new Exception($"invalid operator: '{op}'");
    }

    void Add(string param, Func<int, string> expr)
    {
        Func<Part, int> getter = null;
        if(param == "x")
            getter = (p) => p.x;
        else if(param == "m")
            getter = (p) => p.m;
        else if(param == "a")
            getter = (p) => p.a;
        else if(param == "s")
            getter = (p) => p.s;

        if(getter == null)
            throw new Exception($"invalid part parameter: '{param}'");

        exprs.Add((part) => expr(getter(part)));
    }

    public string Evaluate(Part p)
    {
        foreach(var ex in exprs)
        {
            var res = ex(p);
            if(res != null)
                return res;
        }
        throw new Exception("Bad rule def");
    }
}

class Workflow : DefDict<string, Rule> {}
class Parts : List<Part> {}
