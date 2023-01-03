using System;
using System.Collections.Generic;
using GameShopLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameShopLibraryTests
{
    [TestClass]
    public class GameStorageTests
    {
        [TestMethod]
        [DataRow("WorldOfWarcraft", 690, Genre.RPG)]
        [DataRow("Linage2", 500, Genre.RPG)]
        public void ShouldAddGameInGameStorage(string name, double cost, Genre genre)
        {
            //prepare
            var gameStorage = new GameStorage();
            var game = new Game(name, cost, genre);
            var expectedGames = new List<Game>() { game };

            //act
            gameStorage.AddGame(name, cost, genre);
            var actualGames = gameStorage.GetAllGames();

            //validation
            Assert.AreEqual(expectedGames.Count, actualGames.Count);

            for (int counter = 0; counter < expectedGames.Count; counter++)
            {
                Assert.AreEqual(expectedGames[counter].Cost, actualGames[counter].Cost);
                Assert.AreEqual(expectedGames[counter].Name, actualGames[counter].Name);
                Assert.AreEqual(expectedGames[counter].Genre, actualGames[counter].Genre);
            }
        }

        [TestMethod]
        [DataRow("MassEffect", 500, Genre.RPG)]
        [DataRow("NFS", 350, Genre.Race)]
        public void ShouldRemoveGame(string name, double cost, Genre genre)
        {
            //prepare
            var gameStorage = new GameStorage();
            var gameId = gameStorage.AddGame(name, cost, genre);

            var expectedResult = 0;

            //act
            gameStorage.RemoveGame(gameId);
            var actualStorageGames = gameStorage.GetAllGames();

            //validation
            Assert.AreEqual(actualStorageGames.Count, expectedResult);
        }

        [TestMethod]
        [DataRow("MaxPayne", 400, Genre.Shooter)]
        [DataRow("Halo", 500, Genre.Shooter)]
        public void ShouldSellGame(string name, double cost, Genre genre)
        {
            //prepare
            var gameStorage = new GameStorage();
            var gameId = gameStorage.AddGame(name, cost, genre);
            var expectedResult = true;
            var soldGame = true;

            //act
            gameStorage.SellGame(gameId);
            foreach (var item in gameStorage.GetAllAvailableGames())
            {
                if (item.Id == gameId)
                {
                    soldGame = false;
                }
            }

            //validation
            Assert.AreEqual(expectedResult, soldGame);
        }

        [TestMethod]
        [DataRow("DragonAge", 470, Genre.RPG)]
        [DataRow("ElderScrolls", 700, Genre.RPG)]
        public void ShouldGetAllAvailableGames(string name, double cost, Genre genre)
        {
            //prepare
            var gameStorage = new GameStorage();
            gameStorage.AddGame(name, cost, genre);

            var gameIdToSell = gameStorage.AddGame(name, cost, genre);
            gameStorage.SellGame(gameIdToSell);

            var expectedGame = new Game(name, cost, genre);
            var expectedAllGames = new List<Game> { expectedGame };

            //act
            var availableGames = gameStorage.GetAllAvailableGames();

            //validation
            Assert.AreEqual(availableGames.Count, expectedAllGames.Count);
            for (int i = 0; i < availableGames.Count; i++)
            {
                Assert.AreEqual(availableGames[i].Name, expectedAllGames[i].Name);
                Assert.AreEqual(availableGames[i].Cost, expectedAllGames[i].Cost);
                Assert.AreEqual(availableGames[i].Genre, expectedAllGames[i].Genre);
                Assert.AreEqual(availableGames[i].IsBought, expectedAllGames[i].IsBought);
            }
        }

        [TestMethod]
        [DataRow("CallOfDuty", 600, Genre.Shooter)]
        [DataRow("Battlefiled", 600, Genre.Shooter)]
        public void ShouldGetAllGames(string name, double cost, Genre genre)
        {
            //prepare
            var gameStorage = new GameStorage();
            var game = new Game(name, cost, genre);
            gameStorage.AddGame(name, cost, genre);

            var expectedAllGame = new List<Game> { game };

            //act
            var allGames = gameStorage.GetAllAvailableGames();

            //validation
            for (int i = 0; i < allGames.Count; i++)
            {
                Assert.AreEqual(allGames[i].Name, expectedAllGame[i].Name);
                Assert.AreEqual(allGames[i].Cost, expectedAllGame[i].Cost);
                Assert.AreEqual(allGames[i].Genre, expectedAllGame[i].Genre);
                Assert.AreEqual(allGames[i].IsBought, expectedAllGame[i].IsBought);
            }
        }

        [TestMethod]
        public void AddGameWithEmptyName_ThrowExeption()
        {
            //prepare
            var gameStorage = new GameStorage();
            var name = string.Empty;
            var cost = 100;
            var genre = Genre.Race;

            //act
            var result = Assert.ThrowsException<ArgumentException>(() => gameStorage.AddGame(name, cost, genre));

            //validation
            var expectedExeption = "The string can't be left empty, null or consist of only whitespaces.";
            Assert.AreEqual(expectedExeption, result.Message);

        }

        [TestMethod]
        public void AddGameWithSpaceName_ThrowExeption()
        {
            //prepare
            var gameStorage = new GameStorage();
            var name = " ";
            var cost = 100;
            var genre = Genre.Race;

            //act
            var result = Assert.ThrowsException<ArgumentException>(() => gameStorage.AddGame(name, cost, genre));

            //validation
            var expectedExeption = "The string can't be left empty, null or consist of only whitespaces.";
            Assert.AreEqual(expectedExeption, result.Message);

        }

        [TestMethod]
        public void RemoveUnknownGame_ThrowExeption()
        {
            //prepare
            var gameStorage = new GameStorage();
            gameStorage.AddGame("Doom", 1500, Genre.Shooter);
            var fakeGameId = Guid.NewGuid();

            //act
            var removeGame = Assert.ThrowsException<InvalidOperationException>(() => gameStorage.RemoveGame(fakeGameId));

            //validation
            var expectedExtension = "Последовательность не содержит соответствующий элемент";
            Assert.AreEqual(expectedExtension, removeGame.Message);
        }

        [TestMethod]
        public void SellUnknowGame_ExpectedZeroGames()
        {
            //prepare
            var gameStorage = new GameStorage();
            var gameId = Guid.NewGuid();

            //act
            var removeGame = Assert.ThrowsException<InvalidOperationException>(() => gameStorage.SellGame(gameId));

            //validation
            var expectedExtension = "Последовательность не содержит соответствующий элемент";
            Assert.AreEqual(removeGame.Message, expectedExtension);
        }

    }
}
