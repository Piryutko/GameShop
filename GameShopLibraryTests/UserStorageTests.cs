using System;
using System.Collections.Generic;
using GameShopLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameShopLibraryTests
{
    [TestClass]
    public class UserStorageTests
    {
        [TestMethod]
        [DataRow("Oliver")]
        [DataRow("Jack")]
        public void ShouldAddUser(string name)
        {
            //prepare
            var userStorage = new UserStorage();

            var expectedUsers = new List<User> { new User(name) };

            //act
            userStorage.AddUser(name);

            //validation
            var actualUsers = userStorage.GetAllUsers();

            Assert.AreEqual(actualUsers.Count, expectedUsers.Count);
            for (int i = 0; i < actualUsers.Count; i++)
            {
                Assert.AreEqual(expectedUsers[i].Name, actualUsers[i].Name);
            }
        }

        [TestMethod]
        [DataRow("Liam")]
        [DataRow("Mike")]
        public void ShouldRemoveUser(string name)
        {
            //prepare
            var usersStorage = new UserStorage();
            var userId = usersStorage.AddUser(name);

            var expectedUsersCount = 0;

            //act
            usersStorage.RemoveUser(userId);
            var actualUsers = usersStorage.GetAllUsers();

            //validation
            Assert.AreEqual(expectedUsersCount, actualUsers.Count);
        }

        [TestMethod]
        [DataRow("Olivia")]
        [DataRow("Jane")]
        public void ShouldGetAllUsers(string name)
        {
            //prepare
            var userStorage = new UserStorage();
            userStorage.AddUser(name);

            var expectedUsers = new List<User> { new User(name) };

            //act
            var actualUsers = userStorage.GetAllUsers();

            //validation
            Assert.AreEqual(expectedUsers.Count, actualUsers.Count);
            for (int i = 0; i < actualUsers.Count; i++)
            {
                Assert.AreEqual(expectedUsers[i].Name, actualUsers[i].Name);
            }
        }

        [TestMethod]
        public void AddUserWithEmptyName_ThrowExeption()
        {
            //prepare
            var userStorage = new UserStorage();
            var name = string.Empty;

            //act
            var user = Assert.ThrowsException<ArgumentException>(() => userStorage.AddUser(name));

            //validation
            var expectedExeption = "The string can't be left empty, null or consist of only whitespaces.";

            Assert.AreEqual(expectedExeption, user.Message);
        }

        [TestMethod]
        public void AddUserWithSpaceName_ThrowExeption()
        {
            //prepare
            var userStorage = new UserStorage();
            var name = " ";

            //act
            var user = Assert.ThrowsException<ArgumentException>(() => userStorage.AddUser(name));

            //validation
            var expectedExeption = "The string can't be left empty, null or consist of only whitespaces.";

            Assert.AreEqual(expectedExeption, user.Message);
        }

        [TestMethod]
        public void RemoveUnknownUser_ThrowExeption()
        {
            //prepare
            var userStorage = new UserStorage();
            var userId = Guid.NewGuid();

            //act
            var user = Assert.ThrowsException<InvalidOperationException>(() => userStorage.RemoveUser(userId));

            //validation
            var expectedExeption = "Последовательность не содержит соответствующий элемент";
            Assert.AreEqual(expectedExeption, user.Message);
        }
    }
}
