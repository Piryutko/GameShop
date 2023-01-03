using System;
using GameShopLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameShopLibraryTests
{
    [TestClass]
    public class ShopFacadeTests
    {
        [TestMethod]
        [DataRow("Fallout", 764, Genre.RPG)]
        [DataRow("BulletStorm", 300, Genre.Shooter)]
        public void ShouldAddGame(string name, double cost, Genre genre)
        {
            //prepare
            var purchaseStorage = new PurchaseStorage();
            var gameStorage = new GameStorage();
            var userStorage = new UserStorage();
            var shopFacade = new GameFacade(purchaseStorage, gameStorage, userStorage);

            //act
            var gameId = shopFacade.AddGame(name, cost, genre);

            //validation 
            var expectedGameCount = 1;
            var expectedIsBought = false;
            var gameCount = gameStorage.GetAllGames();

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
        [DataRow("MassEffect", 500, Genre.RPG,"NFS", 350, Genre.Race)]
        public void ShouldGetAllGames(string firstName, double firstCost, Genre firstGenre, string secondName, double secondCost, Genre secondGenre)
        {
            //prepare
            var purchaseStorage = new PurchaseStorage();
            var gameStorage = new GameStorage();
            var userStorage = new UserStorage();
            var shopFacade = new GameFacade(purchaseStorage, gameStorage, userStorage);

            var firstGame = shopFacade.AddGame(firstName, firstCost, firstGenre);
            var secondGame = shopFacade.AddGame(secondName, secondCost, secondGenre);

            //act
            var games = shopFacade.GetAllGames();

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
        public void ShouldRemoveGame(string name, double cost, Genre genre)
        {
            //prepare
            var purchaseStorage = new PurchaseStorage();
            var gameStorage = new GameStorage();
            var userStorage = new UserStorage();
            var shopFacade = new GameFacade(purchaseStorage, gameStorage, userStorage);

            var gameId = shopFacade.AddGame(name, cost, genre);

            //act
            shopFacade.RemoveGame(gameId);

            //validation
            var expectedGameCount = 0;
            var games = shopFacade.GetAllGames();

            Assert.AreEqual(expectedGameCount, games.Count);
        }

        [TestMethod]
        [DataRow("Wolfenstein", 500, Genre.Shooter,"Jim")]
        [DataRow("Linage2", 500, Genre.RPG,"Jack")]
        public void ShouldBuyGame(string name, double cost, Genre genre, string userName)
        {
            //prepare
            var purchaseStorage = new PurchaseStorage();
            var gameStorage = new GameStorage();
            var userStorage = new UserStorage();
            var shopFacade = new GameFacade(purchaseStorage, gameStorage, userStorage);

            var userId = userStorage.AddUser(userName);
            var gameId = shopFacade.AddGame(name, cost, genre);

            //act
            shopFacade.BuyGame(userId, gameId);

            //validation 
            var expectedPurchase = true;
            var expectedGamesCount = 1;
            var expectedAvailableGamesCount = 0;
            var allGames = gameStorage.GetAllGames();
            var allAvailableGames = gameStorage.GetAllAvailableGames();

            Assert.AreEqual(expectedGamesCount, allGames.Count);
            Assert.AreEqual(expectedAvailableGamesCount, allAvailableGames.Count);
            Assert.AreEqual(allGames[0].IsBought, expectedPurchase);
        }

        [TestMethod]
        public void BuyingUnknownGame_ThrowExeption()
        {
            //preapre
            var userStorage = new UserStorage();
            var purchaseStorage = new PurchaseStorage();
            var gameStorage = new GameStorage();

            var user = userStorage.AddUser("John");
            var fakeGameId = Guid.NewGuid();
            var gameFacade = new GameFacade(purchaseStorage, gameStorage, userStorage);

            //act
            var buyingGame = Assert.ThrowsException<InvalidOperationException>(() => gameFacade.BuyGame(user, fakeGameId));

            //validation
            var expectedExeption = "Последовательность не содержит соответствующий элемент";
            Assert.AreEqual(expectedExeption, buyingGame.Message);
        }

        [TestMethod]
        public void UnknowUserBuys_ThrowExeption()
        {
            //preapre
            var gameStorage = new GameStorage();
            var gameId = gameStorage.AddGame("GodOfWar", 2000, Genre.RPG);
            var user = Guid.NewGuid();
            var gameFacade = new GameFacade(new PurchaseStorage(), gameStorage, new UserStorage());

            //act
            var buyingGame = Assert.ThrowsException<InvalidOperationException>(() => gameFacade.BuyGame(user, gameId));

            //validation
            var expectedExeption = "Последовательность не содержит соответствующий элемент";

            Assert.AreEqual(buyingGame.Message, expectedExeption);
        }

        [TestMethod]
        public void AddGameWithEmptyName_ThrowExeption()
        {
            //prepare
            var purchaseStorage = new PurchaseStorage();
            var gameStorage = new GameStorage();
            var userStorage = new UserStorage();
            var shopFacade = new GameFacade(purchaseStorage, gameStorage, userStorage);

            var name = string.Empty;
            var cost = 100;
            var genre = Genre.Race;

            //act
            var game = Assert.ThrowsException<ArgumentException>(() => gameStorage.AddGame(name, cost, genre));

            //validation 
            string expectedException = "The string can't be left empty, null or consist of only whitespaces.";
            Assert.AreEqual(game.Message, expectedException);
        }

        [TestMethod]
        public void AddGameWithSpaceName_ThrowExeption()
        {
            //prepare
            var purchaseStorage = new PurchaseStorage();
            var gameStorage = new GameStorage();
            var userStorage = new UserStorage();
            var shopFacade = new GameFacade(purchaseStorage, gameStorage, userStorage);

            var name = " ";
            var cost = 100;
            var genre = Genre.Race;

            //act
            var game = Assert.ThrowsException<ArgumentException>(() => gameStorage.AddGame(name, cost, genre));

            //validation 
            string expectedException = "The string can't be left empty, null or consist of only whitespaces.";
            Assert.AreEqual(game.Message, expectedException);
        }

        [TestMethod]
        public void AddGameWithNegativeCost_ThrowExeption()
        {
            //prepare
            var purchaseStorage = new PurchaseStorage();
            var gameStorage = new GameStorage();
            var userStorage = new UserStorage();
            var shopFacade = new GameFacade(purchaseStorage, gameStorage, userStorage);
            var name = "Doom";
            var cost = -10;
            var genre = Genre.Shooter;

            //act
            var game = Assert.ThrowsException<ArgumentOutOfRangeException>(() => shopFacade.AddGame(name, cost, genre));

            //validation 
            string expectedException = "Value '-10' is not greater than limit '0'.\r\nФактическое значение было -10.";
            Assert.AreEqual(expectedException, game.Message);
        }

    }
}
