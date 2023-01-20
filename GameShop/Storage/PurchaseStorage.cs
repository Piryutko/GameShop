using GameShop.Domains;

namespace GameShop.Storages
{
    public class PurchaseStorage : IPurchaseStorage
    {
        public PurchaseStorage()
        {
            _gameShop = new GameShopContext();
        }

        private readonly GameShopContext _gameShop;

        public void Add(Guid userId, Guid gameId)
        {
            _gameShop.Purchases.Add(new Purchase(userId, gameId));
            _gameShop.SaveChanges();
        }

        public void Remove(Guid gameId)
        {
            var purchase = _gameShop.Purchases.Single(g => g.GameId == gameId);
            _gameShop.Purchases.Remove(purchase);
            _gameShop.SaveChanges();
        }

        public List<Guid> GetAllGames(Guid userId)
        {
            var result = _gameShop.Purchases.Where(u => u.UserId == userId).Select(u => u.GameId).ToList();

            return result;
        }
    }
}
