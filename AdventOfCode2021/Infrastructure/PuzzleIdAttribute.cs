namespace AdventOfCode2021.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PuzzleIdAttribute : Attribute
    {
        public int PuzzleId { get; set; }

        public PuzzleIdAttribute(int puzzleId)
        {
            PuzzleId = puzzleId;
        }
    }
}
