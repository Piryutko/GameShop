using System;
using System.Collections.Generic;

namespace GameShopLibrary
{
    public class GameFacade
    {
        public GameFacade(PurchaseStorage purchaseStorage, GameStorage gameStorage, UserStorage userStorage)
        {
            _purchaseStorage = purchaseStorage;

            _gameStorage = gameStorage;

            _userStorage = userStorage;
        }

        private UserStorage _userStorage;

        private PurchaseStorage _purchaseStorage;

        private GameStorage _gameStorage;

        public Guid AddGame(string name, double cost, Genre genre)
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
            var user = _userStorage.GetUser(userId);

            _purchaseStorage.Add(user, game);
            _gameStorage.SellGame(game);
        }
    }
}
