using AdventOfCode2021.Infrastructure;
using System.Text;

namespace AdventOfCode2021.Solutions
{
	[PuzzleId(1)]
    internal class Puzzle1 : IPuzzle
    {
		public int SolveFirst(StreamReader inputs)
        {
			return Solve(inputs, 1);
		}

        public int SolveSecond(StreamReader inputs)
		{
			return Solve(inputs, 3);
		}

		private int Solve(StreamReader inputs, int windowSize)
        {
			int increasedCount = 0;
			int decreasedCount = 0;
			int noChangeCount = 0;

			Queue<int> values = new Queue<int>();
			int? previousSum = null;
			int currentSum;

			inputs.BaseStream.Position = 0;

			do
			{
				var value = Convert.ToInt32(inputs.ReadLine());
				values.Enqueue(value);

				currentSum = values.Sum();

				if (values.Count < windowSize) continue;

				Console.Write(OutputStack(values));

				if (previousSum.HasValue)
				{
					if (previousSum > currentSum)
					{
						Console.WriteLine(" - decreased");
						decreasedCount++;
					}
					else if (previousSum < currentSum)
					{
						Console.WriteLine(" - increased");
						increasedCount++;
					}
					else
					{
						Console.WriteLine(" - no change");
						noChangeCount++;
					}
				}
				else
				{
					Console.WriteLine();
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
