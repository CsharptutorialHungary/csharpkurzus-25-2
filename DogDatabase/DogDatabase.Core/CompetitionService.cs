using DogDatabase.Core.Interfaces;
using DogDatabase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogDatabase.Core
{
    internal class CompetitionService: ICompetitionService
    {
        private IReadFromConsole _reader;
        private IRepository<CompetitionRecord> _competitionRepository;
        public CompetitionService(IReadFromConsole reader, IRepository<CompetitionRecord> competitionRepository) 
        {
            _reader = reader;
            _competitionRepository = competitionRepository;
        }

        public void CreateCompetitionResult()
        {
            Console.WriteLine("Adding new competition result:");

            Console.Write("Dog's name: ");
            string dogName = _reader.ReadNonEmptyString();

            Console.Write("Competition's name: ");
            string compName = _reader.ReadNonEmptyString();

            Console.Write("Year: ");
            int year = _reader.ReadInt();

            Console.Write("Placement: ");
            int placement = _reader.ReadInt();

            CompetitionRecord newComp = new CompetitionRecord(dogName, compName, year, placement);
            _competitionRepository.Add(newComp);
        }
    }
}
