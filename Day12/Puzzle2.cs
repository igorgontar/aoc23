using System.Text;
using Utils.Profiler;
using static IOUtils;
using static Algo;

class Puzzle2
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        long sum = 0;
        using var reader = new StreamReader(file);
        foreach (var line in reader.NonEmptyLines())
        {
            var split = line.Split(' ', splitOptions);
            string sp = split[0]; 
            string sr = split[1];
            
            string springs = string.Join('?',sp,sp,sp,sp,sp);
            string dams = string.Join(',',sr,sr,sr,sr,sr);
            int[] damaged = dams.Split(',', splitOptions).Select(s => int.Parse(s)).ToArray();
            
            int count = CountWays(springs, damaged);
            sum += count;
        }

        return sum;
    }

    static int CountWays(string l, int[] r)
    {
        int res = 0;
        if(l=="")
            return 0;

        return res;
    }


// def process(l, r):
//     res = 0
//     if l == "":
//         return 1 if r == () else 0

//     if r == ():
//         return 0 if "#" in l else 1

//     k = (l, r)

//     if k in cache:
//         return cache[k]

//     if l[0] in ".?":
//         res += process(l[1:], r)

//     if l[0] in "#?":
//         if (
//             r[0] <= len(l)
//             and "." not in l[: r[0]]
//             and (r[0] == len(l) or l[r[0]] != "#")
//         ):
//             res += process(l[r[0] + 1 :], r[1:])
//     cache[k] = res
//     return res

}
