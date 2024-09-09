using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using WebApi.Interfaces;

namespace WebApi.Services;

public class VisitService : IVisitService
{
    private readonly IVisitRepository _visitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VisitService(IVisitRepository visitRepository, IUnitOfWork unitOfWork)
    {
        _visitRepository = visitRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task RecordVisitAsync(int subscriptionId)
    {
        var visit = new Visit
        {
            SubscriptionId = subscriptionId,
            VisitDate = DateTime.Now,
        };
        await _unitOfWork.VisitRepository.AddAsync(visit);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CancelVisitAsync(int visitId)
    {
        var visit = await _visitRepository.GetByIdAsync(visitId);
        await _unitOfWork.VisitRepository.UpdateAsync(visit);
        await _unitOfWork.SaveChangesAsync();
    }
}