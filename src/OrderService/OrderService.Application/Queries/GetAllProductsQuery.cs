using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Queries;

public record GetAllProductsQuery() : IRequest<List<Product>>;
