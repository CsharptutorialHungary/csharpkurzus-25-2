using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using DogDatabase.Core.Interfaces;
using DogDatabase.Core.Models;

namespace DogDatabase.Core
{
    internal class DogRepository : IRepository<DogRecord>
    {
        private string _filepath = "dogs.json";

        public List<DogRecord> LoadAll()
        {
            if (!File.Exists(_filepath)) {
                return new List<DogRecord>();
            }

            try
            {
                string json = File.ReadAllText(_filepath);
                return JsonSerializer.Deserialize<List<DogRecord>>(json) ?? new List<DogRecord>();
            }
            catch
            {
                throw new Exception("Error while loading!");
            }
        }

        public void SaveAll(List<DogRecord> items)
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
        public void Add(DogRecord item)
        {
            if (CheckIfExists(item))
            {
                return;
            }
            List<DogRecord> dogs = LoadAll();
            dogs.Add(item);
            SaveAll(dogs);
        }

        public bool CheckIfExists(DogRecord item)
        {
            List<DogRecord> dogs = LoadAll();
            if (dogs.Contains(item))
            {
                Console.WriteLine("This dog already exists in the database!");
                return true;
            }
            return false;
        }
    }
}
