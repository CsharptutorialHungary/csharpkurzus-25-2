using DogDatabase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DogDatabase.Core.Interfaces
{
    public interface ICompetitionSearch
    {

        public void SearchByDogName(string name);

        public void SearchByYear(int year);

        public void SearchMostWins();

        public void DisplayResults(List<CompetitionRecord> competitions);
    }
}
