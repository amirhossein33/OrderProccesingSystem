using MediatR;

namespace OrderService.Application.Queries;

public record GetOrderByIdQuery(Ulid OrderId) : IRequest<OrderDto?>;

public record OrderDto(Ulid Id, Ulid ProductId, int Quantity, string Status, DateTimeOffset CreatedAt, DateTimeOffset? UpdatedAt);
