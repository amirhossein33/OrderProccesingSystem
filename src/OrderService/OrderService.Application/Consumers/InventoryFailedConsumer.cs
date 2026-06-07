using MassTransit;
using OrderService.Domain.Repositories;
using Shared.Contracts.Events;

namespace OrderService.Application.Consumers;

public class InventoryFailedConsumer : IConsumer<InventoryFailedEvent>
{
    private readonly IOrderRepository _orderRepository;

    public InventoryFailedConsumer(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Consume(ConsumeContext<InventoryFailedEvent> context)
    {
        var order = await _orderRepository.GetByIdAsync(context.Message.OrderId, context.CancellationToken);

        if (order is null)
            return;

        order.Fail();
        await _orderRepository.UpdateAsync(order, context.CancellationToken);
    }
}
