using Utils.Profiler;

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ColorConsole.Enabled = true;

            Profiler.Debug("Current dir: {0}\n", Environment.CurrentDirectory);

            Profiler.Warn("=== AoC 2023 Day9 ===");

            using(Profiler.CheckPoint("Puzzle1"))
            {
                using(Profiler.CheckPoint("Test"))
                    Profiler.Trace("Result  : {0} (114)", Puzzle1.Solve("InputTest.txt"));
                
                using(Profiler.CheckPoint("Input"))
                    Profiler.Trace("Result: {0} (1757008019)", Puzzle1.Solve("Input.txt"));
            }        
            
            using(Profiler.CheckPoint("Puzzle2"))
            {
                using(Profiler.CheckPoint("Test"))
                    Profiler.Trace("Result  : {0} (2)", Puzzle2.Solve("InputTest.txt"));
                
                using(Profiler.CheckPoint("Input"))
                    Profiler.Trace("Result: {0} (995)", Puzzle2.Solve("Input.txt"));
            }        
        }
        catch (Exception ex)
        {
            Profiler.Error("Main() - {0}", ex);
        }
    }
}
