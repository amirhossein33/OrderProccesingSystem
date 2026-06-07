using MediatR;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Queries;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return null;

        return new OrderDto(
            order.Id,
            order.ProductId,
            order.Quantity,
            order.Status,
            order.CreatedAt,
            order.UpdatedAt);
    }
}
