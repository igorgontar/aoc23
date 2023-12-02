using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;

class Puzzle2
{
    class Bag : Dictionary<string, int> { }

    static Bag CreateEmptyBag() 
    { 
        return new Bag()
        { 
            {"red", 0}, 
            {"green", 0}, 
            {"blue", 0}, 
        };
    }

    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries;

    static int CalcGamePower(string input)
    {
        Bag bag = CreateEmptyBag();
        string[] rounds = input.Split(';', splitOptions);      
        foreach(string r in rounds)
        {
            string[] set = r.Split(',', 3, splitOptions);   
            foreach(string s in set) 
            {
                string[] cube = s.Split(' ', 3, splitOptions);   
                int count = int.Parse(cube[0]);
                string color = cube[1];
                
                if(count > bag[color])
                    bag[color] = count;
            }  
        }
        int power = bag.Values.Aggregate((x,y) => x*y);
        return power;
    }

    public static int Solve(string input)
    {
        var reader = new StringReader(input);
        int sum = 0;
        string? line;

        while((line = reader.ReadLine()) != null)
        {
            if(string.IsNullOrWhiteSpace(line)) continue;
            
            string[] split = line.Split(':',2,splitOptions);  
            string game = split[1];
            int power = CalcGamePower(game);
            sum += power;
        }
        return sum;
    }
}
