// ILoggerService.cs – loggolás
namespace LibraryApp.Interfaces;

public interface ILoggerService
{
    void Info(string msg);
    void Error(string msg);
}
