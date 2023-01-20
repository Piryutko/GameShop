using EnsureThat;

namespace GameShop.Domains;

public partial class User
{
    public User(string name)
    {
        Ensure.That(name).IsNotNullOrWhiteSpace();

        Name = name;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
}
