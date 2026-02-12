using DogDatabase.Core.Interfaces;
using DogDatabase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogDatabase.Core
{
    internal class DogSearch: IDogSearch
    {
        private IReadFromConsole _reader;
        private IRepository<DogRecord> _dogRepository;
        public DogSearch(IReadFromConsole reader, IRepository<DogRecord> dogRepository)
        {
            _reader = reader;
            _dogRepository = dogRepository;
        }

        public void SearchByYear(int year)
        {
            var dogs = _dogRepository.LoadAll().Where(dog => dog.Birthyear == year).OrderBy(dog => dog.Name).ToList();
            DisplayResults(dogs);
        }

        public void SearchByOwner(string owner)
        {
            var dogs = _dogRepository.LoadAll().Where(dog => dog.Owner == owner).OrderBy(dog => dog.Name).ToList();
            DisplayResults(dogs);
        }

        public void SearchDogSiblings(string name)
        {
            var dog = _dogRepository.LoadAll().FirstOrDefault(d => d.Name == name);
            
            if (dog == null)
            {
                DisplayResults(new List<DogRecord>());
                return;
            }

            var siblings = _dogRepository.LoadAll().Where(sibling => sibling.Mother == dog.Mother && sibling.Father == dog.Father).OrderBy(sibling => sibling.Name).ToList();
            DisplayResults(siblings);
        }

        public void DisplayResults(List<DogRecord> dogs)
        {
            Console.Clear();
            if (dogs.Count == 0) {
                Console.WriteLine("No dog found.");
            } else
            {
                Console.WriteLine("Results:");
                foreach (var dog in dogs)
                {
                    Console.WriteLine($"{dog.Name} ({dog.Gender}), Year of Birth: {dog.Birthyear}, Owner: {dog.Owner}");
                }
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
