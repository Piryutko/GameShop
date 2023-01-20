using GameShop.Domains;

namespace GameShop.Storages
{
    public class UserStorage : IUserStorage
    {
        public UserStorage()
        {
            _gameShop = new GameShopContext();
        }

        private GameShopContext _gameShop;

        public Guid Add(string name)
        {
            var user = new User(name);
            _gameShop.Users.Add(user);
            _gameShop.SaveChanges();
            return user.Id;
        }

        public bool Remove(Guid id)
        {
            try
            {
                var user = _gameShop.Users.Single(u => u.Id == id);
                _gameShop.Users.Remove(user);
                _gameShop.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<User> GetAll()
        {
            return _gameShop.Users.ToList();
        }

        public Guid Exist(Guid userId)
        {
            var user = _gameShop.Users.Single(u => u.Id == userId);
            return user.Id;
        }

    }
}
