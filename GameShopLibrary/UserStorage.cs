using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopLibrary
{
    public class UserStorage
    {
        public UserStorage()
        {
            _users = new List<User>();
        }
        
        private List<User> _users;

        public Guid AddUser(string name)
        {
            var user = new User(name);

            _users.Add(user);

            return user.Id;
        }

        public void RemoveUser(Guid id)
        {
            var user = _users.Single(u => u.Id == id);
            _users.Remove(user);
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public Guid GetUser(Guid userId)
        {
            var user = _users.Single(u => u.Id == userId);
            return user.Id;
        }
    }
}
