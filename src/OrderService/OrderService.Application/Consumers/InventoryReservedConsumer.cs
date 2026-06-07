using MassTransit;
using OrderService.Domain.Repositories;
using Shared.Contracts.Events;

namespace OrderService.Application.Consumers;

public class InventoryReservedConsumer : IConsumer<InventoryReservedEvent>
{
    private readonly IOrderRepository _orderRepository;

    public InventoryReservedConsumer(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Consume(ConsumeContext<InventoryReservedEvent> context)
    {
        var order = await _orderRepository.GetByIdAsync(context.Message.OrderId, context.CancellationToken);

        if (order is null)
            return;

        order.Confirm();
        await _orderRepository.UpdateAsync(order, context.CancellationToken);
    }
}
