namespace WebApi.Interfaces;

public interface IVisitService
{
    Task RecordVisitAsync(int subscriptionId);
    Task CancelVisitAsync(int visitId);
}