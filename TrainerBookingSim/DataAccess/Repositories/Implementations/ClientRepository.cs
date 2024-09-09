using System.Resources;
using Common.Models;
using Dapper;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DataAccess.Repositories.Implementations;

public class ClientRepository : IClientRepository
{
    private readonly string _connectionString;
    private readonly ILogger<ClientRepository> _logger;
    private readonly ResourceManager _resourceManager;

    public ClientRepository(IConfiguration configuration, ILogger<ClientRepository> logger)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        _logger = logger;
        _resourceManager = new ResourceManager("YourNamespace.SqlQueries", typeof(ClientRepository).Assembly);
    }

    public async Task UpsertClientProfileAsync(Client client)
    {
        var sql = _resourceManager.GetString("UpsertClientProfile");

        using var connection = new NpgsqlConnection(_connectionString);
        {
            await connection.ExecuteAsync(sql, new
            {
                client.Id,
                client.DateOfBirth,
                client.Gender,
                client.PassportId,
                client.ExternalClientId
            });
        }
    }
}