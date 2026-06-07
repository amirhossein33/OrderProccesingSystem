using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Persistence;

namespace OrderService.API.Extensions;

public static class WebApplicationExtensions
{
    public static async Task ApplyDbMigrationsAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
        await db.Database.MigrateAsync();
    }
}
