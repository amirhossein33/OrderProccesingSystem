using MediatR;

namespace OrderService.Application.Commands;

public record CreateOrderCommand(Ulid ProductId, int Quantity) : IRequest<CreateOrderResult>;

public record CreateOrderResult(Ulid OrderId, string Status);
