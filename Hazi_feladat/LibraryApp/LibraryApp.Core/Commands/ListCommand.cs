using LibraryApp.Core;
using LibraryApp.Interfaces;
using LibraryApp.Models;

namespace LibraryApp.Commands;

[Command(Symbol = "list")]
public class ListCommand : ICommand
{
    private readonly List<Book> _books;
    private readonly ILoggerService _logger;

    public ListCommand(List<Book> books, ILoggerService logger)
    {
        _books = books;
        _logger = logger;
    }

    public string Name => "list";
    public string Description => "Könyvek listázása.";

    public void Execute()
    {
        if (_books.Count == 0)
        {
            //Console.WriteLine("Nincs könyv.");
            _logger.Error("A könyvtárad üres.");
            return;
        }

        foreach (var b in _books)
            Console.WriteLine($"[{b.Id}] {b.Title} - {b.Author} ({b.Year}, {b.Genre})");
    }
}
