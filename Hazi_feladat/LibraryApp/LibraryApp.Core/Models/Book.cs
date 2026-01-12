// Book.cs – immutable rekord típus
namespace LibraryApp.Models;

public record Book(int Id, string Title, string Author, int Year, string Genre);
