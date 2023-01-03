using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopLibrary
{
    public class PurchaseStorage
    {
        public PurchaseStorage()
        {
            _purchases = new List<Purchase>();
        }

        private List<Purchase> _purchases;

        public void Add(Guid userId, Guid gameId)
        {
            _purchases.Add(new Purchase(userId, gameId));
        }

        public void Remove(Guid gameId)
        {
            var purchase = _purchases.Single(g => g.GameId == gameId);
            _purchases.Remove(purchase);
        }

        public List<Guid> GetAllGames(Guid userId)
        {
            var result = _purchases.Where(u => u.UserId == userId).Select(u => u.GameId).ToList();

            return result;
        }
    }
}
