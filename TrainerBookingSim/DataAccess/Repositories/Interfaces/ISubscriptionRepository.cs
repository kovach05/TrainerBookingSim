
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface ISubscriptionRepository
{
    Task AddAsync(Subscription subscription);
    Task UpdateAsync(Subscription subscription);
    Task<Subscription> GetByIdAsync(int id);
    Task<IEnumerable<Subscription>> GetByUserIdAsync(string userId);
    Task<IEnumerable<Subscription>> GetAllAsync();
    Task LinkTrainerAsync(int subscriprionId, string trainerId);
    Task SaveChangesAsync();
}