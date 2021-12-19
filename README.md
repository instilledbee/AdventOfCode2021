# AdventOfCode2021
Advent of Code 2021 solutions - written in C#/.NET. The aim of this exercise is not to have terse or performant solutions, but rather be an educational repository for others to pick up tips on the logic. 

## Running the application
* Build the console application. As this is a .NET 6 app, compiling can be done in Visual Studio 2022. Alternatively, one can download the [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) and build from the command line.
* The application can be run from the command line with the following parameters: `<puzzle number> [<--verbse>]` e.g.: `AdventOfCode2021.exe 1 --verbose`
  * The first argument, `<puzzle number>` is required and is used to specify which solution to run.
  * The second argument, `--verbose` is optional. Specifying this argument will show more detailed console output for the running solution. Omit the parameter so that the application only outputs the final answer.

## Solution Structure
The aim of the solution's structure is to abstract away most of the infrastrctural/cross-cutting concerns so one can easily view the solutions. The project structure is as follows:

* `Solutions` - where the puzzle logic is contained. Solutions are implemented as individual classes per "day", and each solution exposes methods to solve the first and second parts of each day.
* `Infrastructure` - "glue" code that helps keep the application organized
* `Extensions` - helper methods reused across multiple solutions
