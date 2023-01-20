using GameShop.Domains;
using GameShop.StorageInMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopTests
{
    [TestClass]
    public class PurchaseStorageTests
    {

        private PurchaseInMemoryStorage _purchaseInMemoryStorage;
        private GameInMemoryStorage _gameInMemoryStorage;
        private UserInMemoryStorage _userInMemoryStorage;

        [TestInitialize]
        public void PurchaseStorageInizialize()
        {
            _purchaseInMemoryStorage = new PurchaseInMemoryStorage();
            _gameInMemoryStorage = new GameInMemoryStorage();
            _userInMemoryStorage = new UserInMemoryStorage();
        }

        [TestMethod]
        [DataRow("Noah", "Fallout", 764, Genre.RPG)]
        [DataRow("Jake", "BulletStorm", 300, Genre.Shooter)]
        public void ShouldAddPurchase(string userName, string gameName, int cost, Genre genre)
        {
            //prepare
            var userId = _userInMemoryStorage.Add(userName);
            var gameId = _gameInMemoryStorage.AddGame(gameName, cost, genre);

            //act
            _purchaseInMemoryStorage.Add(userId, gameId);
            var actualGames = _purchaseInMemoryStorage.GetAllGames(userId);

            //validation
            var expectedResult = new List<Purchase>() { new Purchase(userId, gameId) };
            Assert.AreEqual(expectedResult.Count, actualGames.Count);
        }

        [TestMethod]
        [DataRow("Emma", "Witcher3", 546, Genre.RPG)]
        [DataRow("John", "Borderlands", 300, Genre.RPG)]
        public void ShouldRemovePurchase(string name, string gameName, int cost, Genre genre)
        {
            //prepare
            var userId = _userInMemoryStorage.Add(name);
            var gameId = _gameInMemoryStorage.AddGame(gameName, cost, genre);
            _purchaseInMemoryStorage.Add(userId, gameId);

            var expectedResult = 0;

            //act
            _purchaseInMemoryStorage.Remove(gameId);
            var actualGames = _purchaseInMemoryStorage.GetAllGames(userId);

            //validation
            Assert.AreEqual(expectedResult, actualGames.Count);
        }

        [TestMethod]
        [DataRow("Ava", "Gothic", 200, Genre.RPG)]
        [DataRow("Eva", "Diablo", 500, Genre.RPG)]
        public void ShouldGetUserItemId(string name, string gameName, int cost, Genre genre)
        {
            //prepare
            var userId = _userInMemoryStorage.Add(name);
            var gameId = _gameInMemoryStorage.AddGame(gameName, cost, genre);
            _purchaseInMemoryStorage.Add(userId, gameId);

            var expectedResult = new List<Guid> { gameId };

            //act
            var actualGames = _purchaseInMemoryStorage.GetAllGames(userId);

            //validation
            Assert.AreEqual(expectedResult.Count, actualGames.Count);

            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], actualGames[i]);
            }
        }

    }
}
