namespace BusinessLogic.Interface;

public interface ISubscriptionService
{
    Task AddOrContinueSubscriptionAsync(string userId, int visits, int daysValid);
    Task LinkTrainerToSubscriptionAsync(int subscriprionId, string trainerId);
}