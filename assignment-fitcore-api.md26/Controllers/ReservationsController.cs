using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitCore.API.Controllers;

/// <summary>
/// Controller for reservations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _service;

    public ReservationsController(IReservationService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all reservations.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    /// <summary>
    /// Create reservation.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateReservationDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }
}