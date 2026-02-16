using GameStore.Interfaces;
using GameStore.Models;
using System.Text.Json;

namespace GameStore.Services
{
    public class JsonService : IGameRepository
    {
        private readonly string _filePath;
         
        public JsonService(string fileName)
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, fileName);
        }

        public List<Game> LoadGames()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new List<Game>();
                }

                string json = File.ReadAllText(_filePath);

                if (string.IsNullOrWhiteSpace(json))
                    return new List<Game>();

                return JsonSerializer.Deserialize<List<Game>>(json) ?? new List<Game>();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Fájlbeolvasási hiba: {ex.Message}");
                return new List<Game>();
            }
        }

        public void SaveGames(List<Game> gamesToSave)
        {
            try
            {
                string json = JsonSerializer.Serialize(gamesToSave, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mentési hiba: {ex.Message}");
            }
        }
    }
}