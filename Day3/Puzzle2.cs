using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

using Utils.Profiler;

class Puzzle2
{
    public static int Solve(string file)
    {
        using var reader = new StreamReader(file);
        return Solve(reader);
    }

    public static int Solve(TextReader reader)
    {
        Engine engine = new();

        // construct engine
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            engine.AddRow(line);
        }

        int sum = 0;
        // collect parts
        PartCollection parts = new PartCollection(engine.DY);
        for (int y = 0; y < engine.DY; y++)
        {
            string part = null;
            int partX = -1;
            for (int x = 0; x < engine.DX; x++)
            {
                char c = engine[y, x];
                if (c > 2) // it's digit
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
                        {
                            parts.Add(new Part(partNo, partX, y, part.Length));
                            sum += partNo;
                        }
                        part = null; // reset current part token
                    }
                }
            }

            // flush at the end of line
            if (part != null)
            {
                int partNo = engine.CheckPotentialPart(part, partX, y);
                if(partNo >= 0)
                {
                    parts.Add(new Part(partNo, partX, y, part.Length));
                    sum += partNo;
                }
            }
        }

        Profiler.Trace("Part1 result: {0}", sum); // see if we have brocken anything

        // find gears
        sum = 0;
        for (int y = 0; y < engine.DY; y++)
        {
            for (int x = 0; x < engine.DX; x++)
            {
                char c = engine[y, x];
                if (c == 2) // it's a gear
                {
                    var near = parts.FindPartsAroundGear(x, y);
                    if(near.Count == 2)
                    {
                        int ratio = near.Aggregate((e1,e2) => e1*e2);
                        sum += ratio;
                    }
                }
            }
        }

        return sum;
    }

}
