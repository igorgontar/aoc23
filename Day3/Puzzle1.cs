using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

class Puzzle1
{
    public static int Solve(string file)
    {
        using var reader = new StreamReader(file);
        return Solve(reader);
    }

    public static int Solve(TextReader reader)
    {
        Engine engine = new();

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            engine.AddRow(line);
        }

        int sum = 0;

        for (int y = 0; y < engine.DY; y++)
        {
            string part = null;
            int partX = -1;
            for (int x = 0; x < engine.DX; x++)
            {
                char c = engine[y, x];
                if (c > 2) // it's a digit
                {
                    if(part == null) // initialize part buffer
                    {
                        part = "";
                        partX = x;
                    };
                    part += c;
                }
                else
                {
                    if (part != null)
                    {
                        int partNo = engine.CheckPotentialPart(part, partX, y);
                        if(partNo >= 0)
                            sum += partNo;
                        //Console.WriteLine("part {0}: {1}", part, isPart);
                        part = null; // reset current part token
                    }
                }
            }

            // flush at the end of line
            if (part != null)
            {
                int partNo = engine.CheckPotentialPart(part, partX, y);
                if(partNo >= 0)
                    sum += partNo;
                //Console.WriteLine("part {0}: {1}", part, isPart);
            }
        }

        return sum;
    }

}
