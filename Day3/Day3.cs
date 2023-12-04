using System.IO;
using Utils.Profiler;

class Program
{
    static readonly TextWriter log = Console.Out;

    public static void Main(string[] args)
    {
        try
        {
            ColorConsole.Enabled = true;

            log.WriteLine("Current dir: {0}\n", Environment.CurrentDirectory);

            log.WriteLine("=== AoC 2023 Day3 ===");

            using(Profiler.CheckPoint("Puzzle1"))
            {
                var res = Puzzle1.Solve("InputTest.txt");    
                Profiler.Trace("Test  : {0} (4361)", res);
                
                res = Puzzle1.Solve("Input.txt");    
                Profiler.Trace("Result: {0} (528799)", res);
            }        
            
            log.WriteLine();
            
            using(Profiler.CheckPoint("Puzzle2"))
            {
                var res = Puzzle2.Solve("InputTest.txt");    
                Profiler.Trace("Test  : {0} (467835)", res);
                
                res = Puzzle2.Solve("Input.txt");    
                Profiler.Trace("Result: {0} (84907174)", res);
            }        
            log.WriteLine();
        }
        catch (Exception ex)
        {
            log.WriteLine("Main() - {0}", ex);
        }
    }
}
