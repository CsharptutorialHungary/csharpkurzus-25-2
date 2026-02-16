

using GameStore.Interfaces;
using GameStore.Services;
using GameStore.UI;

namespace GameStore.Core
{
    public class App
    {
        public void Start()
        {
            IGameRepository repo = new JsonService("games.json");
            var gameStore = new GameService(repo);
            var statisticsService = new StatisticsService(gameStore);
            var statisticsUi = new StatisticsUI(statisticsService);
            var mainMenuUi = new MainUI(gameStore, statisticsUi);
            var dataBaseSeeder = new DatabaseSeeder();

            dataBaseSeeder.SeedDatabase(gameStore);
            

            mainMenuUi.Run();
        }
    }
}
