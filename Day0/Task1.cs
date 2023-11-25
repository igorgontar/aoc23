using System;
using System.Collections.Generic;

namespace Day0
{
    class Task1
    {
        string[] input1 = new [] {
            "abc cba acb sgsag sagsfag xya", 
            "abc cba acb sgsag sagsfag", 
            "abc cba abc sgsag sagsfag", 
            "abc cba acb sgsag sagsfag", 
            }; 

        int Solve(string[] input)
        {
            int count = 0;
            foreach(var s in input)
            {
                if(ContainsSameWords(s))
                {
                    Console.WriteLine("bad   - {0}", s);        
                }
                else
                {
                    count++;
                    Console.WriteLine("good  - {0}", s);        
                }

            }
            return count;    
        }

        bool ContainsSameWords(string x)
        {
            var list = x.Split(' ',StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries);
            var set = new HashSet<string>();
            foreach(var s in list)
            {
                if(set.Contains(s))
                    return true;
                set.Add(s);    
            }
            return false;
        }

        public void Run()
        {
            Console.WriteLine($"Advent of Code 2023 {this.GetType()}:");
            
            Console.WriteLine("Input 1");        
            int res = Solve(input1);
            Console.WriteLine("Count = {0}", res);        

        }
    }
}