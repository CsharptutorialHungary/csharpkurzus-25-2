using LibraryApp.Core;
using LibraryApp.Interfaces;
using LibraryApp.Models;

namespace LibraryApp.Commands;

[Command(Symbol = "search")]
public class SearchCommand : ICommand
{
    private readonly List<Book> _books;
    private readonly ILoggerService _logger;

    public SearchCommand(List<Book> books, ILoggerService logger)
    {
        _books = books;
        _logger = logger;
    }

    public string Name => "search";
    public string Description => "Keresés különböző attribútumok alapján.";

    public void Execute()
    {
        if (_books.Count == 0)
        {
            Console.WriteLine("Nincs adat.");
            return;
        }

        var valid = new List<string> { "title", "author", "year", "genre" };
        string field;

        while (true)
        {
            Console.Write("Attribútum (title, author, year, genre): ");
            field = Console.ReadLine()?.Trim().ToLower() ?? "";

            if (valid.Contains(field))
                break;

            _logger.Error("Érvénytelen attribútum!");


        }

        Console.Write("Érték: ");
        string q = Console.ReadLine()?.Trim().ToLower() ?? "";

        IEnumerable<Book> result = field switch
        {
            "title" => _books.Where(b => b.Title.ToLower().Contains(q)),
            "author" => _books.Where(b => b.Author.ToLower().Contains(q)),
            "year" => _books.Where(b => b.Year.ToString().Equals(q)),
            "genre" => _books.Where(b => b.Genre.ToLower().Contains(q)),
            _ => Enumerable.Empty<Book>()
        };

        if (!result.Any())
        {
            Console.WriteLine("Nincs találat.");
            return;
        }

        foreach (var b in result)
            Console.WriteLine($"[{b.Id}] {b.Title} - {b.Author}");
    }
}
