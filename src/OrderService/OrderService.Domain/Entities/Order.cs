namespace OrderService.Domain.Entities;

public class Order
{
    public Ulid Id { get; private set; }
    public Ulid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public Product Product { get; private set; } = null!;

    private Order() { }

    public static Order Create(Ulid productId, int quantity)
    {
        return new Order
        {
            Id = Ulid.NewUlid(),
            ProductId = productId,
            Quantity = quantity,
            Status = OrderStatus.Pending,
            CreatedAt = DateTimeOffset.Now.ToUniversalTime()
        };
    }

    public void Confirm()
    {
        Status = OrderStatus.Confirmed;
        UpdatedAt = DateTimeOffset.Now.ToUniversalTime();
    }

    public void Fail()
    {
        Status = OrderStatus.Failed;
        UpdatedAt = DateTimeOffset.Now.ToUniversalTime();
    }
}
