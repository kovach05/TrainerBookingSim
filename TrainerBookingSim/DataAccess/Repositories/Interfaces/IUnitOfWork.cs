namespace DataAccess.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBookingRepository BookingRepository { get; }
    IVisitRepository VisitRepository { get; }
    Task SaveChangesAsync();

}