using DogDatabase.Core.Interfaces;
using DogDatabase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogDatabase.Core
{
    internal class CompetitionSearch: ICompetitionSearch
    {
        private IReadFromConsole _reader;
        private IRepository<CompetitionRecord> _competitionRepository;

        public CompetitionSearch(IReadFromConsole reader, IRepository<CompetitionRecord> competitionRepository)
        {
            _reader = reader;
            _competitionRepository = competitionRepository;
        }

        public void SearchByDogName(string name)
        {
            var competitions = _competitionRepository.LoadAll().Where(comp => comp.DogName == name).OrderBy(comp => comp.Year).ToList();
            DisplayResults(competitions);
        }

        public void SearchByYear(int year)
        {
            var competitions = _competitionRepository.LoadAll().Where(comp => comp.Year == year).OrderBy(comp => comp.CompetitionName).ToList();
            DisplayResults(competitions);
        }

        public void SearchMostWins()
        {
            var competitions = _competitionRepository.LoadAll().Where(comp => comp.Placement == 1).GroupBy(comp => comp.DogName).Select(dog => new { DogName = dog.Key, Count = dog.Count() }).ToList();
            if (competitions.Count == 0)
            {
                Console.WriteLine("No first place records found.");
            } else
            {
                int maxCount = competitions.Max(dog => dog.Count);
                var winners = competitions.Where(dog => dog.Count == maxCount).ToList();

                Console.WriteLine("Dog with the most first place finishes:");
                foreach (var w in winners)
                {
                    Console.WriteLine($"{w.DogName} — {w.Count} first place finishes");
                }
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        public void DisplayResults(List<CompetitionRecord> competitions)
        {
            Console.Clear();
            if (competitions.Count == 0)
            {
                Console.WriteLine("No competition found.");
            }
            else
            {
                Console.WriteLine("Results:");
                foreach (var comp in competitions)
                {
                    Console.WriteLine($"{comp.CompetitionName} ({comp.Year}), Dog: {comp.DogName}, Placement: {comp.Placement}");
                }
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
