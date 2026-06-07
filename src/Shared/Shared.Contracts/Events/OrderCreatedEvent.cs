namespace Shared.Contracts.Events;

public record OrderCreatedEvent
{
    public Ulid OrderId { get; init; }
    public Ulid ProductId { get; init; }
    public int Quantity { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}
