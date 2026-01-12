// JsonFileService.cs – JSON szerializáció
using System.IO;
using System.Text.Json;
using LibraryApp.Interfaces;

namespace LibraryApp.Services;

public class JsonFileService : IFileService
{
    public void Save<T>(T data, string filePath)
    {
        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    public T? Load<T>(string filePath)
    {
        if (!File.Exists(filePath))
            return default;

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(json);
    }
}
