using AdventOfCode2021.Extensions;
using AdventOfCode2021.Infrastructure;
using System.Text;

namespace AdventOfCode2021.Solutions
{
	[PuzzleId(1)]
    internal class Puzzle1 : BasePuzzle
    {
        public Puzzle1(bool verboseOutput, StreamReader inputs) : base(verboseOutput, inputs)
        {
        }

        public override int SolveFirst()
        {
			return Solve(Inputs, 1);
		}

        public override int SolveSecond()
		{
			return Solve(Inputs, 3);
		}

		private int Solve(StreamReader inputs, int windowSize)
        {
			int increasedCount = 0;
			int decreasedCount = 0;
			int noChangeCount = 0;

			Queue<int> values = new Queue<int>();
			int? previousSum = null;
			int currentSum;

			inputs.ResetPosition();

			do
			{
				int readValue = Convert.ToInt32(inputs.ReadLine());
				values.Enqueue(readValue);

				currentSum = values.Sum();

				if (values.Count < windowSize) continue;

				Logger.Write(OutputStack(values));

				if (previousSum.HasValue)
				{
					if (previousSum > currentSum)
					{
						Logger.WriteLine(" - decreased");
						decreasedCount++;
					}
					else if (previousSum < currentSum)
					{
						Logger.WriteLine(" - increased");
						increasedCount++;
					}
					else
					{
						Logger.WriteLine(" - no change");
						noChangeCount++;
					}
				}
				else
				{
					Logger.WriteLine();
				}

				previousSum = currentSum;
				values.Dequeue();
			} while (!inputs.EndOfStream);

			return increasedCount;
		}

		private string OutputStack(Queue<int> values)
        {
			var sb = new StringBuilder();

			sb.Append(string.Join(" + ", values));

			if (values.Count() > 1)
			{
				sb.Append(" = ");
				sb.Append(values.Sum());
			}

			return sb.ToString();
        }
    }
}
