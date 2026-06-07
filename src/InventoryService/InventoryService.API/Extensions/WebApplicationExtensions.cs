using Microsoft.EntityFrameworkCore;
using InventoryService.Infrastructure.Persistence;

namespace InventoryService.API.Extensions;

public static class WebApplicationExtensions
{
    public static async Task ApplyDbMigrationsAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
        await db.Database.MigrateAsync();
    }
}
