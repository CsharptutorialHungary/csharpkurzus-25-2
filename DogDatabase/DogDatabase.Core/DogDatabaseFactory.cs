using DogDatabase.Core.Interfaces;
using DogDatabase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogDatabase.Core
{
    public class DogDatabaseFactory
    {

        public static DogMenu CreateMenu()
        {
            IReadFromConsole reader = new ReadFromConsole();

            IRepository<DogRecord> dogRepository = new DogRepository();
            var dogService = new DogService(reader, dogRepository);
            var dogSearch = new DogSearch(reader, dogRepository);

            IRepository<CompetitionRecord> competitionRepository= new CompetitionRepository();
            var competitionService = new CompetitionService(reader, competitionRepository);
            var competitionSearch = new CompetitionSearch(reader, competitionRepository);

            return new DogMenu(
                dogService,
                dogSearch,
                reader,
                competitionService,
                competitionSearch
            );
        }
    }
}
