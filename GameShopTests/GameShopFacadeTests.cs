using GameShop.Domains;
using GameShop.Facades;
using GameShop.StorageInMemory;
using GameShop.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopTests
{

    [TestClass]
    public class GameShopFacadeTests
    {
        private PurchaseInMemoryStorage _purchaseInMemoryStorage;
        private GameInMemoryStorage _gameInMemoryStorage;
        private UserInMemoryStorage _userInMemoryStorage;
        private GameShopFacade _gameShopFacade;

        [TestInitialize]
        public void ShopFacadeTestsInitialize()
        {
            _purchaseInMemoryStorage = new PurchaseInMemoryStorage();
            _gameInMemoryStorage = new GameInMemoryStorage();
            _userInMemoryStorage = new UserInMemoryStorage();
            _gameShopFacade = new GameShopFacade(_gameInMemoryStorage, _purchaseInMemoryStorage, _userInMemoryStorage);
        }

        [TestMethod]
        [DataRow("Fallout", 764, Genre.RPG)]
        [DataRow("BulletStorm", 300, Genre.Shooter)]
        public void ShouldAddGame(string name, int cost, Genre genre)
        {
            //act
            _gameShopFacade.AddGame(name, cost, genre);

            //validation 
            var expectedGameCount = 1;
            var expectedIsBought = false;
            var gameCount = _gameInMemoryStorage.GetAllGames();

            Assert.AreEqual(expectedGameCount, gameCount.Count);

            for (int i = 0; i < gameCount.Count; i++)
            {
                Assert.AreEqual(gameCount[i].Name, name);
                Assert.AreEqual(gameCount[i].Cost, cost);
                Assert.AreEqual(gameCount[i].Genre, genre);
                Assert.AreEqual(gameCount[i].IsBought, expectedIsBought);
            }
        }

        [TestMethod]
        [DataRow("Linage2", 500, Genre.RPG, "WorldOfWarcraft", 690, Genre.RPG)]
        [DataRow("MassEffect", 500, Genre.RPG, "NFS", 350, Genre.Race)]
        public void ShouldGetAllGames(string firstName, int firstCost, Genre firstGenre, string secondName, int secondCost, Genre secondGenre)
        {
            //prepare
            var firstGame = _gameShopFacade.AddGame(firstName, firstCost, firstGenre);
            var secondGame = _gameShopFacade.AddGame(secondName, secondCost, secondGenre);

            //act
            var games = _gameShopFacade.GetAllGames();

            //validation 
            var expectedGameCount = 2;

            Assert.AreEqual(expectedGameCount, games.Count);

            Assert.AreEqual(games[0].Name, firstName);
            Assert.AreEqual(games[0].Cost, firstCost);
            Assert.AreEqual(games[0].Id, firstGame);
            Assert.AreEqual(games[0].Genre, firstGenre);

            Assert.AreEqual(games[1].Name, secondName);
            Assert.AreEqual(games[1].Cost, secondCost);
            Assert.AreEqual(games[1].Id, secondGame);
            Assert.AreEqual(games[1].Genre, secondGenre);
        }

        [TestMethod]
        [DataRow("Wolfenstein", 500, Genre.Shooter)]
        [DataRow("MassEffect", 400, Genre.RPG)]
        public void ShouldRemoveGame(string name, int cost, Genre genre)
        {
            //prepare
            var gameId = _gameShopFacade.AddGame(name, cost, genre);

            //act
            _gameShopFacade.RemoveGame(gameId);

            //validation
            var expectedGameCount = 0;
            var games = _gameShopFacade.GetAllGames();

            Assert.AreEqual(expectedGameCount, games.Count);
        }

        [TestMethod]
        [DataRow("Wolfenstein", 500, Genre.Shooter, "Jim")]
        [DataRow("Linage2", 500, Genre.RPG, "Jack")]
        public void ShouldBuyGame(string name, int cost, Genre genre, string userName)
        {
            //prepare
            var userId = _userInMemoryStorage.Add(userName);
            var gameId = _gameShopFacade.AddGame(name, cost, genre);

            //act
            _gameShopFacade.BuyGame(userId, gameId);

            //validation 
            var expectedPurchase = true;
            var expectedGamesCount = 1;
            var expectedAvailableGamesCount = 0;
            var allGames = _gameInMemoryStorage.GetAllGames();
            var allAvailableGames = _gameInMemoryStorage.GetAllAvailableGames();

            Assert.AreEqual(expectedGamesCount, allGames.Count);
            Assert.AreEqual(expectedAvailableGamesCount, allAvailableGames.Count);
            Assert.AreEqual(allGames[0].IsBought, expectedPurchase);
        }

        [TestMethod]
        public void BuyingUnknownGame_ThrowExeption()
        {
            //preapre
            var user = _userInMemoryStorage.Add("John");
            var fakeGameId = Guid.NewGuid();
            var gameFacade = new GameShopFacade(_gameInMemoryStorage, _purchaseInMemoryStorage, _userInMemoryStorage);

            //act
            var buyingGame = Assert.ThrowsException<InvalidOperationException>(() => gameFacade.BuyGame(user, fakeGameId));

            //validation
            var expectedExeption = "Sequence contains no matching element";
            Assert.AreEqual(expectedExeption, buyingGame.Message);
        }

        [TestMethod]
        public void UnknowUserBuys_ThrowExeption()
        {
            //preapre
            var gameId = _gameInMemoryStorage.AddGame("GodOfWar", 2000, Genre.RPG);
            var user = Guid.NewGuid();
            var gameFacade = new GameShopFacade(_gameInMemoryStorage, new PurchaseStorage(), new UserStorage());

            //act
            var buyingGame = Assert.ThrowsException<InvalidOperationException>(() => gameFacade.BuyGame(user, gameId));

            //validation
            var expectedExeption = "Sequence contains no elements";

            Assert.AreEqual(buyingGame.Message, expectedExeption);
        }

        [TestMethod]
        public void AddGameWithEmptyName_ThrowExeption()
        {
            //prepare
            var name = string.Empty;
            var cost = 100;
            var genre = Genre.Race;

            //act
            var game = Assert.ThrowsException<ArgumentException>(() => _gameInMemoryStorage.AddGame(name, cost, genre));

            //validation
            string expectedException = "The string can't be left empty, null or consist of only whitespaces.";
            Assert.AreEqual(game.Message, expectedException);
        }

        [TestMethod]
        public void AddGameWithSpaceName_ThrowExeption()
        {
            //prepare
            var name = " ";
            var cost = 100;
            var genre = Genre.Race;

            //act
            var game = Assert.ThrowsException<ArgumentException>(() => _gameInMemoryStorage.AddGame(name, cost, genre));

            //validation 
            string expectedException = "The string can't be left empty, null or consist of only whitespaces.";
            Assert.AreEqual(game.Message, expectedException);
        }

        [TestMethod]
        public void AddGameWithNegativeCost_ThrowExeption()
        {
            //prepare
            var name = "Doom";
            var cost = -10;
            var genre = Genre.Shooter;

            //act
            var game = Assert.ThrowsException<ArgumentOutOfRangeException>(() => _gameShopFacade.AddGame(name, cost, genre));

            //validation 
            string expectedException = "Value '-10' is not greater than limit '0'.\r\nActual value was -10.";
            Assert.AreEqual(expectedException, game.Message);
        }

    }
}
