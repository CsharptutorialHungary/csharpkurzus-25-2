using GameStore.Models;
using GameStore.Services;

namespace GameStore.Services
{
    public class StatisticsService
    {
        private readonly GameService _gameService;

        public StatisticsService(GameService gameService)
        {
            _gameService = gameService;
        }

        private List<Game> Games => _gameService.GetAllGames();

        public int GetTotalGameCount()
        {
            return Games.Count;
        }

        public double GetAvaragePrice()
        {
            if (!Games.Any())
                return 0;

            return Games.Average(b => b.Price);
        }

        public Game? GetCheapestGame()
        {
            return Games.MinBy(b => b.Price);
        }

        public IEnumerable<dynamic> GetDevelopersByGameCount()
        {
            return _gameService.GetAllGames()
                .GroupBy(b => b.Developer)
                .Select(g => new { Developer = g.Key, Count = g.Count() })
                .OrderByDescending(a => a.Count);

        }
    }
}
