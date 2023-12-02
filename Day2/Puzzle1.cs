using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;

class Puzzle1
{
    class Bag : Dictionary<string, int> { }
    
    static readonly Bag bag =  new Bag()
    { 
        {"red", 12}, 
        {"green", 13}, 
        {"blue", 14}, 
    };

    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries;

    static bool IsValidGame(string input)
    {
        string[] rounds = input.Split(';', splitOptions);      
        bool valid = true;
        foreach(string r in rounds)
        {
            string[] set = r.Split(',', 3, splitOptions);   
            foreach(string s in set) 
            {
                string[] cube = s.Split(' ', 3, splitOptions);   
                int count = int.Parse(cube[0]);
                string color = cube[1];
                int cubesCountInTheBag = bag[color];
                if(count > cubesCountInTheBag)
                {
                    valid = false;
                }
            }  
            if(!valid) // no sense to process remaining rounds
                break;
        }
        return valid;
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
            if(IsValidGame(game))
            {
                string gameId = split[0];
                string sid = gameId.Split(' ',splitOptions)[1];
                int gid = int.Parse(sid);
                sum += gid;
            }
        }
        return sum;
    }
}
