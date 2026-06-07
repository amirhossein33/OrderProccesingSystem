using MassTransit;
using InventoryService.Domain.Repositories;
using Shared.Contracts.Events;

namespace InventoryService.Application.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderCreatedConsumer(IInventoryRepository inventoryRepository, IPublishEndpoint publishEndpoint)
    {
        _inventoryRepository = inventoryRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;

        var item = await _inventoryRepository.GetByProductIdAsync(message.ProductId, context.CancellationToken);

        if (item is null || !item.HasSufficientStock(message.Quantity))
        {
            await _publishEndpoint.Publish(new InventoryFailedEvent
            {
                OrderId = message.OrderId,
                ProductId = message.ProductId,
                Reason = item is null
                    ? $"Product '{message.ProductId}' not found in inventory."
                    : $"Insufficient stock for product '{message.ProductId}'. Available: {item.Quantity}, Requested: {message.Quantity}"
            }, context.CancellationToken);

            return;
        }

        item.Reserve(message.Quantity);
        await _inventoryRepository.UpdateAsync(item, context.CancellationToken);

        await _publishEndpoint.Publish(new InventoryReservedEvent
        {
            OrderId = message.OrderId,
            ProductId = message.ProductId,
            QuantityReserved = message.Quantity
        }, context.CancellationToken);
    }
}
