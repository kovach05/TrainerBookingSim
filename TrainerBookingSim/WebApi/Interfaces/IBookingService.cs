using DataAccess.Models;

namespace WebApi.Interfaces;

public interface IBookingService
{
    Task<Booking> BookTrainerAsync(int clientId, int trainerId, DateTime bookingDate);
    Task<List<Booking>> GetClientBookingAsync(int clientId);
    Task<List<Booking>> GetTrainerBookingAsync(int trainerId);
}