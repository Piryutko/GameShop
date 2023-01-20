using GameShop.Domains;

namespace GameShop.Storages
{
    public interface IUserStorage
    {
        public Guid Add(string name);

        public bool Remove(Guid id);

        public List<User> GetAll();

        public Guid Exist(Guid userId);

    }
}
