using GameStore.Services;

namespace GameStore.UI
{
    public class StatisticsUI
    {
        private readonly StatisticsService _statisticsService;

        public StatisticsUI(StatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public void Run()
        {
            bool statsMenuRuning = true;

            while (statsMenuRuning)
            {

                Console.WriteLine("\n--- Statisztika Menü ---");
                Console.ResetColor();
                Console.WriteLine("1. Game Store-ban lévő játékok száma");
                Console.WriteLine("2. Legolcsóbb játék");
                Console.WriteLine("3. Játékok átlagára");
                Console.WriteLine("4. Fejlesztői ranglista");
                Console.WriteLine("0. Vissza a Főmenübe");

                string? input = Console.ReadLine() ?? "";


                switch (input)
                {
                    case "1": DisplayTotalGameCount(); break;
                    case "2": DisplayCheapestGame(); break;
                    case "3": DisplayAvaragePrice(); break;
                    case "4": DisplayDevelopersByGameCount(); break;

                    case "0": statsMenuRuning = false; break;
                    default: Console.WriteLine("Érvénytelen választás"); break;
                }
            }
        }

        private void DisplayCheapestGame()
        {
            var game = _statisticsService.GetCheapestGame();
            if (game != null)
            {
                Console.WriteLine($"\nA legolcsóbb játék: {game.Title} ({game.Developer}, {game.Year}, {game.Price})");
            }
            else
            {
                Console.WriteLine("Üres a game store");
            }
        }

        private void DisplayTotalGameCount()
        {
            int gameCount = _statisticsService.GetTotalGameCount();
            Console.WriteLine($"Az összes játék a store-ban: {gameCount}");
        }

        private void DisplayAvaragePrice()
        {
            double avgPrice = _statisticsService.GetAvaragePrice();
            Console.WriteLine($"Az összes játék átlagára: {avgPrice:N2}");
        }

        private void DisplayDevelopersByGameCount()
        {
            var stats = _statisticsService.GetDevelopersByGameCount();

            Console.WriteLine($"{"Fejlesztő",-20} {"Darabszám"}");

            foreach (var item in stats)
            {
                Console.WriteLine($"{item.Developer, -20} {item.Count} db");
            }

        }
    }
}
