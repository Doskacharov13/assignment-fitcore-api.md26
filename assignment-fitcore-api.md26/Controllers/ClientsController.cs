using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace assignment_fitcore_api.md26.Controllers;

/// <summary>
/// Controller for managing clients.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    /// <summary>
    /// Get all clients.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _clientService.GetAllAsync();

        return Ok(clients);
    }

    /// <summary>
    /// Create client.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateClientDto dto)
    {
        var client = await _clientService.CreateAsync(dto);

        return Ok(client);
    }
}