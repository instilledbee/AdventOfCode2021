using AdventOfCode2021.Infrastructure;
using System.Text;

namespace AdventOfCode2021.Solutions
{
    [PuzzleId(3)]
    internal class Puzzle3 : BasePuzzle
    {
        private string[] _parsedInputs;

        public Puzzle3(bool verboseOutput, StreamReader inputs) : base(verboseOutput, inputs)
        {
            _parsedInputs = Inputs.ReadToEnd().Split(Environment.NewLine);
        }

        public override int SolveFirst()
        {
            // use the first row to determine number of columns in inputs
            var columnLength = _parsedInputs[0].Length;

            var gammaBinaryValue = new StringBuilder();
            var epsilonBinaryValue = new StringBuilder();

            for (var i = 0; i < columnLength; ++i)
            {
                var input = GetDigitsPerColumn(_parsedInputs, i);

                char gammaBit = GetMostCommonBit(input);
                gammaBinaryValue.Append(gammaBit);

                char epsilonBit = GetLeastCommonBit(input);
                epsilonBinaryValue.Append(epsilonBit);
            }

            int gammaDecimalValue = Convert.ToInt32(gammaBinaryValue.ToString(), 2);
            Logger.WriteLine($"Gamma Value: {gammaDecimalValue}");

            int epsilonDecimalValue = Convert.ToInt32(epsilonBinaryValue.ToString(), 2);
            Logger.WriteLine($"Epsilon Value: {epsilonDecimalValue}");

            return gammaDecimalValue * epsilonDecimalValue;
        }

        public override int SolveSecond()
        {
            // start with all the rows
            var oxygenGeneratorPossibleValues = _parsedInputs;
            int currentColumn = 0;

            do 
            {
                var column = GetDigitsPerColumn(oxygenGeneratorPossibleValues, currentColumn);
                var mostCommonBit = GetMostCommonBit(column);

                oxygenGeneratorPossibleValues = oxygenGeneratorPossibleValues.Where(value => value[currentColumn] == mostCommonBit).ToArray();
                currentColumn++;
            } while (oxygenGeneratorPossibleValues.Count() > 1);

            Logger.WriteLine($"Found oxygen generator rating input: {oxygenGeneratorPossibleValues.First()}");

            var co2scrubberPossibleValues = _parsedInputs;
            currentColumn = 0;

            do
            {
                var column = GetDigitsPerColumn(co2scrubberPossibleValues, currentColumn);
                var leastCommonBit = GetLeastCommonBit(column);

                co2scrubberPossibleValues = co2scrubberPossibleValues.Where(value => value[currentColumn] == leastCommonBit).ToArray();
                currentColumn++;
            } while (co2scrubberPossibleValues.Count() > 1);

            Logger.WriteLine($"Found CO2 scrubber rating input: {co2scrubberPossibleValues.First()}");

            int finalOxygenValue = Convert.ToInt32(oxygenGeneratorPossibleValues.First(), 2);
            int finalCo2Value = Convert.ToInt32(co2scrubberPossibleValues.First(), 2);

            Logger.WriteLine($"Final oxygen generator rating: {finalOxygenValue}");
            Logger.WriteLine($"Final CO2 scrubber rating: {finalCo2Value}");

            return finalCo2Value * finalOxygenValue;
        }

        /// <summary>
        /// Returns a new string from each digit in the specified column
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        private string GetDigitsPerColumn(string[] inputs, int columnIndex)
            => new string(inputs.Select(line => line[columnIndex]).ToArray());

        /// <summary>
        /// Get the most recurring bit in a given string.
        /// If 1s and 0s are equal, prefer 1
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private char GetMostCommonBit(string column)
            => column.OrderByDescending(bit => bit)
                    .GroupBy(bit => bit)
                    .OrderByDescending(bit => bit.Count())
                    .Take(1)
                    .Select(bit => bit.Key)
                    .First();

        /// <summary>
        /// Get the least recurring bit in a given string.
        /// If 1s and 0s are equal, prefer 0
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private char GetLeastCommonBit(string input)
            => input.OrderBy(bit => bit)
                    .GroupBy(bit => bit)
                    .OrderBy(bit => bit.Count())
                    .Take(1)
                    .Select(bit => bit.Key)
                    .First();
    }
}
