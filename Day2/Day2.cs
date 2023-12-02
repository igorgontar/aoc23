using System.IO;

class Program
{
    static readonly TextWriter log = Console.Out;

    public static void Main(string[] args)
    {
        try
        {
            log.WriteLine("=== AoC 2023 Day2 - Puzzle 1 ===");
            log.WriteLine("Test: {0} expected 8", Puzzle1.Solve(Input.TestData));
            log.WriteLine("Result: {0} (2720)", Puzzle1.Solve(Input.Data));
            log.WriteLine();
            
            log.WriteLine("=== AoC 2023 Day2 - Puzzle 2 ===");
            log.WriteLine("Test: {0} expected 2286", Puzzle2.Solve(Input.TestData));
            log.WriteLine("Result: {0} (71535)", Puzzle2.Solve(Input.Data));
            log.WriteLine();
        }
        catch (Exception ex)
        {
            log.WriteLine("Main() - {0}", ex);
        }
    }
}