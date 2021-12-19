namespace AdventOfCode2021.Infrastructure.Logging
{
    internal class NullLogger : ILogger
    {
        public void Write(string message = "")
        {
            return;
        }

        public void WriteLine(string message = "")
        {
            return;
        }
    }
}
