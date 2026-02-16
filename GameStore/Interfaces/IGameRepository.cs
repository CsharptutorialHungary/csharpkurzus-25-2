using GameStore.Models;

namespace GameStore.Interfaces
{
    public interface IGameRepository
    {
        List<Game> LoadGames();
        void SaveGames(List<Game> games);
    }
}
