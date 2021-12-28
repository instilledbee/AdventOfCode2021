using AdventOfCode2021.Infrastructure;
using System.Text;

namespace AdventOfCode2021.Solutions
{
    [PuzzleId(4)]
    internal class Puzzle4 : BasePuzzle
    {
        private List<int> _bingoRolls = new List<int>();
        private List<BingoCard> _bingoCards = new List<BingoCard>();

        public Puzzle4(bool verboseOutput, StreamReader inputs) : base(verboseOutput, inputs)
        {
            ParseInputs();
        }

        public override int SolveFirst()
        {
            Simulate();

            BingoCard winningCard = _bingoCards.OrderBy(card => card.TurnsToWin).FirstOrDefault();

            return winningCard.UnmarkedTotal * winningCard.WinningRoll;
        }

        public override int SolveSecond()
        {
            Simulate();

            BingoCard winningCard = _bingoCards.OrderByDescending(card => card.TurnsToWin).FirstOrDefault();

            return winningCard.UnmarkedTotal * winningCard.WinningRoll;
        }

        /// <summary>
        /// Iterate through each number roll in the input, and simulate marking each possible card
        /// </summary>
        private void Simulate()
        {
            Parallel.ForEach(_bingoCards, card =>
            {
                card.Reset();

                foreach (var roll in _bingoRolls)
                {
                    Logger.Clear();
                    Logger.WriteLine($"Current number: {roll}");

                    card.TurnsToWin++;

                    var bingoNumber = card.FirstOrDefault(roll);

                    if (bingoNumber != null)
                        bingoNumber.IsMarked = true;

                    Logger.WriteLine(card.ToString());

                    if (card.IsWinner)
                    {
                        card.WinningRoll = roll;

                        Logger.WriteLine("Winner!");
                        Logger.WriteLine($"Turns: {card.TurnsToWin}");
                        Logger.WriteLine($"Winning roll: {card.WinningRoll}");

                        break;
                    }
                }
            });
        }

        private void ParseInputs()
        {
            _bingoRolls = Inputs.ReadLine()
                                .Split(',')
                                .Select(x => Convert.ToInt32(x))
                                .ToList();

            // this will store the current bingo card being parsed from the inputs
            var currentBingoCard = new BingoCard();

            // this will store the current card row index being parsed
            int currentRow = 0;

            do
            {
                string line = Inputs.ReadLine();

                if (String.IsNullOrWhiteSpace(line)) continue;

                // splitting by space sometimes leaves out empty strings due to the way the input is formatted
                // make sure we are only selecting valid strings to avoid errors
                string[] validNumbers = line.Split(' ')
                                            .Where(num => !string.IsNullOrWhiteSpace(num))
                                            .ToArray();

                List<BingoNumber> cardRow = validNumbers.Select(num => new BingoNumber()
                                          {
                                              Number = Convert.ToInt32(num),
                                              IsMarked = false
                                          })
                                          .ToList();

                for (int i = 0; i < cardRow.Count; ++i)
                {
                    currentBingoCard.Numbers[currentRow, i] = cardRow[i];
                }

                if (currentRow == 4)
                {
                    _bingoCards.Add(currentBingoCard);

                    // reset to the next card to add
                    currentRow = 0;
                    currentBingoCard = new BingoCard();
                }
                else
                {
                    currentRow++;
                }
            } while (!Inputs.EndOfStream);
        }

        /// <summary>
        /// Represents a 5x5 grid of BingoNumbers
        /// </summary>
        private class BingoCard
        {
            public BingoNumber[,] Numbers { get; private set; }

            /// <summary>
            /// How many turns it took for this card to win, marking in order of the input rolls
            /// </summary>
            public int TurnsToWin { get; set; }

            /// <summary>
            /// The number that made this card a winning card
            /// </summary>
            public int WinningRoll { get; set; }
            
            public BingoCard()
            {
                Numbers = new BingoNumber[5, 5];
            }

            /// <summary>
            /// Returns a string that visually represents the BingoCard's numbers
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                var sb = new StringBuilder();

                for (int x = 0; x < Numbers.GetLength(0); x++)
                {
                    for (int y = 0; y < Numbers.GetLength(1); y++)
                    {
                        BingoNumber value = Numbers[x, y];
                        sb.Append(value.ToString());
                    }

                    sb.AppendLine();
                }

                return sb.ToString();
            }

            /// <summary>
            /// Find an instance of a specific BingoNumber
            /// </summary>
            /// <param name="number"></param>
            /// <returns></returns>
            public BingoNumber FirstOrDefault(int number)
            {
                foreach (var item in Numbers)
                {
                    if (item.Number == number)
                        return item;
                }

                return null;
            }

            /// <summary>
            /// Get the sum of all the unmarked numbers in this BingoCard instance
            /// </summary>
            public int UnmarkedTotal
            {
                get
                {
                    int total = 0;

                    foreach (var item in Numbers)
                    {
                        if (!item.IsMarked)
                            total += item.Number;
                    }

                    return total;
                }
            }

            /// <summary>
            /// Returns true if the card has a marked straight row or column
            /// </summary>
            public bool IsWinner
            {
                get
                {
                    var isWinner = false;

                    // check rows
                    for (int x = 0; x < Numbers.GetLength(0); x++)
                    {
                        int markedInRow = 0;

                        for (int y = 0; y < Numbers.GetLength(1); y++)
                        {
                            if (Numbers[x, y].IsMarked)
                                markedInRow++;
                        }

                        if (markedInRow == Numbers.GetLength(0))
                        {
                            isWinner = true;
                            break;
                        }
                    }

                    // check columns
                    for (int y = 0; y < Numbers.GetLength(1); y++)
                    {
                        int markedInColumn = 0;

                        for (int x = 0; x < Numbers.GetLength(0); x++)
                        {
                            if (Numbers[x, y].IsMarked)
                                markedInColumn++;
                        }

                        if (markedInColumn == Numbers.GetLength(0))
                        {
                            isWinner = true;
                            break;
                        }
                    }

                    return isWinner;
                }
            }

            /// <summary>
            /// Revert this card's state to simulate from the beginning of the game
            /// </summary>
            public void Reset()
            {
                TurnsToWin = 0;
                WinningRoll = 0;

                foreach (var number in Numbers)
                {
                    number.IsMarked = false;
                }
            }
        }

        /// <summary>
        /// Represents a bingo card number that may be marked
        /// </summary>
        private class BingoNumber
        {
            public int Number { get; set; }
            public bool IsMarked { get; set; }

            /// <summary>
            /// Returns a formatted string whether the number is marked or not
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                if (IsMarked)
                    return $" [{string.Format("{0,2}", Number)}] ";

                else
                    return $"  {string.Format("{0,2}", Number)}  ";
            }
        }
    }
}
