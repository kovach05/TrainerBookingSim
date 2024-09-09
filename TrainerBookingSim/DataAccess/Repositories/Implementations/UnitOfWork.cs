using DataAccess.Context;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    private IBookingRepository _bookingRepository;
    private IVisitRepository _visitRepository;

    public IBookingRepository BookingRepository => _bookingRepository ??= new BookingRepository(_context);
    public IVisitRepository VisitRepository => _visitRepository ??= new VisitRepository(_context);

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}