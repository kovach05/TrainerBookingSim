using Common.Models;

namespace DataAccess.Repositories.Interfaces;

public interface IStatisticsRepository
{
    Task<IEnumerable<SubscriptionCountByTrainerModel>> GetSubscriptionCountByTrainerAsync();
    Task<IEnumerable<NewClientsByMonthModel>> GetNewClientsByMonthsAsync();
    Task<IEnumerable<PopularTrainersWithOccupiedPlacesModel>> GetPopularTrainersWithOccupiedPlacesAsync(int threshold);
    Task<IEnumerable<VisitsCountByClientModel>> GetVisitsCountByClientAsync();
    Task<IEnumerable<ClientsWithSubscriptionsAboveVisitCountModel>> GetClientsWithSubscriptionsAboveVisitCountAsync(int minVisitCount);
    Task<IEnumerable<TotalIncomeByTrainerModel>> GetTotalIncomeByTrainerAsync();
}