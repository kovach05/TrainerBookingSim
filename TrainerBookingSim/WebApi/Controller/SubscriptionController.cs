using BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddOrContinueSubscription(string userId, int visits, int daysValid)
    {
        await _subscriptionService.AddOrContinueSubscriptionAsync(userId, visits, daysValid);
        return Ok();
    }

    [HttpPost("{subscriptionId}/link-trainer/{trainerId}")]
    public async Task<IActionResult> LinkTrainerToSubscription(int subscriptionId, string trainerId)
    {
        await _subscriptionService.LinkTrainerToSubscriptionAsync(subscriptionId, trainerId);
        return Ok();
    }
}