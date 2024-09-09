using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> BookTrainer(int clientId, int trainerId, DateTime bookingDate)
    {
        var bookings = await _bookingService.BookTrainerAsync(clientId, trainerId, bookingDate);
        return Ok(bookings);
    }

    [HttpGet("client/{clientID}")]
    public async Task<IActionResult> GetClientBookings(int clientId)
    {
        var bookings = await _bookingService.GetClientBookingAsync(clientId);
        return Ok(bookings);
    }

    [HttpGet("trainer/{client}")]
    public async Task<IActionResult> GetTrainerBookings(int trainerId)
    {
        var bookings = await _bookingService.GetTrainerBookingAsync(trainerId);
        return Ok(bookings);
    }
}