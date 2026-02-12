using DogDatabase.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogDatabase.Core
{
    public class DogMenu
    {
        private readonly IDogService _dogService;
        private readonly IDogSearch _dogSearch;
        private readonly IReadFromConsole _reader;
        private readonly ICompetitionService _competitionService;
        private readonly ICompetitionSearch _competitionSearch;
        public DogMenu(IDogService dogService, IDogSearch dogSearch, IReadFromConsole reader, ICompetitionService competitionService, ICompetitionSearch competitionSearch)
        {
            _dogService = dogService;
            _dogSearch = dogSearch;
            _reader = reader;
            _competitionService = competitionService;
            _competitionSearch = competitionSearch;
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Silken Windhound Database");
                Console.WriteLine("1. Add new dog");
                Console.WriteLine("2. Add new competition result");
                Console.WriteLine("3. Search in dog database");
                Console.WriteLine("4. Search in competition database");
                Console.WriteLine("5. Update dog data");
                Console.WriteLine("0. Quit");
                Console.WriteLine("Choose: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        _dogService.CreateDog();
                        break;
                    case "2":
                        Console.Clear();
                        _competitionService.CreateCompetitionResult();
                        break;
                    case "3":
                        ShowDogSearchMenu();
                        break;
                    case "4":
                        ShowCompetitionSearchMenu();
                        break;
                    case "5":
                        ShowUpdateMenu();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;

                }
            }
        }

        public void ShowDogSearchMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Searching dogs...");
                Console.WriteLine("1. By birth year");
                Console.WriteLine("2. By owner");
                Console.WriteLine("3. Search siblings");
                Console.WriteLine("0. Quit");
                Console.WriteLine("Choose: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Give the birth year:");
                        int year = _reader.ReadInt();
                        _dogSearch.SearchByYear(year);
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Give the owner:");
                        string owner = _reader.ReadNonEmptyString();
                        _dogSearch.SearchByOwner(owner);
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Give the name of the dog:");
                        string name = _reader.ReadNonEmptyString();
                        _dogSearch.SearchDogSiblings(name);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;

                }
            }
        }

        public void ShowCompetitionSearchMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Searching competitions...");
                Console.WriteLine("1. By dog's name");
                Console.WriteLine("2. By year");
                Console.WriteLine("3. Most competitions won");
                Console.WriteLine("0. Quit");
                Console.WriteLine("Choose: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Give the name of the dog:");
                        string name = _reader.ReadNonEmptyString();
                        _competitionSearch.SearchByDogName(name);
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Give the competition year:");
                        int year = _reader.ReadInt();
                        _competitionSearch.SearchByYear(year);
                        break;
                    case "3":
                        Console.Clear();
                        _competitionSearch.SearchMostWins();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;

                }
            }
        }

        public void ShowUpdateMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Update");
                Console.WriteLine("1. Health status");
                Console.WriteLine("2. Owner");
                Console.WriteLine("0. Quit");
                Console.WriteLine("Choose: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Give the name of the dog: ");
                        string name = _reader.ReadNonEmptyString();
                        _dogService.UpdateDogHealthStatus(name);
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Give the name of the dog: ");
                        string owner = _reader.ReadNonEmptyString();
                        _dogService.UpdateDogOwner(owner);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;

                }
            }
        }
    }

}

