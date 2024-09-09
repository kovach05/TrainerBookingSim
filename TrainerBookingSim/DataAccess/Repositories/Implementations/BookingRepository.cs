using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            try
            {
                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync(); // Зберігаємо зміни в базі даних
                return booking;
            }
            catch (Exception ex)
            {
                // Логування або обробка винятку
                throw new Exception("An error occurred while adding the booking.", ex);
            }
        }

        public async Task<List<Booking>> GetByClientIdAsync(
            int clientId, 
            int pageNumber = 1, 
            int pageSize = 10, 
            DateTimeOffset? startDate = null, 
            DateTimeOffset? endDate = null)
        {
            var query = _context.Bookings.AsQueryable();

            query = query.Where(b => b.ClientId == clientId);

            if (startDate.HasValue)
            {
                query = query.Where(b => b.BookingDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(b => b.BookingDate <= endDate.Value);
            }

            query = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }



        public async Task<List<Booking>> GetByTrainerIdAsync(int trainerId)
        {
            return await _context.Bookings.Where(b => b.TrainerId == trainerId).ToListAsync();
        }
    }
}