namespace InventoryService.Domain.Entities;

public class InventoryItem
{
    public Ulid Id { get; private set; }
    public Ulid ProductId { get; private set; }
    public int Quantity { get; private set; }

    private InventoryItem() { }

    public static InventoryItem Create(Ulid productId, int quantity)
    {
        return new InventoryItem
        {
            Id = Ulid.NewUlid(),
            ProductId = productId,
            Quantity = quantity
        };
    }

    public bool HasSufficientStock(int requestedQuantity)
    {
        return Quantity >= requestedQuantity;
    }

    public void Reserve(int quantity)
    {
        if (!HasSufficientStock(quantity))
            throw new InvalidOperationException("Insufficient stock.");

        Quantity -= quantity;
    }
}
