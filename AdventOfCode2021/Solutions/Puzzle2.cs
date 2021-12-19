using AdventOfCode2021.Infrastructure;

namespace AdventOfCode2021.Solutions
{
    [PuzzleId(2)]
    internal class Puzzle2 : BasePuzzle
    {
        public Puzzle2(bool verboseOutput, StreamReader inputs) : base(verboseOutput, inputs)
        {
            _inputs = ParseInputs(Inputs);
        }

        public override int SolveFirst()
        {
            int horizontalPosition = 0;
            int depth = 0;

            foreach (var positionInput in _inputs)
            {
                switch (positionInput.Direction)
                {
                    case "forward":
                        horizontalPosition += positionInput.Distance;
                        break;

                    case "down":
                        depth += positionInput.Distance;
                        break;

                    case "up":
                        depth -= positionInput.Distance;
                        break;
                }
            }

            return horizontalPosition * depth;
        }

        public override int SolveSecond()
        {
            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;

            foreach (var positionInput in _inputs)
            { 
                switch (positionInput.Direction)
                {
                    case "forward":
                        horizontalPosition += positionInput.Distance;
                        depth += (positionInput.Distance * aim);
                        break;

                    case "down":
                        aim += positionInput.Distance;
                        break;

                    case "up":
                        aim -= positionInput.Distance;
                        break;
                }

                Logger.WriteLine($"{positionInput}\t{horizontalPosition}\t{depth}\t{aim}");
            }

            return horizontalPosition * depth;
        }

        private List<PositionInput> ParseInputs(StreamReader inputs)
        {
            return inputs.ReadToEnd()
                         .Split(Environment.NewLine)
                         .Select(line => new PositionInput(line))
                         .ToList();
        }

        private List<PositionInput> _inputs;

        private class PositionInput
        {
            public string Direction { get; set; }

            public int Distance { get; set; }

            public PositionInput(string line)
            {
                string[] values = line.Split(' ');

                Direction = values[0];
                Distance = int.Parse(values[1]);
            }

            public override string ToString()
            {
                return $"{Direction} {Distance}";
            }
        }
    }
}
