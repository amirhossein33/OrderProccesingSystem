namespace Shared.Contracts.Events;

public record InventoryReservedEvent
{
    public Ulid OrderId { get; init; }
    public Ulid ProductId { get; init; }
    public int QuantityReserved { get; init; }
}
