using Utils.Collections;

class Key
{
    public string line;
    public int[] runs;

    public override bool Equals(object obj)
    {
        Key o = (Key)obj;
        if(o == null) return false;

        return line == o.line && runs.eq(o.runs);
    }

    public override int GetHashCode()
    {
        int code = line.GetHashCode();
        for(int i=0; i<runs.Length; i++)
        {
            int h = runs[i].GetHashCode();
            code ^= (h << 4);   
        }

        return code;
    }
}

class Cache : Dictionary<Key,long> {}

class Algo
{
    public static IEnumerable<char[]> CharCombinations(int N)
    {
        if(N > 63)
            throw new Exception("max number of ? can't be > 63");

        long c = 1 << N;
        for (int i = 0; i < c; i++) 
        {
            long mask = 1;
            char[] ret = new char[N];
            for(int n = 0; n < N; n++)
            {
                long x = i & mask;
                ret[n] = x==0 ? '.' : '#';
                mask <<= 1;
            }
            yield return ret;
        }
    }

    public static int CountWays1(string springs, int[] runs)
    {
        char[] spa = springs.ToCharArray();
        
        List<int> qpos = new();
        for(int i = 0; i < spa.Length; i++)
        {
            if(spa[i] == '?')
                qpos.Add(i);
        }

        int count = 0;
        foreach(var ch in CharCombinations(qpos.Count))
        {
            spa = springs.ToCharArray(); // create new copy
            for(int i=0; i<qpos.Count; i++)
            {
                spa[qpos[i]] = ch[i]; // # or . 
            }
            //spa.print();
            
            int[] nums = null; 
            bool match = CalcDamaged(spa, runs, out nums);
            if(match && nums.eq(runs))
                count++;
        }

        //int[] d = CalcDamaged(springs);
        return count;
    }

    static bool CalcDamaged(char[] springs, int[] runs, out int[] nums)
    {
        List<int> res = new();
        nums = new int[] {};

        int runLen = 0;
        foreach(var c in springs)
        {
            if(c == '#')
            {
                runLen++;
            }
            else if (c == '.')
            {
                if(runLen > 0)
                {
                    res.Add(runLen);
                    if(res.Count > runs.Length)
                        return false;

                    if(res[res.Count-1] != runs[res.Count-1])
                        return false;
                }
                runLen = 0;
            }
            else
                throw new Exception($"resolved spring map can't contain '{c}'");
        }

        if(runLen != 0)
            res.Add(runLen);

        nums = res.ToArray();
        return true;
    }

    public static long CountWays2(Cache cache, string l, int[] r)
    {
        long res;
        if (l.Length == 0)
            return r.Length == 0 ? 1 : 0;

        if (r.Length == 0)
            return l.Contains('#') ? 0 : 1;

        var key = new Key { line = l, runs = r };

        if (cache.TryGetValue(key, out res))
            return res;

        if (l[0].inside('.', '?'))
            res += CountWays2(cache, l.Substring(1), r);

        if (l[0].inside('#', '?'))
        {
            if (r[0] <= l.Length
                && !l.Substring(0, r[0]).Contains('.')
                && (r[0] == l.Length || l[r[0]] != '#'))
            {
                int c = r[0] + 1;
                if(c >= l.Length)
                    c = l.Length;

                res += CountWays2(cache, l.Substring(c), r.Skip(1).ToArray());
            }
        }

        cache[key] = res;
        return res;
    }

}