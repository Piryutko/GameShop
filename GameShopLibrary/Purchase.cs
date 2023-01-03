using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopLibrary
{
    public class Purchase
    {
        public Purchase(Guid userId, Guid gameId)
        {
            UserId = userId;

            GameId = gameId;

            PurchaseId = Guid.NewGuid();
        }

        public Guid PurchaseId { get; }

        public Guid UserId { get; }

        public Guid GameId { get; }

    }
}
