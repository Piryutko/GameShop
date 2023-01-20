using GameShop.Domains;

namespace GameShop.Storages
{
    public class GameStorage : IGameStorage
    {
        public GameStorage()
        {
            _gameShop = new GameShopContext();
        }

        private readonly GameShopContext _gameShop;

        public Guid AddGame(string name, decimal cost, Genre genre)
        {
            var game = new Game(name, cost, genre);
            _gameShop.Games.Add(game);
            _gameShop.SaveChanges();
            return game.Id;
        }

        public void RemoveGame(Guid id)
        {
            var game = _gameShop.Games.Single(g => g.Id == id);
            _gameShop.Games.Remove(game);
            _gameShop.SaveChanges();
        }

        public void SellGame(Guid id)
        {
            var game = _gameShop.Games.Single(x => x.Id == id);
            game.Sell();
            _gameShop.Games.Update(game);
            _gameShop.SaveChanges();
        }

        public List<Game> GetAllAvailableGames()
        {
            var games = _gameShop.Games.Where(g => g.IsBought == false).ToList();
            return games;
        }

        public List<Game> GetAllGames()
        {
            return _gameShop.Games.ToList();
        }

        public Guid GetGame(Guid gameId)
        {
            var game = _gameShop.Games.Single(g => g.Id == gameId && g.IsBought == false);
            return game.Id;
        }

    }
}
