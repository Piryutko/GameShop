using EnsureThat;
using System;

namespace GameShopLibrary
{
    public class User
    {
        public User(string name)
        {
            Ensure.That(name).IsNotNullOrWhiteSpace();

            Name = name;
            Id = Guid.NewGuid();
        }

        public string Name { get; }

        public Guid Id { get; }
    }
}
