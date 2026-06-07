using MassTransit;
using MediatR;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using Shared.Contracts.Events;

namespace OrderService.Application.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint)
    {
        _orderRepository = orderRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Create(request.ProductId, request.Quantity);

        await _orderRepository.AddAsync(order, cancellationToken);

        await _publishEndpoint.Publish(new OrderCreatedEvent
        {
            OrderId = order.Id,
            ProductId = order.ProductId,
            Quantity = order.Quantity,
            CreatedAt = order.CreatedAt
        }, cancellationToken);

        return new CreateOrderResult(order.Id, order.Status);
    }
}
