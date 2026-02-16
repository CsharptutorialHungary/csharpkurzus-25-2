using GameStore.Services;

namespace GameStore.Core
{
    public class DatabaseSeeder
    {
        public void SeedDatabase(GameService _gameService)
        {
            var existingGames = _gameService.GetAllGames();

            if (existingGames.Count == 0)
            {
                Console.WriteLine("Üres adatbázis érzékelve. Kezdeti adatok betöltése...");

                _gameService.AddGame("The Witcher 3: Wild Hunt", "CD Projekt Red", 2015, 8990);
                _gameService.AddGame("Cyberpunk 2077", "CD Projekt Red", 2020, 14990);
                _gameService.AddGame("The Witcher 2", "CD Projekt Red", 2011, 2990);

                _gameService.AddGame("Grand Theft Auto V", "Rockstar Games", 2013, 9990);
                _gameService.AddGame("Red Dead Redemption 2", "Rockstar Games", 2018, 18990);

                _gameService.AddGame("Half-Life 2", "Valve", 2004, 3500);
                _gameService.AddGame("Portal 2", "Valve", 2011, 3500);
                _gameService.AddGame("Counter-Strike 2", "Valve", 2023, 0); 

                _gameService.AddGame("Minecraft", "Mojang", 2011, 8500);
                _gameService.AddGame("Elden Ring", "FromSoftware", 2022, 16990);

                Console.WriteLine("Adatok sikeresen feltöltve!");

                Thread.Sleep(1500);
                Console.Clear();
            }
        }
    }
}
