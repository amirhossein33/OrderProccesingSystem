using OrderService.Domain.Entities;

namespace OrderService.Domain.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
}
