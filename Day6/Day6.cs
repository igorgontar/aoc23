using Utils.Profiler;

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ColorConsole.Enabled = true;

            Profiler.Debug("Current dir: {0}\n", Environment.CurrentDirectory);

            Profiler.Warn("=== AoC 2023 Day6 ===");

            using(Profiler.CheckPoint("Puzzle1"))
            {
                Profiler.Trace("Test  : {0} (288)",    Puzzle1.Solve("InputTest.txt"));
                Profiler.Trace("Result: {0} (160816)", Puzzle1.Solve("Input.txt"));
            }        
            
            using(Profiler.CheckPoint("Puzzle2"))
            {
                Profiler.Trace("Test  : {0} (71503)",    Puzzle2.Solve("InputTest.txt"));
                Profiler.Trace("Result: {0} (46561107)", Puzzle2.Solve("Input.txt"));
            }        
        }
        catch (Exception ex)
        {
            Profiler.Error("Main() - {0}", ex);
        }
    }
}
