using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var ulidConverter = new ValueConverter<Ulid, string>(
            v => v.ToString(),
            v => Ulid.Parse(v));

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(ulidConverter).HasMaxLength(26);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");

            entity.HasData(
                new { Id = Ulid.Parse("01JGNX4SP0QZVS6QF4R3ZCWGA1"), Name = "Laptop", Price = 1299.99m },
                new { Id = Ulid.Parse("01JGNX4SP0QZVS6QF4R3ZCWGA2"), Name = "Smartphone", Price = 799.99m },
                new { Id = Ulid.Parse("01JGNX4SP0QZVS6QF4R3ZCWGA3"), Name = "Headphones", Price = 199.99m },
                new { Id = Ulid.Parse("01JGNX4SP0QZVS6QF4R3ZCWGA4"), Name = "Keyboard", Price = 89.99m },
                new { Id = Ulid.Parse("01JGNX4SP0QZVS6QF4R3ZCWGA5"), Name = "Monitor", Price = 549.99m }
            );
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(ulidConverter).HasMaxLength(26);
            entity.Property(e => e.ProductId).HasConversion(ulidConverter).HasMaxLength(26).IsRequired();
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Quantity).IsRequired();
            entity.HasOne(e => e.Product).WithMany().HasForeignKey(e => e.ProductId);
        });
    }
}
