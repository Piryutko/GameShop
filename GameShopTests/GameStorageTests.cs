using GameShop.Domains;
using GameShop.StorageInMemory;

namespace GameShopTests
{
    [TestClass]
    public class GameStorageTests
    {
        private GameInMemoryStorage _gameInMemoryStorage;

        [TestInitialize]
        public void GameStorageInitialize()
        {
            _gameInMemoryStorage = new GameInMemoryStorage();
        }

        [TestMethod]
        [DataRow("WorldOfWarcraft", 1000, Genre.RPG)]
        [DataRow("Linage2", 500, Genre.RPG)]
        public void ShouldAddGameInGameStorage(string name, int cost, Genre genre)
        {
            //prepare
            var game = new Game(name, cost, genre);
            var expectedGames = new List<Game>() { game };

            //act
            _gameInMemoryStorage.AddGame(name, cost, genre);
            var actualGames = _gameInMemoryStorage.GetAllGames();

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
        public void ShouldRemoveGame(string name, int cost, Genre genre)
        {
            //prepare
            var gameId = _gameInMemoryStorage.AddGame(name, cost, genre);

            var expectedResult = 0;

            //act
            _gameInMemoryStorage.RemoveGame(gameId);
            var actualStorageGames = _gameInMemoryStorage.GetAllGames();

            //validation
            Assert.AreEqual(actualStorageGames.Count, expectedResult);
        }

        [TestMethod]
        [DataRow("MaxPayne", 400, Genre.Shooter)]
        [DataRow("Halo", 500, Genre.Shooter)]
        public void ShouldSellGame(string name, int cost, Genre genre)
        {
            //prepare
            var gameId = _gameInMemoryStorage.AddGame(name, cost, genre);
            var expectedResult = true;
            var soldGame = true;

            //act
            _gameInMemoryStorage.SellGame(gameId);
            foreach (var item in _gameInMemoryStorage.GetAllAvailableGames())
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
        public void ShouldGetAllAvailableGames(string name, int cost, Genre genre)
        {
            //prepare
            _gameInMemoryStorage.AddGame(name, cost, genre);

            var gameIdToSell = _gameInMemoryStorage.AddGame(name, cost, genre);
            _gameInMemoryStorage.SellGame(gameIdToSell);

            var expectedGame = new Game(name, cost, genre);
            var expectedAllGames = new List<Game> { expectedGame };

            //act
            var availableGames = _gameInMemoryStorage.GetAllAvailableGames();

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
        public void ShouldGetAllGames(string name, int cost, Genre genre)
        {
            //prepare
            var game = new Game(name, cost, genre);
            _gameInMemoryStorage.AddGame(name, cost, genre);

            var expectedAllGame = new List<Game> { game };

            //act
            var allGames = _gameInMemoryStorage.GetAllAvailableGames();

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
            var name = string.Empty;
            var cost = 100;
            var genre = Genre.Race;

            //act
            var result = Assert.ThrowsException<ArgumentException>(() => _gameInMemoryStorage.AddGame(name, cost, genre));

            //validation
            var expectedExeption = "The string can't be left empty, null or consist of only whitespaces.";
            Assert.AreEqual(expectedExeption, result.Message);

        }

        [TestMethod]
        public void AddGameWithSpaceName_ThrowExeption()
        {
            //prepare
            var name = string.Empty;
            var cost = 100;
            var genre = Genre.Race;

            //act
            var result = Assert.ThrowsException<ArgumentException>(() => _gameInMemoryStorage.AddGame(name, cost, genre));

            //validation
            var expectedExeption = "The string can't be left empty, null or consist of only whitespaces.";
            Assert.AreEqual(expectedExeption, result.Message);

        }

        [TestMethod]
        public void RemoveUnknownGame_ThrowExeption()
        {
            //prepare
            _gameInMemoryStorage.AddGame("Doom", 1500, Genre.Shooter);
            var fakeGameId = Guid.NewGuid();

            //act
            var removeGame = Assert.ThrowsException<InvalidOperationException>(() => _gameInMemoryStorage.RemoveGame(fakeGameId));

            //validation
            var expectedExtension = "Sequence contains no matching element";
            Assert.AreEqual(expectedExtension, removeGame.Message);
        }

        [TestMethod]
        public void SellUnknowGame_ExpectedZeroGames()
        {
            //prepare
            var gameId = Guid.NewGuid();

            //act
            var removeGame = Assert.ThrowsException<InvalidOperationException>(() => _gameInMemoryStorage.SellGame(gameId));

            //validation
            var expectedExtension = "Sequence contains no matching element";
            Assert.AreEqual(removeGame.Message, expectedExtension);
        }
    }
}