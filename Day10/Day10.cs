using Utils.Profiler;

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ColorConsole.Enabled = true;

            //Profiler.Debug("Current dir: {0}\n", Environment.CurrentDirectory);

            Profiler.Warn("=== AoC 2023 Day10 ===");

            using(Profiler.CheckPoint("Puzzle1"))
            {
                using(Profiler.CheckPoint("Test"))
                    Profiler.Trace("Result  : {0} ()", Puzzle1.Solve("inputTest.txt"));
                
                // using(Profiler.CheckPoint("Input"))
                //     Profiler.Trace("Result: {0} ()", Puzzle1.Solve("input.txt"));
            }        
            
            // using(Profiler.CheckPoint("Puzzle2"))
            // {
            //     using(Profiler.CheckPoint("Test"))
            //         Profiler.Trace("Result  : {0} ()", Puzzle2.Solve("inputTest.txt"));
                
            //     using(Profiler.CheckPoint("Input"))
            //         Profiler.Trace("Result: {0} ()", Puzzle2.Solve("input.txt"));
            // }        
        }
        catch (Exception ex)
        {
            Profiler.Error("Main() - {0}", ex);
        }
    }
}
