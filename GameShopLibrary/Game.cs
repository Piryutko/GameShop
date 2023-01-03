using EnsureThat;
using System;

namespace GameShopLibrary
{
    public class Game
    {
        public Game(string name, double cost, Genre genre)
        {
            Ensure.That(name).IsNotNullOrWhiteSpace();
            Ensure.That(cost).IsGt(0.00);

            Name = name;
            Cost = cost;
            Genre = genre;
            Id = Guid.NewGuid();
            IsBought = false;
        }

        public string Name { get; set; }

        public double Cost { get; }

        public Guid Id { get; }

        public bool IsBought { get; private set; }

        public Genre Genre { get; }

        public void Sell()
        {
            IsBought = true;
        }

    }
}

