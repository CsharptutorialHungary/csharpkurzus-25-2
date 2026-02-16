using GameStore.Services;

namespace GameStore.UI
{
    public class MainUI
    {
        private readonly GameService _gameService;
        private readonly StatisticsUI _statistics;

        public MainUI(GameService gameService, StatisticsUI statistics)
        {
            _gameService = gameService;
            _statistics = statistics;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n--- Game Store Menü ---");
                Console.WriteLine("1. Összes játék listázása");
                Console.WriteLine("2. Új játék hozzáadása");
                Console.WriteLine("3. Törlés");
                Console.WriteLine("4. Módosítás");
                Console.WriteLine("5. Statiszitkák");
                Console.WriteLine("0. Kilépés");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1": ListAllGames(); break;
                    case "2": AddNewGame(); break;
                    case "3": DeleteGame(); break;
                    case "4": UpdateGame(); break;
                    case "5": _statistics.Run(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Érvénytelen választás"); break;
                }
            }
        }

        private void ListAllGames()
        {
            var games = _gameService.GetAllGames();
            if (!games.Any())
            {
                Console.WriteLine("Nincsenek játékok a katalógusban");
                return;
            }

            Console.WriteLine("\nÖsszes játék:");

            foreach (var game in games)
            {
                Console.WriteLine($"- {game.Title} ({game.Developer}, {game.Year}, {game.Price})");
            }
        }

        private void AddNewGame()
        {
            try
            {
                Console.Write("Cím: ");
                string title = Console.ReadLine() ?? "";
                Console.Write("Fejlesztő: ");
                string developer = Console.ReadLine() ?? "";
                Console.Write("Kiadási év: ");
                int year = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Ár: ");
                double price = double.Parse(Console.ReadLine() ?? "0");

                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(developer) || year <= 0 || price < 0)
                {
                    Console.WriteLine("Érvénytelen adatok");
                    return;
                }

                _gameService.AddGame(title, developer, year, price);
            }
            catch (FormatException)
            {
                Console.WriteLine("Adatbeviteli hiba");
            }
        }

        private void DeleteGame()
        {
            Console.Write("Törlendő játék címe: ");
            string title = Console.ReadLine() ?? "";
            _gameService.DeleteGame(title);
        }

        private void UpdateGame()
        {
            Console.Write("Módosítandó játék címe: ");
            string originalTitle = Console.ReadLine() ?? "";

            Console.Write("Cím: ");
            string newTitle = Console.ReadLine() ?? "";
            Console.Write("Fejlesztő: ");
            string newDeveloper = Console.ReadLine() ?? "";
            Console.Write("Kiadási év: ");
            int newYear = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Ár: ");
            double newPrice = double.Parse(Console.ReadLine() ?? "0");

            _gameService.UpdateGame(originalTitle, newTitle, newDeveloper, newYear, newPrice);
        }
    }
}
