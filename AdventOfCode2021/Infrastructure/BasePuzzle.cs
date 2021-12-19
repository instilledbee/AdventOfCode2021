using AdventOfCode2021.Infrastructure.Logging;

namespace AdventOfCode2021.Infrastructure
{

    internal abstract class BasePuzzle
    {
        public abstract int SolveFirst();

        public abstract int SolveSecond();

        protected ILogger Logger { get; init; }

        protected StreamReader Inputs { get; init; }

        public BasePuzzle(bool verboseOutput, StreamReader inputs)
        {
            Logger = verboseOutput ? new ConsoleLogger() : new NullLogger();
            Inputs = inputs;
        }
    }
}
