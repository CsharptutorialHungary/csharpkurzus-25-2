// IFileService.cs – fájl műveletekhez
namespace LibraryApp.Interfaces;

public interface IFileService
{
    void Save<T>(T data, string path);
    T? Load<T>(string path);
}
