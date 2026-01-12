using LibraryApp.Core;
using LibraryApp.Interfaces;
using LibraryApp.Models;

namespace LibraryApp.Commands;

[Command(Symbol = "add")]
public class AddCommand : ICommand
{
    private readonly List<Book> _books;
    private readonly ILoggerService _logger;

    public AddCommand(List<Book> books, ILoggerService logger)
    {
        _books = books;
        _logger = logger;
    }

    public string Name => "add";
    public string Description => "Új könyv hozzáadása.";

    public void Execute()
    {
        try
        {
            Console.Write("Cím: ");
            string? title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
                title = "Ismeretlen cím";

            Console.Write("Szerző: ");
            string? author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author))
                author = "Ismeretlen szerző";


            Console.Write("Év: ");
            int year = int.TryParse(Console.ReadLine(), out int y) ? y : 0;

            Console.Write("Műfaj: ");
            string? genre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(genre))
                genre = "Ismeretlen műfaj";

            int newId = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;

            var book = new Book(newId, title, author, year, genre);
            _books.Add(book);

            _logger.Info($"Könyv hozzáadva (ID: {book.Id})");
        }
        catch (Exception ex)
        {
            _logger.Error("Hiba: " + ex.Message);
        }
    }
}
