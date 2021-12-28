namespace AdventOfCode2021.Infrastructure.Logging
{
    internal class ConsoleLogger : ILogger
    {
        public void Write(string message = "") => Console.Write(message);

        public void WriteLine(string message = "") => Console.WriteLine(message);

        public void Clear() => Console.Clear();
    }
}
