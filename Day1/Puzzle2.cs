using System;
using System.IO;
using System.Linq;

class Puzzle2
{
    static string[] words = new [] {
        "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
    }; 

    static string[] digits = new [] {
        "1", "2", "3", "4", "5", "6", "7", "8", "9",
    }; 

    public static int Solve(string input)
    {
        var reader = new StringReader(input);
        int sum = 0;
        string? line;

        while((line = reader.ReadLine()) != null)
        {
            if(string.IsNullOrWhiteSpace(line)) continue;
            
            int first = -1, last = -1;
            int min = int.MaxValue, max = int.MinValue;
            
            for(int n=0; n < digits.Length; n++)
            {
                int i = line.IndexOf(digits[n]);
                if(i >= 0 && i < min)
                    { min = i; first = n + 1; }

                i = line.IndexOf(words[n]);
                if(i >= 0 && i < min)
                    { min = i; first = n + 1; }

                i = line.LastIndexOf(digits[n]);
                if(i >= 0 && i > max)
                    { max = i; last= n + 1; }

                i = line.LastIndexOf(words[n]);
                if(i >= 0 && i > max)
                    { max = i; last = n + 1; }
            }

            if(first < 0 || last < 0)
                throw new Exception($"line does not contain calibration numbers: {line}");

            int num = int.Parse($"{first}{last}");
            sum += num;
        }
        return sum;
    }
}
