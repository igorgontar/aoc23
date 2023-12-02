using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;

class Puzzle1
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries;

    public static int Solve(string input)
    {
        var reader = new StringReader(input);
        int sum = 0;
        string? line;

        while((line = reader.ReadLine()) != null)
        {
            if(string.IsNullOrWhiteSpace(line)) continue;
            
            string[] split = line.Split(':',2,splitOptions);  
        }
        return sum;
    }
}
