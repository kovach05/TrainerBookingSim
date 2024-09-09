using Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataAccess;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost("upsert-client-profile")]
    public async Task<IActionResult> UpsertClientProfile([FromBody] Client client)
    {
        await _clientService.UpsertClientProfileAsync(client);
        return Ok();
    }
}