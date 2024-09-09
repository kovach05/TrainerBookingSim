using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface IBookingRepository
{
    Task<Booking> AddAsync(Booking booking);
    Task<List<Booking>> GetByClientIdAsync(int clientId, int pageNumber = 1, int pageSize = 10, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null);
    Task<List<Booking>> GetByTrainerIdAsync(int trainerId);
}