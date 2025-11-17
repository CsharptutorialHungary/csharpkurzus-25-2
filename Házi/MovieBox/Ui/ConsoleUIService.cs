using MovieBox.Core.Filter;
using MovieBox.Core.Records;
using System.Globalization;

namespace MovieBox.Ui
{
    /// <summary>
    /// Concrete implementation of IConsoleUIService that uses the System.Console.
    /// </summary>
    public class ConsoleUIService : IConsoleUIService
    {
        public void Clear() => Console.Clear();

        public string GetMainMenuChoice()
        {
            Console.WriteLine("--- MovieBox Main Menu ---");
            Console.WriteLine("1. List All Movies");
            Console.WriteLine("2. Add a New Movie");
            Console.WriteLine("3. Search for Movies");
            Console.WriteLine("4. Show Collection Statistics");
            Console.WriteLine("5. Delete a Movie");
            Console.WriteLine("--------------------------");
            Console.WriteLine("6. Exit");
            Console.Write("\nPlease select an option: ");
            return Console.ReadLine() ?? "";
        }

        public Movie GetNewMovieData()
        {
            ShowMessage("--- Add a New Movie ---", MessageType.Title);

            // Use validation helpers to get input
            string title = ReadString("Title: ", isRequired: true)!;
            string director = ReadString("Director: ", isRequired: true)!;
            int year = ReadInt("Release Year (1888-2100): ", 1888, DateTime.Now.Year + 10);
            double rating = ReadDouble("Rating (0.0-10.0): ", 0.0, 10.0);

            return new Movie(title, director, year, rating);
        }

        public MovieFilterCriteria GetSearchCriteria()
        {
            ShowMessage("--- Search for Movies ---", MessageType.Title);
            Console.WriteLine("(Press ENTER to skip any filter)");

            // Use nullable helpers to build criteria
            return new MovieFilterCriteria
            {
                Title = ReadString("Title contains: "),
                DirectorContains = ReadString("Director contains: "),
                Year = ReadInt("Exact Year: "),
                RatingMin = ReadDouble("Minimum Rating: "),
                RatingMax = ReadDouble("Maximum Rating: "),
                ResultCount = ReadInt("Limit results (e.g., 5): ")
            };
        }

        public void DisplayMovies(IEnumerable<Movie> movies)
        {
            if (!movies.Any())
            {
                Console.WriteLine("No movies found.");
                return;
            }

            int titleWidth = movies.Max(m => m.Title.Length) + 2;
            int directorWidth = movies.Max(m => m.Director.Length) + 2;
            const int yearWidth = 8;
            const int ratingWidth = 8;

            Console.WriteLine($"| {"Title".PadRight(titleWidth)} | {"Director".PadRight(directorWidth)} | {"Year".PadRight(yearWidth)} | {"Rating".PadRight(ratingWidth)} |");
            Console.WriteLine($"| {new string('-', titleWidth)} | {new string('-', directorWidth)} | {new string('-', yearWidth)} | {new string('-', ratingWidth)} |");

            foreach (var movie in movies)
            {
                Console.WriteLine($"| {movie.Title.PadRight(titleWidth)} | {movie.Director.PadRight(directorWidth)} | {movie.ReleaseYear.ToString().PadRight(yearWidth)} | {movie.Rating.ToString("F1").PadRight(ratingWidth)} |");
            }
        }

        public void DisplayStats(MovieCollectionStats stats)
        {
            ShowMessage("--- Movie Collection Stats ---", MessageType.Title);
            Console.WriteLine($"- Total Movies:     {stats.TotalCount}");
            Console.WriteLine($"- Average Rating:   {stats.AverageRating:F2}");
            Console.WriteLine($"- Highest Rated:    {(stats.HighestRatedMovie?.Title ?? "N/A")} ({(stats.HighestRatedMovie?.Rating.ToString("F1") ?? "N/A")})");
            Console.WriteLine("- Movies per Decade:");
            foreach (var decade in stats.MoviesPerDecade.OrderBy(kv => kv.Key))
            {
                Console.WriteLine($"  - {decade.Key}s: {decade.Value} movie(s)");
            }
        }

        public int GetMovieSelectionForDeletion(IEnumerable<Movie> movies)
        {
            var moviesList = movies.ToList();

            if (!moviesList.Any())
            {
                ShowMessage("No movies available to delete.", MessageType.Error);
                return -1;
            }

            ShowMessage("--- Delete a Movie ---", MessageType.Title);
            Console.WriteLine("Select a movie to delete:\n");

            for (int i = 0; i < moviesList.Count; i++)
            {
                var movie = moviesList[i];
                Console.WriteLine($"{i + 1}. {movie.Title} ({movie.ReleaseYear}) - {movie.Director}");
            }

            Console.WriteLine($"{moviesList.Count + 1}. Cancel");
            Console.Write("\nEnter the number of the movie to delete (or cancel): ");

            while (true)
            {
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int selection))
                {
                    ShowMessage("Invalid input. Please enter a valid number.", MessageType.Error);
                    Console.Write("Try again: ");
                    continue;
                }

                if (selection == moviesList.Count + 1)
                {
                    ShowMessage("Deletion cancelled.", MessageType.Info);
                    return -1;
                }

                if (selection < 1 || selection > moviesList.Count)
                {
                    ShowMessage($"Please select a number between 1 and {moviesList.Count + 1}.", MessageType.Error);
                    Console.Write("Try again: ");
                    continue;
                }

                return selection - 1;
            }
        }

        public void ShowMessage(string message, MessageType type = MessageType.Info)
        {
            switch (type)
            {
                case MessageType.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case MessageType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case MessageType.Title:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
            }

            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void WaitForUser()
        {
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }

        private string? ReadString(string prompt, bool isRequired = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    if (isRequired)
                    {
                        ShowMessage("This field is required. Please try again.", MessageType.Error);
                        continue;
                    }
                    return null;
                }
                return input;
            }
        }

        private int? ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                if (int.TryParse(input, CultureInfo.InvariantCulture, out int result))
                {
                    return result;
                }

                ShowMessage("Invalid number. Please enter a whole number (e.g., 5).", MessageType.Error);
            }
        }

        private int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                int? result = ReadInt(prompt);
                if (result.HasValue)
                {
                    if (result.Value >= min && result.Value <= max)
                    {
                        return result.Value;
                    }
                    ShowMessage($"Invalid range. Please enter a number between {min} and {max}.", MessageType.Error);
                }
                else
                {
                    ShowMessage("This field is required.", MessageType.Error);
                }
            }
        }

        private double? ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                if (double.TryParse(input, CultureInfo.InvariantCulture, out double result))
                {
                    return result;
                }

                ShowMessage("Invalid number. Please enter a number (e.g., 8.8).", MessageType.Error);
            }
        }

        private double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                double? result = ReadDouble(prompt);

                if (result.HasValue)
                {
                    if (result.Value >= min && result.Value <= max)
                    {
                        return result.Value;
                    }
                    ShowMessage($"Invalid range. Please enter a number between {min} and {max}.", MessageType.Error);
                }
                else
                {
                    ShowMessage("This field is required.", MessageType.Error);
                }
            }
        }
    }
}