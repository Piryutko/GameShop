using EnsureThat;
using GameShop.Domains;
using GameShop.Storages;

namespace GameShop.StorageInMemory
{
    public class GameInMemoryStorage : IGameStorage
    {
        public GameInMemoryStorage()
        {
            _games = new List<Game>();
        }

        private List<Game> _games;

        public Guid AddGame(string name, decimal cost, Genre genre)
        {
            Ensure.That(name).IsNotNullOrWhiteSpace();
            Ensure.That(cost).IsGt(0);

            var game = new Game(name, cost, genre);
            _games.Add(game);
            return game.Id;
        }

        public List<Game> GetAllAvailableGames()
        {
            var games = _games.Where(g => g.IsBought == false).ToList();

            return games;
        }

        public List<Game> GetAllGames()
        {
            return _games;
        }

        public Guid GetGame(Guid id)
        {
            var game = _games.Single(g => g.Id == id && g.IsBought == false);
            return game.Id;
        }

        public void RemoveGame(Guid id)
        {
            var game = _games.Single(g => g.Id == id);
            _games.Remove(game);
        }

        public void SellGame(Guid id)
        {
            var game = _games.Single(x => x.Id == id);
            game.Sell();
        }
    }
}
