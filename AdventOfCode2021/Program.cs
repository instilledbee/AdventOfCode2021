using AdventOfCode2021.Infrastructure;
using System.Reflection;

public static class Program
{
    static void Main(string[] args)
    {
        int puzzleId;

        try
        {
            puzzleId = Convert.ToInt32(args[0]);
        }
        catch
        {
            Console.WriteLine("Please pass a puzzle number.");
            return;
        }

        var puzzle = FindPuzzle(puzzleId);

        if (puzzle != null)
        {
            var inputs = GetInputs(puzzleId);

            int solutionA = puzzle.SolveFirst(inputs);
            int solutionB = puzzle.SolveSecond(inputs);

            Console.WriteLine($"Solution to Puzzle #{puzzleId}A: {solutionA}");
            Console.WriteLine($"Solution to Puzzle #{puzzleId}B: {solutionB}");
        }
        else
        {
            Console.WriteLine($"Unable to find solution for puzzle #{puzzleId}");
            return;
        }
	}

    static IPuzzle FindPuzzle(int puzzleId)
    {
        var matchingPuzzle = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t =>
        {
            var attr = t.GetCustomAttributes(typeof(PuzzleIdAttribute), true).FirstOrDefault() as PuzzleIdAttribute;

            return attr?.PuzzleId == puzzleId;
        });

        if (matchingPuzzle != null)
            return Activator.CreateInstance(matchingPuzzle) as IPuzzle;

        else
            return null;
    }

    static StreamReader GetInputs(int puzzleId)
    {
        return new StreamReader($@".\\Inputs\{puzzleId}.txt");
    }
}