using GameShop.Domains;

namespace GameShop.Storages
{
    public interface IGameStorage
    {
        public Guid AddGame(string name, decimal cost, Genre genre);

        public void RemoveGame(Guid id);

        public void SellGame(Guid id);

        public List<Game> GetAllAvailableGames();

        public List<Game> GetAllGames();

        public Guid GetGame(Guid id);

    }
}
