using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace WebApi;

public class ExpiryCheckWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public ExpiryCheckWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await context.Subscriptions
                    .Where(s => s.ExpiryDate < DateTime.UtcNow && !s.IsExpired)
                    .ExecuteDeleteAsync(stoppingToken);

            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}