using LibraryApp.Core;
using LibraryApp.Interfaces;
using LibraryApp.Models;

namespace LibraryApp.Commands;

//Az id-k nem frissülnek, meg kell még csinálni
[Command(Symbol = "delete")]
public class DeleteCommand : ICommand
{
    private readonly List<Book> _books;
    private readonly ILoggerService _logger;

    public DeleteCommand(List<Book> books, ILoggerService logger)
    {
        _books = books;
        _logger = logger;
    }

    public string Name => "delete";
    public string Description => "Könyv törlése ID vagy cím alapján.";

    public void Execute()
    {
        if (_books.Count == 0)
        {
            Console.WriteLine("Nincs könyv.");
            return;
        }

        Console.Write("Adj meg ID-t vagy címet: ");
        string? input = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(input))
        {
            _logger.Error("Üres bemenet.");
            return;
        }

        if (int.TryParse(input, out int id))
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                _logger.Error("Nincs ilyen ID.");
                return;
            }

            _books.Remove(book);
            _logger.Info($"Törölve: {book.Title}");
            return;
        }

        var matches = _books.Where(b => b.Title.ToLower().Contains(input.ToLower())).ToList();

        if (!matches.Any())
        {
            Console.WriteLine("Nincs találat.");
            return;
        }

        if (matches.Count == 1)
        {
            _books.Remove(matches[0]);
            _logger.Info($"Törölve: {matches[0].Title}");
            return;
        }

        Console.WriteLine("Találatok:");
        for (int i = 0; i < matches.Count; i++)
            Console.WriteLine($"{i+1}. [{matches[i].Id}] {matches[i].Title}");

        Console.Write("Sorszám: ");
        if (int.TryParse(Console.ReadLine(), out int index) &&
            index > 0 && index <= matches.Count)
        {
            var book = matches[index - 1];
            _books.Remove(book);
            _logger.Info($"Törölve: {book.Title}");
        }
        else
            _logger.Error("Érvénytelen sorszám.");
    }
}
