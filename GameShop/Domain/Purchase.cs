namespace GameShop.Domains
{
    public partial class Purchase
    {
        public Purchase(Guid userId, Guid gameId)
        {
            UserId = userId;

            GameId = gameId;

            PurchaseId = Guid.NewGuid();
        }

        public Guid PurchaseId { get; set; }

        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

    }
}
