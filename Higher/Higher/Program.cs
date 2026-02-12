using Higher;
using System.Text.Json;

class Program
{
    static List<PlayerScore> data = new List<PlayerScore>();

    static void Main()
    {
        try { 
            var dataFile = File.ReadAllText("scores.json");
            data = JsonSerializer.Deserialize<List<PlayerScore>>(dataFile);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("The file was not found: " + e.Message);
            return;
        }

        catch (JsonException e)
        {
            Console.WriteLine("The json data file is formated incorrectly " + e.Message);
            return;
        }

        while (true)
        {
            Console.WriteLine("Choose an option:\n1. Get scores of a game\n2. Get sorted list\n3. Print statistics\n4. Group by property\n5. Adding data\n6. Exit");
            string choice = Console.ReadLine();

            switch (choice)
            {
                
                case "1":
                    Console.WriteLine("Enter the game name:");
                    string gameName = Console.ReadLine();
                    getScoresOfAGame(gameName);
                    break;
                case "2":
                    Console.WriteLine("Enter the type of ordering (ascending/descending):");
                    string typeOfOrdering = Console.ReadLine();
                    getSortedList(typeOfOrdering);
                    break;
                case "3":
                    PrintStatistics();
                    break;
                case "4":
                    Console.WriteLine("Enter the property to group by (Name/Score/GameName/Date):");
                    string property = Console.ReadLine();
                    if (Enum.TryParse<Options>(property, out var option))
                    {
                        GroupBy(option);
                    }
                    else
                    {
                        Console.WriteLine("Invalid property");
                    }
                    break;
                case "5":
                //needs to be improved its -dry
                    Console.WriteLine("Enter the name of the player:");
                    string newName = Console.ReadLine();
                    Console.WriteLine("Enter the score:");
                    string newScore = Console.ReadLine();
                    Console.WriteLine("Enter the game:");
                    string newGameName = Console.ReadLine();
                    DateTime newDate = DateTime.Now;
                    data.Add(new PlayerScore(newName, int.Parse(newScore), newGameName, newDate));
                    break;
                case "6":
                    var options = new JsonSerializerOptions();
                    options.WriteIndented = true;
                    string jsonString = JsonSerializer.Serialize(data, options);
                    File.WriteAllText("scores.json", jsonString);

                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }


        getScoresOfAGame("Tetris");
        PrintStatistics();
        GroupBy(Options.Score);
        

        
    }

    static void getScoresOfAGame(string gameName)
    {
        var filteredList = data.Where(x => x.GameName == gameName);
        foreach (PlayerScore p in filteredList)
        {
            p.PrettyPrint();
        }
    }

    static void getSortedList(string typeOfOrdering)
    {
        IEnumerable<PlayerScore> sortedList;
        if (typeOfOrdering == "ascending")
        {
            sortedList = data.OrderBy(x => x.Score);
        }
        else
        {
            sortedList = data.OrderByDescending(x => x.Score);
        }

        foreach (PlayerScore p in sortedList)
        {
            p.PrettyPrint();
        }
    }

    static void PrintStatistics()
    {
        var count = data.Count();
        var maxScore = data.Max(x => x.Score);
        var avgScore = data.Average(x => x.Score);
        Console.WriteLine($"The number of records: {count}\nThe best was: {maxScore}\nThe average score is: {avgScore}\n");
    }

    static void GroupBy(Options property)
    {
        // Itt mi lenne a szebb megoldás?
        IEnumerable<IGrouping<object, PlayerScore>> group = property switch
        {
            Options.Name => data.GroupBy(x => (object)x.Name),
            Options.Score => data.GroupBy(x => (object)x.Score),
            Options.GameName => data.GroupBy(x => (object)x.GameName),
            Options.Date => data.GroupBy(x => (object)x.Date),
            _ => throw new ArgumentException("Invalid property")
        };

        foreach (IGrouping<object, PlayerScore> g in group)
        {
            Console.WriteLine($"Group: {g.Key}");

            foreach (PlayerScore record in g)
            {
                record.PrettyPrint();
            }
        }
    }
}