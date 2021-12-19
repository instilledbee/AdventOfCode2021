using AdventOfCode2021.Infrastructure.Logging;

namespace AdventOfCode2021.Infrastructure
{

    internal abstract class BasePuzzle
    {
        public abstract int SolveFirst(StreamReader inputs);

        public abstract int SolveSecond(StreamReader inputs);

        protected ILogger Logger { get; init; }

        public BasePuzzle(bool verboseOutput)
        {
            Logger = verboseOutput ? new ConsoleLogger() : new NullLogger();
        }
    }
}
