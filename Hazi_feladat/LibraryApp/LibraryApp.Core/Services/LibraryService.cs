// LibraryService.cs – könyvek betöltése/mentése
using System.Collections.Generic;
using LibraryApp.Interfaces;
using LibraryApp.Models;

namespace LibraryApp.Services;

public class LibraryService : ILibraryService
{
    private readonly IFileService _fileService;
    private readonly ILoggerService _logger;

    public LibraryService(IFileService fileService, ILoggerService logger)
    {
        _fileService = fileService;
        _logger = logger;
    }

    public List<Book> LoadBooks(string path)
    {
        try
        {
            return _fileService.Load<List<Book>>(path) ?? new List<Book>();
        }
        catch
        {
            _logger.Error("Hiba a könyvek betöltésekor.");
            return new List<Book>();
        }
    }

    public void SaveBooks(List<Book> books, string path)
    {
        try
        {
            _fileService.Save(books, path);
            _logger.Info("Könyvek elmentve.");
        }
        catch
        {
            _logger.Error("Mentési hiba.");
        }
    }
}
