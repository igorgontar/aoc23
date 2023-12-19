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

    public static int CountArrangements(string springs, int[] runs)
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
            
            int[] nums = CalcDamaged(spa);
            if(nums.eq(runs))
                count++;
        }

        //int[] d = CalcDamaged(springs);
        return count;
    }

    static int[] CalcDamaged(char[] springs)
    {
        List<int> res = new();
        
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
                    res.Add(runLen);
                runLen = 0;
            }
            else
                throw new Exception($"resolved spring map can't contain '{c}'");
        }

        if(runLen != 0)
            res.Add(runLen);

        return res.ToArray();
    }
}