using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class VisitRepository : IVisitRepository
{
    private readonly ApplicationDbContext _context;

    public VisitRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Visit visit)
    {
        await _context.Visits.AddAsync(visit);
    }

    public Task UpdateAsync(Visit visit)
    {
        _context.Visits.Update(visit);
        return Task.CompletedTask;
    }

    public async Task<Visit> GetByIdAsync(int id)
    {
        return await _context.Visits.FindAsync(id);
    }

    public async Task<IEnumerable<Visit>> GetBySubscriptionIdAsync(int subscriptionId)
    {
        return await _context.Visits
            .Where(v => v.SubscriptionId == subscriptionId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Visit>> GetAllAsync()
    {
        return await _context.Visits.ToListAsync();
    }
    
}