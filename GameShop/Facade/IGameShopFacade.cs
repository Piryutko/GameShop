using GameShop.Domains;

namespace GameShop.Facade
{
    public interface IGameShopFacade
    {
        public Guid AddGame(string name, decimal cost, Genre genre);

        public List<Game> GetAllGames();

        public List<Game> GetAllAvailableGames();

        public void RemoveGame(Guid id);

        public void BuyGame(Guid userId, Guid gameId);

    }
}
