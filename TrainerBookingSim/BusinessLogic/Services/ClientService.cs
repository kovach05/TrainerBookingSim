using Common.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataAccess;

public class ClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger<ClientService> _logger;

    public ClientService(IClientRepository clientRepository, ILogger<ClientService> logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;
    }

    public async Task UpsertClientProfileAsync(Client client)
    {
        try
        {
            await _clientRepository.UpsertClientProfileAsync(client);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while upserting client profile");
            throw;
        }
    }
}