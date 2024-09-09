using Common.Models;

namespace BusinessLogic;

public interface IStatisticsService
{
    Task<IEnumerable<SubscriptionCountByTrainerModel>> GetSubscriptionCountByTrainerAsync();
    Task<IEnumerable<NewClientsByMonthModel>> GetNewClientsByMonthAsync();
    Task<IEnumerable<PopularTrainersWithOccupiedPlacesModel>> GetPopularTrainersWithOccupiedPlacesAsync(int threshold);
    Task<IEnumerable<VisitsCountByClientModel>> GetVisitsCountByClientAsync();
    Task<IEnumerable<ClientsWithSubscriptionsAboveVisitCountModel>> GetClientsWithSubscriptionsAboveVisitCountAsync(int minVisitCount);
    Task<IEnumerable<TotalIncomeByTrainerModel>> GetTotalIncomeByTrainerAsync();
}