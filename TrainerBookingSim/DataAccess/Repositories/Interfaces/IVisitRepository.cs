using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface IVisitRepository
{
    Task AddAsync(Visit visit);
    Task UpdateAsync(Visit visit);
    Task<Visit> GetByIdAsync(int id);
    Task<IEnumerable<Visit>> GetBySubscriptionIdAsync(int subscriptionId);
    Task<IEnumerable<Visit>> GetAllAsync();
}