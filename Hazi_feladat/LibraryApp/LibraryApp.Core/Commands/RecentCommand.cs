using LibraryApp.Core;
using LibraryApp.Interfaces;
using LibraryApp.Models;

namespace LibraryApp.Commands;

[Command(Symbol = "recent")]
public class RecentCommand : ICommand
{
    private readonly List<Book> _books;
    private readonly ILoggerService _logger;

    public RecentCommand(List<Book> books, ILoggerService logger)
    {
        _books = books;
        _logger = logger;
    }

    public string Name => "recent";
    public string Description => "Könyvek listázása megadott kezdő évtől.";

    public void Execute()
    {
        if (_books.Count == 0)
        {
            _logger.Error("Nincs könyv.");
            return;
        }

        int startYear;

        // kezdő év bekérése hibakezeléssel
        while (true)
        {
            Console.Write("Add meg a kezdő évet: ");
            if (int.TryParse(Console.ReadLine(), out startYear))
                break;

            Console.ForegroundColor = ConsoleColor.Red;
            _logger.Error("Hibás év! Kérlek adj meg egy számot.");
            Console.ResetColor();
        }

        // szűrés
        var result = _books
            .Where(b => b.Year >= startYear)
            .OrderByDescending(b => b.Year)
            .ToList();

        if (!result.Any())
        {
            _logger.Error("Nincs könyv a megadott évtől kezdve.");
            return;
        }

        Console.WriteLine($"\nKönyvek {startYear}-től:");

        foreach (var b in result)
            Console.WriteLine($"[{b.Id}] {b.Title} - {b.Author} ({b.Year}, {b.Genre})");
    }
}
