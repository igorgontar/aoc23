using System.IO;

class Program
{
    static readonly TextWriter log = Console.Out;

    public static void Main(string[] args)
    {
        try
        {
            log.WriteLine("=== AoC 2023 Day3 - Puzzle 1 ===");
            log.WriteLine("Test: {0} expected X", Puzzle1.Solve(Input.TestData));
            log.WriteLine("Result: {0} (X)", Puzzle1.Solve(Input.Data));
            log.WriteLine();
            
            log.WriteLine("=== AoC 2023 Day3 - Puzzle 2 ===");
            log.WriteLine("Test: {0} expected X", Puzzle2.Solve(Input.TestData));
            log.WriteLine("Result: {0} (X)", Puzzle2.Solve(Input.Data));
            log.WriteLine();
        }
        catch (Exception ex)
        {
            log.WriteLine("Main() - {0}", ex);
        }
    }
}