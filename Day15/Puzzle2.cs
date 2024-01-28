using static IOUtils;
using Utils.Profiler;

class Puzzle2
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    class Lens
    {
        public readonly string Name;
        public readonly int Focus;

        public Lens(string name, int focus)
        {
            Name = name;
            Focus = focus;
        }

        public override bool Equals(object obj)
        {
            return obj is Lens lens && Name == lens.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    class Hashmap
    {
        public class HashItems : LinkedList<Lens> { }

        public readonly HashItems[] map = new HashItems[256];

        public Hashmap()
        { }

        public Lens this[string name]
        {
            get
            {
                var hash = HashKey(name);
                HashItems list = map[hash];
                if (list == null)
                    return null;
                var lens = list.FirstOrDefault(l => l.Name == name);
                return lens;
            }
            set
            {
                var hash = HashKey(name);
                HashItems list = map[hash];
                if (list == null)
                {
                    list = new HashItems();
                    map[hash] = list;
                }
                var node = list.Find(new Lens(name, 0));
                if (node == null)
                {
                    list.AddLast(value);
                    return;
                }
                node.Value = value;    
            }
        }

        public void Remove(string name)
        {
            var hash = HashKey(name);
            HashItems list = map[hash];
            if (list == null)
                return;
            var node = list.Find(new Lens(name, 0));
            if (node == null)
                return;
            list.Remove(node);
        }

    }

    public static long Solve(string file)
    {
        using var reader = new StreamReader(file);
        var line = reader.ReadLine();
        var codes = line.Split(',', splitOptions);

        Hashmap map = new();

        // process instructions
        foreach (var code in codes)
        {
            string name = null;
            char cmd;
            int focus = 0;
            if (code.Contains('-'))
            {
                var split = code.Split(new char[] { '-' }, splitOptions);
                name = split[0];
                cmd = '-';
            }
            else
            {
                var split = code.Split(new char[] { '=' }, splitOptions);
                name = split[0];
                cmd = '=';
                focus = int.Parse(split[1]);
            }

            if (cmd == '-')
                map.Remove(name);
            else if (cmd == '=')
                map[name] = new Lens(name, focus);

        }

        // calculate power
        long sum = 0;
        int box = 0;
        foreach (var list in map.map)
        {
            box++;
            if (list != null)
            {
                int slot = 0;
                foreach (var lens in list)
                {
                    ++slot;
                    int pow = box * slot * lens.Focus;
                    sum += pow;
                }
            }
        }

        return sum;
    }

    static int HashKey(string s)
    {
        int h = 0;
        foreach (var ch in s)
        {
            int c = (int)ch;
            h += c;
            h *= 17;
            h %= 256;
        }
        return h;
    }
}
