using GameShop.Facades;
using GameShop.StorageInMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopTests
{
    [TestClass]
    public class UserFacadeTests
    {
        private UserInMemoryStorage _userInMemoryStorage;
        private UserFacade _userFacade;

        [TestInitialize]
        public void UserFacadeTestsInitialize()
        {
            _userInMemoryStorage = new UserInMemoryStorage();
            _userFacade = new UserFacade(_userInMemoryStorage);
        }

        [TestMethod]
        [DataRow("Altair")]
        [DataRow("Ezio")]
        public void ShouldCreateUser(string name)
        {
            //act
            var userId = _userFacade.CreateUser(name);

            //validation

            var expectedCount = 1;
            var expectedName = name;
            var expectedUserId = userId;
            var actualUsers = _userInMemoryStorage.GetAll();

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
            var userId = _userFacade.CreateUser(name);

            //act
            _userFacade.DeleteUser(userId);

            //validation
            var expectedUserCount = 0;
            var users = _userInMemoryStorage.GetAll();
            Assert.AreEqual(expectedUserCount, users.Count);
        }

        [TestMethod]
        public void AddUserWithEmptyName_ThrowExeption()
        {
            //prepare
            var name = string.Empty;

            //act
            var user = Assert.ThrowsException<ArgumentException>(() => _userFacade.CreateUser(name));

            //validation
            string expectedException = "The string can't be left empty, null or consist of only whitespaces.";
            Assert.AreEqual(user.Message, expectedException);
        }

        [TestMethod]
        public void AddUserWithSpaceName_ThrowExeption()
        {
            //prepare
            var name = string.Empty;

            //act
            var user = Assert.ThrowsException<ArgumentException>(() => _userFacade.CreateUser(name));

            //validation
            string expectedException = "The string can't be left empty, null or consist of only whitespaces.";
            Assert.AreEqual(user.Message, expectedException);
        }

    }
}
