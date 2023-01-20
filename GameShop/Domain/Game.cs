namespace GameShop.Domains
{
    public partial class Game
    {
        public Game(string name, decimal cost, Genre genre)
        {
            Name = name;
            Cost = cost;
            Genre = genre;
            Id = Guid.NewGuid();
            IsBought = false;
        }

        public string Name { get; }

        public decimal Cost { get; }

        public Guid Id { get; }

        public Genre Genre { get; }

        public bool IsBought { get; private set; }

        public void Sell()
        {
            IsBought = true;
        }

    }
}
