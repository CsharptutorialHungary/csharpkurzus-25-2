// ConsoleLoggerService.cs – színes loggolás
using System;
using LibraryApp.Interfaces;

namespace LibraryApp.Services;

public class ConsoleLoggerService : ILoggerService
{
    public void Info(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[INFO] " + msg);
        Console.ResetColor();
    }

    public void Error(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[ERROR] " + msg);
        Console.ResetColor();
    }
}
