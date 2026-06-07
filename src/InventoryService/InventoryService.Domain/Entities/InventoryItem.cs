namespace InventoryService.Domain.Entities;

public class InventoryItem
{
    public Ulid Id { get; private set; }
    public Ulid ProductId { get; private set; }
    public int Quantity { get; private set; }

    private InventoryItem() { }

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

    public void Replenish(int quantity)
    {
        if (quantity <= 0)
            throw new InvalidOperationException("Replenish quantity must be greater than zero.");

        Quantity += quantity;
    }
}
