using GameShop.Domains;
using GameShop.Facade;
using GameShop.Storages;

namespace GameShop.Facades
{
    public class GameShopFacade : IGameShopFacade
    {
        public GameShopFacade(IGameStorage gameStorage, IPurchaseStorage purchaseStorage, IUserStorage userStorage)
        {
            _gameStorage = gameStorage;
            _purchaseStorage = purchaseStorage;
            _userStorage = userStorage;
        }

        private readonly IGameStorage _gameStorage;
        private readonly IPurchaseStorage _purchaseStorage;
        private readonly IUserStorage _userStorage;

        public Guid AddGame(string name, decimal cost, Genre genre)
        {
            return _gameStorage.AddGame(name, cost, genre);
        }

        public List<Game> GetAllGames()
        {
            return _gameStorage.GetAllGames();
        }

        public List<Game> GetAllAvailableGames()
        {
            return _gameStorage.GetAllAvailableGames();
        }

        public void RemoveGame(Guid id)
        {
            _gameStorage.RemoveGame(id);
        }

        public void BuyGame(Guid userId, Guid gameId)
        {
            var game = _gameStorage.GetGame(gameId);
            var user = _userStorage.Exist(userId);

            _purchaseStorage.Add(user, game);
            _gameStorage.SellGame(game);
        }

        public Guid AddUser(string name)
        {
           return _userStorage.Add(name);
        }

    }
}
