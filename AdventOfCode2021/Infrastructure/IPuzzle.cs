namespace AdventOfCode2021.Infrastructure
{

    internal interface IPuzzle
    {
        int SolveFirst(StreamReader inputs);

        int SolveSecond(StreamReader inputs);
    }
}
