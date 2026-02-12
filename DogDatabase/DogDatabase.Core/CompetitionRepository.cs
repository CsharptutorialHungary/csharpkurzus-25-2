using DogDatabase.Core.Interfaces;
using DogDatabase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DogDatabase.Core
{
    public class CompetitionRepository : IRepository<CompetitionRecord>
    {
        private string _filepath = "competitions.json";
        public List<CompetitionRecord> LoadAll()
        {
            if (!File.Exists(_filepath))
            {
                return new List<CompetitionRecord>();
            }

            try
            {
                string json = File.ReadAllText(_filepath);
                return JsonSerializer.Deserialize<List<CompetitionRecord>>(json) ?? new List<CompetitionRecord>();
            }
            catch
            {
                throw new Exception("Error while loading!");
            }
        }

        public void SaveAll(List<CompetitionRecord> items)
        {
            try
            {
                string newJson = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filepath, newJson);
                Console.WriteLine("Successfully saved.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
            catch
            {
                throw new Exception("Error while saving!");
            }

        }
        public void Add(CompetitionRecord item)
        {
            if (CheckIfExists(item))
            {
                return;
            }
            List<CompetitionRecord> competitions = LoadAll();
            competitions.Add(item);
            SaveAll(competitions);
        }

        public bool CheckIfExists(CompetitionRecord item)
        {
            List<CompetitionRecord> competitions = LoadAll();
            if (competitions.Contains(item))
            {
                Console.WriteLine("This competition result already exists in the database!");
                return true;
            }
            return false;
        }
    }
}
