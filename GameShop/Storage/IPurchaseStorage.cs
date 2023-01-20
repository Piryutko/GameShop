namespace GameShop.Storages
{
    public interface IPurchaseStorage
    {
        public void Add(Guid userId, Guid gameId);

        public void Remove(Guid gameId);

        public List<Guid> GetAllGames(Guid userId);

    }
}
