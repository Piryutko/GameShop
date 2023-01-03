using System;
using System.Collections.Generic;
using GameShopLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameShopLibraryTests
{
    [TestClass]
    public class PurchaseStorageTests
    {
        [TestMethod]
        [DataRow("Noah","Fallout",764,Genre.RPG)]
        [DataRow("Jake", "BulletStorm", 300, Genre.Shooter)]
        public void ShouldAddPurchase(string name, string gameName, double cost, Genre genre)
        {
            //prepare
            var purchaseStorage = new PurchaseStorage();
            var gameStorage = new GameStorage();
            var userStorage = new UserStorage();

            var userId = userStorage.AddUser(name);
            var gameId = gameStorage.AddGame(gameName, cost, genre);

            var expectedResult = new List<Purchase>() { new Purchase(userId, gameId) };

            //act
            purchaseStorage.Add(userId, gameId);
            var actualGames = purchaseStorage.GetAllGames(userId);

            //validation
            Assert.AreEqual(expectedResult.Count, actualGames.Count);

        }

        [TestMethod]
        [DataRow("Emma", "Witcher3", 546, Genre.RPG)]
        [DataRow("John", "Borderlands", 300, Genre.RPG)]
        public void ShouldRemovePurchase(string name,string gameName, double cost, Genre genre)
        {
            //prepare
            var purchaseStorage = new PurchaseStorage();
            var gameStorage = new GameStorage();
            var userStorage = new UserStorage();

            var userId = userStorage.AddUser(name);
            var gameId = gameStorage.AddGame(gameName, cost, genre);
            purchaseStorage.Add(userId, gameId);

            var expectedResult = 0;

            //act
            purchaseStorage.Remove(gameId);
            var actualGames = purchaseStorage.GetAllGames(userId);

            //validation
            Assert.AreEqual(expectedResult, actualGames.Count);
        }

        [TestMethod]
        [DataRow("Ava", "Gothic", 200, Genre.RPG)]
        [DataRow("Eva", "Diablo", 500, Genre.RPG)]
        public void ShouldGetUserItemId(string name, string gameName, double cost, Genre genre)
        {
            //prepare
            var purchaseStorage = new PurchaseStorage();
            var gameStorage = new GameStorage();
            var userStorage = new UserStorage();

            var userId = userStorage.AddUser(name);
            var gameId = gameStorage.AddGame(gameName, cost, genre);
            purchaseStorage.Add(userId, gameId);
            
            var expectedResult = new List<Guid> { gameId };

            //act
            var actualGames = purchaseStorage.GetAllGames(userId);

            //validation
            Assert.AreEqual(expectedResult.Count, actualGames.Count);

            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], actualGames[i]);
            }
        }
    }
}
