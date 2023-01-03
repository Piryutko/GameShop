using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopLibrary
{
    public class UserFacade
    {
        public UserFacade(UserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        private UserStorage _userStorage;

        public Guid CreateUser(string name)
        {
            return _userStorage.AddUser(name);
        }

        public void DeleteUser(Guid id)
        {
            _userStorage.RemoveUser(id);
        }
    }
}
