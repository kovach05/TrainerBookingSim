using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using WebApi.Interfaces;

namespace WebApi.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookingService(IBookingRepository bookingRepository, IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Booking> BookTrainerAsync(int clientId, int trainerId, DateTime bookingDate)
    {
        var booking = new Booking
        {
            ClientId = clientId,
            TrainerId = trainerId,
            BookingDate = bookingDate,
            CreatedDate = DateTime.Now
        };

        await _bookingRepository.AddAsync(booking);
        await _unitOfWork.SaveChangesAsync();
        
        return booking;
    }

    public async Task<List<Booking>> GetClientBookingAsync(int clientId)
    {
        return await _bookingRepository.GetByClientIdAsync(clientId);
    }

    public async Task<List<Booking>> GetTrainerBookingAsync(int trainerId)
    {
        return await _bookingRepository.GetByTrainerIdAsync(trainerId);
    }
}