using BusinessLogic.Interface;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace BusinessLogic.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task AddOrContinueSubscriptionAsync(string userId, int visits, int daysValid)
    {
        var existingSubscriptions = await _subscriptionRepository.GetByUserIdAsync(userId);
        var existingSubscription = existingSubscriptions
            .FirstOrDefault(s => !s.IsExpired && s.ExpiryDate > DateTime.Now);

        if (existingSubscription != null)
        {
            existingSubscription.Visits += visits;
            existingSubscription.ExpiryDate = DateTime.Now.AddDays(daysValid);
            await _subscriptionRepository.UpdateAsync(existingSubscription);
        }
        else
        {
            var subscription = new Subscription
            {
                UserId = userId,
                Visits = visits,
                CreatedDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(daysValid),
                IsExpired = false
            };

            await _subscriptionRepository.AddAsync(subscription);
        }

        await _subscriptionRepository.SaveChangesAsync();
    }

    public async Task LinkTrainerToSubscriptionAsync(int subscriptionId, string trainerId)
    {
        await _subscriptionRepository.LinkTrainerAsync(subscriptionId, trainerId);
        await _subscriptionRepository.SaveChangesAsync();
    }
}