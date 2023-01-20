using GameShop.Domains;
using GameShop.Storages;

namespace GameShop.StorageInMemory
{
    public class UserInMemoryStorage : IUserStorage
    {
        public UserInMemoryStorage()
        {
            _users = new List<User>();
        }

        private List<User> _users;

        public Guid Add(string name)
        {
            var user = new User(name);

            _users.Add(user);

            return user.Id;
        }

        public Guid Exist(Guid userId)
        {
            var user = _users.Single(u => u.Id == userId);
            return user.Id;
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public bool Remove(Guid id)
        {
            try
            {
                var user = _users.Single(u => u.Id == id);
                _users.Remove(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
