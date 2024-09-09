using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _context;

    public SubscriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Subscription subscription)
    {
        await _context.Subscriptions.AddAsync(subscription);
    }

    public Task UpdateAsync(Subscription subscription)
    {
        _context.Subscriptions.Update(subscription);
        return Task.CompletedTask;
    }

    public async Task<Subscription> GetByIdAsync(int id)
    {
        return await _context.Subscriptions.FindAsync(id);
    }

    public async Task<IEnumerable<Subscription>> GetByUserIdAsync(string userId)
    {
        return await _context.Subscriptions
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> GetAllAsync()
    {
        return await _context.Subscriptions.ToListAsync();
    }

    public async Task LinkTrainerAsync(int subscriptionId, string trainerId)
    {
        var subscription = await _context.Subscriptions.FindAsync(subscriptionId);
        if (subscription != null)
        {
            subscription.TrainerId = trainerId;
            _context.Subscriptions.Update(subscription);
        }
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}