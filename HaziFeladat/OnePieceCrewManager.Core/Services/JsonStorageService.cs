using System.Text.Json;

namespace OnePieceCrewManager.Core.Services
{
    public class JsonStorageService
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true
        };

        public JsonStorageService(string path)
        {
            _path = path;
        }

        public List<CrewMember> Load()
        {
            try
            {
                if (!File.Exists(_path))
                {
                    Console.WriteLine($"Figyelem: A fájl nem létezik: {_path}. Üres lista visszaadva.");
                    return new List<CrewMember>();
                }

                var json = File.ReadAllText(_path);
                var members = JsonSerializer.Deserialize<List<CrewMember>>(json, _options) ?? new List<CrewMember>();
                return members;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Hiba: Sérült vagy érvénytelen JSON fájl: {_path}. {ex.Message}");
                return new List<CrewMember>();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O hiba történt a fájl olvasása közben: {_path}. {ex.Message}");
                return new List<CrewMember>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Váratlan hiba történt betöltés közben: {ex.Message}");
                return new List<CrewMember>();
            }
        }

        public bool Save(List<CrewMember> members)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
                var json = JsonSerializer.Serialize(members, _options);
                File.WriteAllText(_path, json);
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O hiba történt mentés közben: {_path}. {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Váratlan hiba történt mentés közben: {ex.Message}");
                return false;
            }
        }
    }
}
