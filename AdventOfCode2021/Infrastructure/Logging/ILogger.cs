namespace AdventOfCode2021.Infrastructure.Logging
{
    internal interface ILogger
    {
        void Write(string message = "");

        void WriteLine(string message = "");

        void Clear();
    }
}
