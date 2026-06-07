using InventoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InventoryService.Infrastructure.Persistence;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
    {
    }

    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var ulidConverter = new ValueConverter<Ulid, string>(
            v => v.ToString(),
            v => Ulid.Parse(v));

        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(ulidConverter).HasMaxLength(26);
            entity.Property(e => e.ProductId).HasConversion(ulidConverter).HasMaxLength(26).IsRequired();
            entity.HasIndex(e => e.ProductId).IsUnique();
            entity.Property(e => e.Quantity).IsRequired();

            entity.HasData(
                new { Id = Ulid.Parse("01JGNX5AA0QQNVENTORY000001"), ProductId = Ulid.Parse("01JGNX4SP0QZVS6QF4R3ZCWGA1"), Quantity = 50 },   // Laptop
                new { Id = Ulid.Parse("01JGNX5AA0QQNVENTORY000002"), ProductId = Ulid.Parse("01JGNX4SP0QZVS6QF4R3ZCWGA2"), Quantity = 100 },  // Smartphone
                new { Id = Ulid.Parse("01JGNX5AA0QQNVENTORY000003"), ProductId = Ulid.Parse("01JGNX4SP0QZVS6QF4R3ZCWGA3"), Quantity = 200 },  // Headphones
                new { Id = Ulid.Parse("01JGNX5AA0QQNVENTORY000004"), ProductId = Ulid.Parse("01JGNX4SP0QZVS6QF4R3ZCWGA4"), Quantity = 150 },  // Keyboard
                new { Id = Ulid.Parse("01JGNX5AA0QQNVENTORY000005"), ProductId = Ulid.Parse("01JGNX4SP0QZVS6QF4R3ZCWGA5"), Quantity = 30 }    // Monitor
            );
        });
    }
}
