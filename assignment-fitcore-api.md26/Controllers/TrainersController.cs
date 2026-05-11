using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace assignment_fitcore_api.md26.Controllers;

/// <summary>
/// Controller for trainers.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TrainersController : ControllerBase
{
    private readonly ITrainerService _service;

    public TrainersController(ITrainerService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all trainers.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trainers = await _service.GetAllAsync();

        return Ok(trainers);
    }

    /// <summary>
    /// Create trainer.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateTrainerDto dto)
    {
        var trainer = await _service.CreateAsync(dto);

        return Ok(trainer);
    }
}