using Common.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services;

public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly ILogger<StatisticsService> _logger;

        public StatisticsService(IStatisticsRepository statisticsRepository, ILogger<StatisticsService> logger)
        {
            _statisticsRepository = statisticsRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<SubscriptionCountByTrainerModel>> GetSubscriptionCountByTrainerAsync()
        {
            try
            {
                return await _statisticsRepository.GetSubscriptionCountByTrainerAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting subscription count by trainer.");
                throw;
            }
        }

        public async Task<IEnumerable<NewClientsByMonthModel>> GetNewClientsByMonthAsync()
        {
            try
            {
                return await _statisticsRepository.GetNewClientsByMonthsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting new clients by month.");
                throw;
            }
        }

        public async Task<IEnumerable<PopularTrainersWithOccupiedPlacesModel>> GetPopularTrainersWithOccupiedPlacesAsync(int threshold)
        {
            try
            {
                return await _statisticsRepository.GetPopularTrainersWithOccupiedPlacesAsync(threshold);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting popular trainers with occupied places.");
                throw;
            }
        }

        public async Task<IEnumerable<VisitsCountByClientModel>> GetVisitsCountByClientAsync()
        {
            try
            {
                return await _statisticsRepository.GetVisitsCountByClientAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting visits count by client.");
                throw;
            }
        }

        public async Task<IEnumerable<ClientsWithSubscriptionsAboveVisitCountModel>> GetClientsWithSubscriptionsAboveVisitCountAsync(int minVisitCount)
        {
            try
            {
                return await _statisticsRepository.GetClientsWithSubscriptionsAboveVisitCountAsync(minVisitCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting clients with subscriptions above visit count.");
                throw;
            }
        }

        public async Task<IEnumerable<TotalIncomeByTrainerModel>> GetTotalIncomeByTrainerAsync()
        {
            try
            {
                return await _statisticsRepository.GetTotalIncomeByTrainerAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting total income by trainer.");
                throw;
            }
        }
    }