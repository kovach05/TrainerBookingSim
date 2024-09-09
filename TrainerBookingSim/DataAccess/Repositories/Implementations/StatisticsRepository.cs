using System.Resources;
using Common.Models;
using Dapper;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Repositories.Implementations;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly string? _connectionString;
    private readonly ResourceManager _resourceManager;

    public StatisticsRepository(IConfiguration configuration, ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }


    public async Task<IEnumerable<SubscriptionCountByTrainerModel>> GetSubscriptionCountByTrainerAsync()
    {
        var sql = _resourceManager.GetString("GetSubscriptionCountByTrainer");

        using var connection = new NpgsqlConnection(_connectionString);
        {
            return await connection.QueryAsync<SubscriptionCountByTrainerModel>(sql);
        }
    }

    public async Task<IEnumerable<NewClientsByMonthModel>> GetNewClientsByMonthsAsync()
    {
        var sql = _resourceManager.GetString("GetNewClientsByMonthsAsync");

        using var connection = new NpgsqlConnection(_connectionString);
        {
            return await connection.QueryAsync<NewClientsByMonthModel>(sql);
        }
    }

    public async Task<IEnumerable<PopularTrainersWithOccupiedPlacesModel>> GetPopularTrainersWithOccupiedPlacesAsync(int threshold)
    {
        var sql = _resourceManager.GetString("GetPopularTrainersWithOccupiedPlacesAsync");

        using var connection = new NpgsqlConnection(_connectionString);
        {
            return await connection.QueryAsync<PopularTrainersWithOccupiedPlacesModel>(sql, new { Threshold = threshold });
        }
    }

    public async Task<IEnumerable<VisitsCountByClientModel>> GetVisitsCountByClientAsync()
    {
        var sql = _resourceManager.GetString("GetVisitsCountByClientAsync");
        
        using var connection = new NpgsqlConnection(_connectionString);
        {
            return await connection.QueryAsync<VisitsCountByClientModel>(sql);
        }
    }

    public async Task<IEnumerable<ClientsWithSubscriptionsAboveVisitCountModel>> GetClientsWithSubscriptionsAboveVisitCountAsync(int minVisitCount)
    {
        var sql = _resourceManager.GetString("GetClientsWithSubscriptionsAboveVisitCountAsync");

        using var connection = new NpgsqlConnection(_connectionString);
        {
            return await connection.QueryAsync<ClientsWithSubscriptionsAboveVisitCountModel>(sql, new { MinVisitCount = minVisitCount });
        }
    }

    public async Task<IEnumerable<TotalIncomeByTrainerModel>> GetTotalIncomeByTrainerAsync()
    {
        var sql = _resourceManager.GetString("GetTotalIncomeByTrainerAsync");

        using var connection = new NpgsqlConnection(_connectionString);
        {
            return await connection.QueryAsync<TotalIncomeByTrainerModel>(sql);
        }
    }
}