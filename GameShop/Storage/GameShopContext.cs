using GameShop.Domains;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Storages;

public partial class GameShopContext : DbContext
{
    public GameShopContext()
    {

    }

    public GameShopContext(DbContextOptions<GameShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-88G2M6G;Database=GameShop;Trusted_Connection=True;TrustServerCertificate=Yes;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Cost).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(entity => entity.Genre).HasConversion(entity => Convert.ToInt32(entity), dbEntity => (Genre)dbEntity);
            entity.Property(p => p.IsBought);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK_Purchase");

            entity.Property(e => e.PurchaseId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
