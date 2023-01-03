using System;
using GameShopLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameShopLibraryTests
{
    [TestClass]
    public class UserFacadeTests
    {
        [TestMethod]
        [DataRow("Altair")]
        [DataRow("Ezio")]
        public void ShouldCreateUser(string name)
        {
            //prepare
            var userStorage = new UserStorage();
            var userFacade = new UserFacade(userStorage);

            //act
            var userId = userFacade.CreateUser(name);

            //validation

            var expectedCount = 1;
            var expectedName = name;
            var expectedUserId = userId;
            var actualUsers = userStorage.GetAllUsers();

            Assert.AreEqual(expectedName, actualUsers[0].Name);
            Assert.AreEqual(expectedUserId, actualUsers[0].Id);
            Assert.AreEqual(expectedCount, actualUsers.Count);
        }

        [TestMethod]
        [DataRow("Connor")]
        [DataRow("Edward")]
        public void ShouldDeleteUser(string name)
        {
            //prepare
            var userStorage = new UserStorage();
            var userFacade = new UserFacade(userStorage);
            var userId = userFacade.CreateUser(name);
            var userCount = userStorage.GetAllUsers();

            //act
            userFacade.DeleteUser(userId);

            //validation
            var expectedUserCount = 0;
            var users = userStorage.GetAllUsers();
            Assert.AreEqual(expectedUserCount, users.Count);
        }

        [TestMethod]
        public void AddUserWithEmptyName_ThrowExeption()
        {
            //prepare
            var userStorage = new UserStorage();
            var userFacade = new UserFacade(userStorage);
            var name = string.Empty;

            //act
            var user = Assert.ThrowsException<ArgumentException>(() => userFacade.CreateUser(name));

            //validation
            string expectedException = "The string can't be left empty, null or consist of only whitespaces.";
            Assert.AreEqual(user.Message, expectedException);
        }

        [TestMethod]
        public void AddUserWithSpaceName_ThrowExeption()
        {
            //prepare
            var userStorage = new UserStorage();
            var userFacade = new UserFacade(userStorage);
            var name = " ";

            //act
            var user = Assert.ThrowsException<ArgumentException>(() => userFacade.CreateUser(name));

            //validation
            string expectedException = "The string can't be left empty, null or consist of only whitespaces.";
            Assert.AreEqual(user.Message, expectedException);
        }

    }
}
