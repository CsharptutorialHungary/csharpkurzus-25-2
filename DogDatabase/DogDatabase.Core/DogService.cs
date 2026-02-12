using DogDatabase.Core.Interfaces;
using DogDatabase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DogDatabase.Core
{
    internal class DogService: IDogService
    {
        private IReadFromConsole _reader;
        private IRepository<DogRecord> _dogRepository;

        public DogService(IReadFromConsole reader, IRepository<DogRecord> dogRepository)
        {
            _reader = reader;
            _dogRepository = dogRepository;
        }

        public void CreateDog()
        {
            Console.WriteLine("Adding new dog: ");

            Console.Write("Name: ");
            string name = _reader.ReadNonEmptyString();

            Console.Write("Gender (male/female): ");
            string gender = _reader.ReadNonEmptyString();

            while (!_reader.CheckIfGenderIsCorrect(gender))
            {
                Console.Write("Gender (male/female): ");
                gender = _reader.ReadNonEmptyString();
            }

            Console.Write("Health status (healthy/ill/deceased): ");
            string healthStatus = _reader.ReadNonEmptyString();

            while (!_reader.CheckIfHealthStatusIsCorrect(healthStatus))
            {
                Console.Write("Health status (healthy/ill/deceased): ");
                healthStatus = _reader.ReadNonEmptyString();
            }

            Console.Write("Year of birth: ");
            int birthyear = _reader.ReadInt();

            Console.Write("Owner: ");
            string owner = _reader.ReadNonEmptyString();

            Console.Write("Dam: ");
            string mother = _reader.ReadOptionalString();

            Console.Write("Sire: ");
            string father = _reader.ReadOptionalString();

            DogRecord newDog = new(name, gender, healthStatus, birthyear, owner, mother, father);

            _dogRepository.Add(newDog);
        }

        public void UpdateDogHealthStatus(string name)
        {
            List<DogRecord> dogs = _dogRepository.LoadAll();
            DogRecord? dog = dogs.FirstOrDefault(d => d.Name == name);

            if (dog == null || dogs.Count == 0)
            {
                Console.WriteLine("Dog not found.");
                return;
            }

            Console.Write($"Current health status: {dog.HealthStatus}\n");
            Console.Write("New health status (healthy/ill/deceased): ");
            string newStatus = _reader.ReadNonEmptyString();

            while (!_reader.CheckIfHealthStatusIsCorrect(newStatus))
            {
                Console.Write("New health status (healthy/ill/deceased): ");
                newStatus = _reader.ReadNonEmptyString();
            }

            DogRecord updatedDog = dog with { HealthStatus = newStatus };
            List<DogRecord> updatedList = dogs.Select(d => d.Name == name ? updatedDog : d).ToList();

            _dogRepository.SaveAll(updatedList);
        }

        public void UpdateDogOwner(string name)
        {
            List<DogRecord> dogs = _dogRepository.LoadAll();
            DogRecord? dog = dogs.FirstOrDefault(d => d.Name == name);

            if (dog == null || dogs.Count == 0)
            {
                Console.WriteLine("Dog not found.");
                return;
            }

            Console.Write($"Current owner: {dog.Owner}\n");
            Console.Write("New owner: ");
            string newOwner = _reader.ReadNonEmptyString();

            DogRecord updatedDog = dog with { Owner = newOwner };
            List<DogRecord> updatedList = dogs.Select(d => d.Name == name ? updatedDog : d).ToList();

            _dogRepository.SaveAll(updatedList);
        }
    }
}
