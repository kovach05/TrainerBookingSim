using Common.Models;

namespace DataAccess.Repositories.Interfaces;

public interface IClientRepository
{
    Task UpsertClientProfileAsync(Client client);
}