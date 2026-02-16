using GameStore.Interfaces;
using GameStore.Models;

namespace GameStore.Services
{
    public class GameService
    {
        private readonly IGameRepository _repository;
        private List<Game> _games;
        public GameService(IGameRepository repository)
        {
            _repository = repository;
            _games = _repository.LoadGames();
        }

        public List<Game> GetAllGames()
        {
            return _games.OrderBy(b => b.Title).ToList();
        }

        public void AddGame(string title, string developer, int year, double price)
        {
            bool exist = _games
                .Any(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
                b.Developer.Equals(developer, StringComparison.OrdinalIgnoreCase));

            if (exist)
            {
                Console.WriteLine($"Ez a játék már szerepel a katalógusban: {title}");
            }
            else
            {
                var newGame = new Game(title, developer, year, price);
                _games.Add(newGame);
                _repository.SaveGames(_games);
                Console.WriteLine($"Játék sikeresen hozzáadva: {newGame.Title}");

            }
        }

        public void DeleteGame(string title)
        {
            var gameToRemove = _games.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (gameToRemove != null)
            {
                _games.Remove(gameToRemove);
                _repository.SaveGames(_games);
                Console.WriteLine($"A '{gameToRemove.Title}' sikeresen törölve.");
            }
            else
            {
                Console.WriteLine($"Nem található '{title}' című játék.");
            }
        }

        public void UpdateGame(string originalTitle, string newTitle, string newDeveloper, int newYear, double newPrice)
        {
            var index = _games.FindIndex(b => b.Title.Equals(originalTitle, StringComparison.OrdinalIgnoreCase));

            if (index != -1)
            {
                var updatedGame = new Game(newTitle, newDeveloper, newYear, newPrice);

                _games[index] = updatedGame;
                _repository.SaveGames(_games);
                Console.WriteLine("Játék adatai frissítve.");
            }
            else
            {
                Console.WriteLine($"Nem található '{originalTitle}' című játék, így nem lehet módosítani.");
            }
        }
    }
}
