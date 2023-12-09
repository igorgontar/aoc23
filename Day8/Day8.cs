using Utils.Profiler;

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ColorConsole.Enabled = true;

            Profiler.Debug("Current dir: {0}\n", Environment.CurrentDirectory);

            Profiler.Warn("=== AoC 2023 Day8 ===");

            using(Profiler.CheckPoint("Puzzle1"))
            {
                Profiler.Trace("Test  : {0} (6)", Puzzle1.Solve("InputTest1.txt"));
                Profiler.Trace("Result: {0} (16697)", Puzzle1.Solve("Input.txt"));
            }        
            
            using(Profiler.CheckPoint("Puzzle2"))
            {
                using(Profiler.CheckPoint("Test"))
                    Profiler.Trace("Result  : {0} (6)", Puzzle2.Solve("InputTest2.txt"));
                
                using(Profiler.CheckPoint("Input"))
                    Profiler.Trace("Result: {0} (10668805667831)", Puzzle2.Solve("Input.txt"));
            }        
            
            // using(Profiler.CheckPoint("Puzzle2a"))
            // {
            //     Profiler.Trace("Test  : {0} (6)", Puzzle2b.Solve("InputTest2.txt"));
            //     Profiler.Trace("Result: {0} (10668805667831)", Puzzle2b.Solve("Input.txt"));
            // }        
        }
        catch (Exception ex)
        {
            Profiler.Error("Main() - {0}", ex);
        }
    }
}
