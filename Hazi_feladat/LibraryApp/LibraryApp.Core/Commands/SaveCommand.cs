using LibraryApp.Core;
using LibraryApp.Interfaces;
using LibraryApp.Models;

namespace LibraryApp.Commands;

public class SaveCommand : ICommand
{
    private readonly ILibraryService _lib;
    private readonly List<Book> _books;
    private readonly string _path;

    public SaveCommand(ILibraryService lib, List<Book> books, string path)
    {
        _lib = lib;
        _books = books;
        _path = path;
    }

    public string Name => "save";
    public string Description => "Adatok mentése.";

    public void Execute()
    {
        _lib.SaveBooks(_books, _path);
        Console.WriteLine("Mentve.");
    }
}
