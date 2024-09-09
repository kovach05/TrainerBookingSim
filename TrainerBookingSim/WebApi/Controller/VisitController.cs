using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class VisitController : ControllerBase
{
    private readonly IVisitService _visitService;

    public VisitController(IVisitService visitService)
    {
        _visitService = visitService;
    }

    [HttpPost("record")]
    public async Task<IActionResult> RecordVisit(int subscriptionId)
    {
        await _visitService.RecordVisitAsync(subscriptionId);
        return Ok();
    }
}