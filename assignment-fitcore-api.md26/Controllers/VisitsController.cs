using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitCore.API.Controllers;

/// <summary>
/// Controller for visits.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VisitsController : ControllerBase
{
    private readonly IVisitService _service;

    public VisitsController(IVisitService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all visits.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    /// <summary>
    /// Register visit.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Register(CreateVisitDto dto)
    {
        return Ok(await _service.RegisterVisitAsync(dto));
    }
}