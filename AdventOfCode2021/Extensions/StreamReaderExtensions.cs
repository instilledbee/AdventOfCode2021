namespace AdventOfCode2021.Extensions
{
    public static class StreamReaderExtensions
    {
        public static void ResetPosition(this StreamReader reader)
        {
            reader.BaseStream.Position = 0;
        }
    }
}
