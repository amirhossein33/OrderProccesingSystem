namespace OrderService.Domain.Entities;

public class Product
{
    public Ulid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    private Product() { }

    public static Product Create(string name, decimal price)
    {
        return new Product
        {
            Id = Ulid.NewUlid(),
            Name = name,
            Price = price
        };
    }
}
