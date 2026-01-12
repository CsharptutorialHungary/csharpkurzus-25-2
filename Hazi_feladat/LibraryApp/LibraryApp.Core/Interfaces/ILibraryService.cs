// ILibraryService.cs – könyvadat kezelés
using System.Collections.Generic;
using LibraryApp.Models;

namespace LibraryApp.Interfaces;

public interface ILibraryService
{
    List<Book> LoadBooks(string path);
    void SaveBooks(List<Book> books, string path);
}
