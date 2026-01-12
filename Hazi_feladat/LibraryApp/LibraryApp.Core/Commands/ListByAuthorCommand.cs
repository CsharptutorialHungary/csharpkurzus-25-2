using LibraryApp.Core;
using LibraryApp.Interfaces;
using LibraryApp.Models;

namespace LibraryApp.Core.Commands;

[Command(Symbol = "lba")]
internal class ListByAuthorCommand : ICommand
{
    private readonly List<Book> _books;
    private readonly ILoggerService _logger;

    public ListByAuthorCommand(List<Book> books, ILoggerService logger)
    {
        _books = books;
        _logger = logger;
    }

    public string Name => "lba";
    public string Description => "Könyvek listázása szerző szerint.";

    public void Execute() 
    { 
        if (_books.Count == 0)
        {
            _logger.Error("Nincs könyv.");
            return;
        }

        string author;

        Console.Write("Add meg a szerző teljes nevét: ");
        author = Console.ReadLine()?.Trim().ToLower() ?? "";
        
        var result = _books.Where(book => book.Author.ToLower() == author)
                           .OrderByDescending(b => b.Year)
                           .ToList();

        if (!result.Any())
        {
            _logger.Error("Nincs könyved a szerzőtől");
            return;
        }

        Console.WriteLine($"\n{author} könyvei:");

        foreach (var b in result)
            Console.WriteLine($"[{b.Id}] {b.Title} - {b.Author} ({b.Year}, {b.Genre})");
    }
}
