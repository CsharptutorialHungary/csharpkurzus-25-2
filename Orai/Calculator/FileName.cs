using Calculator.HTTP;

namespace Calculator;

internal sealed class ConsoleLogger : ILogger
{
    public void Error(string message)
        => Console.WriteLine($"\e[31m{message}\e[0m");

    public void Info(string message)
        => Console.WriteLine($"\e[32m{message}\e[0m");

    public void Warning(string message)
        => Console.WriteLine($"\e[33m{message}\e[0m");
}
