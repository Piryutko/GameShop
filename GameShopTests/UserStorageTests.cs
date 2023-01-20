using GameShop.StorageInMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopTests
{
    [TestClass]
    public class UserStorageTests
    {
        private UserInMemoryStorage _userInMemoryStorage;

        [TestInitialize]
        public void UserStorageTestsInitialize()
        {
            _userInMemoryStorage= new UserInMemoryStorage();
        }

        [TestMethod]
        [DataRow("Oliver")]
        [DataRow("Jack")]
        public void ShouldAddUser(string name)
        {
            //act
            _userInMemoryStorage.Add(name);

            //validation
            var actualUsers = _userInMemoryStorage.GetAll();
            var expectedUsersCount = 1;
            var exdectedName = name;

            Assert.AreEqual(actualUsers.Count, expectedUsersCount);

            Assert.AreEqual(exdectedName, actualUsers[0].Name);
        }

        [TestMethod]
        [DataRow("Liam")]
        [DataRow("Mike")]
        public void ShouldRemoveUser(string name)
        {
            //prepare
            var userId = _userInMemoryStorage.Add(name);

            var expectedUsersCount = 0;

            //act
            _userInMemoryStorage.Remove(userId);
            var actualUsers = _userInMemoryStorage.GetAll();

            //validation
            Assert.AreEqual(expectedUsersCount, actualUsers.Count);
        }

        [TestMethod]
        [DataRow("Olivia")]
        [DataRow("Jane")]
        public void ShouldGetAllUsers(string name)
        {
            //prepare
            var userStorage = new UserInMemoryStorage();
            userStorage.Add(name);

            //act
            var actualUsers = userStorage.GetAll();

            //validation
            var expectedUsers = 1;
            var expectedName = name;

            Assert.AreEqual(expectedUsers, actualUsers.Count);
            Assert.AreEqual(expectedName, actualUsers[0].Name);
        }

        [TestMethod]
        public void AddUserWithEmptyName_ThrowExeption()
        {
            //prepare
            var name = string.Empty;

            //act
            var user = Assert.ThrowsException<ArgumentException>(() => _userInMemoryStorage.Add(name));

            //validation
            var expectedExeption = "The string can't be left empty, null or consist of only whitespaces.";

            Assert.AreEqual(expectedExeption, user.Message);
        }

        [TestMethod]
        public void AddUserWithSpaceName_ThrowExeption()
        {
            //prepare
            var userStorage = new UserInMemoryStorage();
            var name = string.Empty;

            //act
            var user = Assert.ThrowsException<ArgumentException>(() => userStorage.Add(name));

            //validation
            var expectedExeption = "The string can't be left empty, null or consist of only whitespaces.";

            Assert.AreEqual(expectedExeption, user.Message);
        }

        [TestMethod]
        public void RemoveUnknownUser_ReturnFalse()
        {
            //prepare
            var userId = Guid.NewGuid();

            //act
            var result = _userInMemoryStorage.Remove(userId);

            //validation
            var expectedResult = false;

            Assert.AreEqual(expectedResult, result);
        }
    }
}
