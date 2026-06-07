namespace Shared.Contracts.Events;

public record InventoryFailedEvent
{
    public Ulid OrderId { get; init; }
    public Ulid ProductId { get; init; }
    public string Reason { get; init; } = string.Empty;
}
