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

        bool outputVerbose = args.Contains("--verbose");

        var inputs = GetInputs(puzzleId);
        var puzzle = FindPuzzle(puzzleId, outputVerbose, inputs);

        if (puzzle != null)
        {
            int solutionA = puzzle.SolveFirst();
            Console.WriteLine($"Solution to Puzzle #{puzzleId}A: {solutionA}");

            int solutionB = puzzle.SolveSecond();
            Console.WriteLine($"Solution to Puzzle #{puzzleId}B: {solutionB}");
        }
        else
        {
            Console.WriteLine($"Unable to find solution for puzzle #{puzzleId}");
            return;
        }
	}

    static BasePuzzle FindPuzzle(int puzzleId, bool outputVerbose, StreamReader inputs)
    {
        var matchingPuzzle = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t =>
        {
            var attr = t.GetCustomAttributes(typeof(PuzzleIdAttribute), true).FirstOrDefault() as PuzzleIdAttribute;

            return attr?.PuzzleId == puzzleId;
        });

        if (matchingPuzzle != null)
            return Activator.CreateInstance(matchingPuzzle, args: new object[] { outputVerbose, inputs }) as BasePuzzle;

        else
            return null;
    }

    static StreamReader GetInputs(int puzzleId)
    {
        return new StreamReader($@".\\Inputs\{puzzleId}.txt");
    }
}