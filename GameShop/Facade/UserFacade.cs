using GameShop.Storages;

namespace GameShop.Facades
{
    public class UserFacade : IUserFacade
    {
        public UserFacade(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        private IUserStorage _userStorage;

        public Guid CreateUser(string name)
        {
            return _userStorage.Add(name);
        }

        public void DeleteUser(Guid id)
        {
            _userStorage.Remove(id);
        }

    }
}
